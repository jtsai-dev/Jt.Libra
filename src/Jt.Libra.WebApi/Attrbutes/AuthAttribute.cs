using Jt.Libra.Core.Security;
using Jt.Libra.Domain.Entity;
using Jt.Libra.Infrastructure;
using Jt.Libra.Infrastructure.Enums;
using Jt.Libra.Infrastructure.Exceptions;
using Jt.Libra.WebApi.Configuration;
using Jt.Libra.WebApi.Controllers;
using Jt.Libra.WebApi.Models;
using log4net;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Threading;

namespace Jt.Libra.WebApi.Attrbutes
{
    [AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = false)]
    public class AuthAttribute : ActionFilterAttribute
    {
        public bool IsHandle { get; }

        /// <summary>
        /// the attribute for auth
        /// support on action and controller
        /// </summary>
        /// <param name="isHandle"></param>
        public AuthAttribute(bool isHandle = true)
        {
            IsHandle = isHandle;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (IsHandle)
            {
                string token;
                if (!TryGetToken(context, out token))
                {
                    throw new AuthorizationException();
                }

                try
                {
                    var securityKey = new SymmetricSecurityKey(System.Text.Encoding.Default.GetBytes(JtConstants.JwtSecurityKey));

                    SecurityToken securityToken;
                    JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
                    var jwtConfig = JtConfiguration.GetInstance().GetSection("Jwt").Get<JwtConfig>();
                    TokenValidationParameters validationParameters = new TokenValidationParameters()
                    {
                        ValidAudience = jwtConfig.Audience,
                        ValidIssuer = jwtConfig.Issuer,
                        ValidateLifetime = jwtConfig.Lifetime,
                        ValidateIssuerSigningKey = jwtConfig.IssuerSigningKey,
                        IssuerSigningKey = securityKey,
                    };

                    var principal = handler.ValidateToken(token, validationParameters, out securityToken);
                    Thread.CurrentPrincipal = principal;
                    context.HttpContext.User = principal;

                    (context.Controller as JtControllerBase).account = AnalyzingJwt(principal);
                }
                catch (SecurityTokenInvalidLifetimeException ex)
                {
                    throw new JtException("登录状态超时，请重新登录", ResultCodeEnum.TokenOvertime, ex);
                }
                catch (SecurityTokenValidationException ex)
                {
                    throw new AuthorizationException("无效的登录状态，请重新登陆", ex);
                }
                catch (Exception ex)
                {
                    throw new AuthorizationException("登录状态异常，请重新登陆", ex);
                }
            }

            base.OnActionExecuting(context);
        }

        private static bool TryGetToken(ActionExecutingContext context, out string token)
        {
            token = null;
            Microsoft.Extensions.Primitives.StringValues tokenHeader;
            if (!context.HttpContext.Request.Headers.TryGetValue("token", out tokenHeader) || tokenHeader.Count() > 1)
            {
                return false;
            }
            token = tokenHeader.ElementAt(0);
            return true;
        }

        private static Account AnalyzingJwt(System.Security.Claims.ClaimsPrincipal principal)
        {
            var userTypeClaim = principal.Claims.FirstOrDefault(p => p.Type == JtClaimTypes.UserType);
            var idClaim = principal.Claims.FirstOrDefault(p => p.Type == JtClaimTypes.Id);
            var accountNameClaim = principal.Claims.FirstOrDefault(c => c.Type == JtClaimTypes.AccountName);
            var gernderClaim = principal.Claims.FirstOrDefault(c => c.Type == JtClaimTypes.Gender);
            var mobileClaim = principal.Claims.FirstOrDefault(c => c.Type == JtClaimTypes.Mobile);
            if (string.IsNullOrEmpty(userTypeClaim?.Value) || string.IsNullOrEmpty(idClaim?.Value) || string.IsNullOrEmpty(accountNameClaim?.Value))
            {
                throw new AuthorizationException("登录状态异常，请重新登陆");
            }
            int? gender = null;
            if (!string.IsNullOrEmpty(gernderClaim.Value))
            {
                gender = int.Parse(gernderClaim.Value);
            }

            // TODO: 调整登陆账号信息的dto
            return new Account()
            {
                Id = long.Parse(idClaim.Value),
                AccountName = accountNameClaim.Value,
                PhoneNumber = mobileClaim.Value,
                Gender = gender,
            };
        }
    }

    [AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = false)]
    public class UnAuthAttribute : AuthAttribute
    {
        public UnAuthAttribute() : base(false) { }
    }
}