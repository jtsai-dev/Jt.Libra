using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Jt.Libra.Core.Security;
using Jt.Libra.Domain.Entity;
using Jt.Libra.Infrastructure.Exceptions;
using Jt.Libra.WebApi.Attrbutes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Jt.Libra.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JtControllerBase : ControllerBase
    {
        public Account account;
        
        //public class ClaimsSession
        //{
        //    public static long UserId
        //    {
        //        get
        //        {
        //            var userIdClaim = _principal.Claims.FirstOrDefault(c => c.Type == JtClaimTypes.UserId);
        //            if (string.IsNullOrEmpty(userIdClaim?.Value))
        //            {
        //                throw new AuthorizationException("登录状态异常，请重新登陆");
        //            }

        //            string id = userIdClaim.Value;
        //            return long.Parse(id);
        //        }
        //    }

        //    public static string Mobile
        //    {
        //        get
        //        {
        //            var mobileClaim = _principal.Claims.FirstOrDefault(c => c.Type == JtClaimTypes.Mobile);
        //            return mobileClaim.Value;
        //        }
        //    }

        //    public static string AccountName
        //    {
        //        get
        //        {
        //            var accountNameClaim = _principal.Claims.FirstOrDefault(c => c.Type == JtClaimTypes.AccountName);
        //            if (string.IsNullOrEmpty(accountNameClaim?.Value))
        //            {
        //                throw new AuthorizationException("登录状态异常，请重新登陆");
        //            }
        //            return accountNameClaim.Value;
        //        }
        //    }

        //    public static int? Gender
        //    {
        //        get
        //        {
        //            var gernderClaim = _principal.Claims.FirstOrDefault(c => c.Type == JtClaimTypes.AccountName);
        //            if (string.IsNullOrEmpty(gernderClaim?.Value))
        //            {
        //                return null;
        //            }
        //            return int.Parse(gernderClaim.Value);
        //        }
        //    }
        //}
    }
}