using Jt.Libra.Domain.Entity;
using Jt.Libra.Core.Security;
using Jt.Libra.Infrastructure;
using Jt.Libra.Infrastructure.Exceptions;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Jt.Libra.Domain.IRepository;
using System.Net.Http;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;
using Jt.Libra.Infrastructure.Enums;

namespace Jt.Libra.Application
{
    public class UserService : IUserService
    {
        private readonly HttpClient http;
        private readonly string wechat_AppId;
        private readonly string wechatApi_Base;
        private readonly string wechatApi_AccessToken;
        private readonly string wechatApi_UserInfo;

        private ILogger _logger;
        private IConfiguration _configuration;
        private IUserRepository _userRepository;
        private IUserMappingRepository _userMappingRepository;
        private IWxUserRepository _wxUserRepository;
        public UserService(
            IConfiguration configuration,
            IUserRepository userRepository,
            IUserMappingRepository userMappingRepository,
            IWxUserRepository wxUserRepository,
            ILogger<UserService> logger)
        {
            _logger = logger;
            _configuration = configuration;
            _userRepository = userRepository;
            _userMappingRepository = userMappingRepository;
            _wxUserRepository = wxUserRepository;

            http = new HttpClient();
            wechat_AppId = _configuration.GetSection("Wechat")["AppId"];
            wechatApi_Base = _configuration.GetSection("Wechat")["Base"];
            wechatApi_AccessToken = _configuration.GetSection("Wechat")["AccessToken"];
            wechatApi_UserInfo = _configuration.GetSection("Wechat")["UserInfo"];
        }

        public async Task<AccessTokenOutput> SessionKey(AccessTokenInput input)
        {
            if (!string.IsNullOrEmpty(input.Code))
            {
                // 微信登陆
                var wxToken = await WxGetToken(input.Code);
                var wxUserInfo = await WxGetUserInfo(wxToken.AccessToken, wxToken.OpenId);
                var mapping = await _userMappingRepository.GetAsync(wxToken.OpenId, wechat_AppId);
                if (mapping == null)
                {
                    var wxUserId = await _wxUserRepository.InsertAsync(new Domain.Entity.WxUserInfo()
                    {
                        Avatar = wxUserInfo.Avatar,
                        City = wxUserInfo.City,
                        Country = wxUserInfo.Country,
                        Gender = wxUserInfo.Gender,
                        NickName = wxUserInfo.NickName,
                        Province = wxUserInfo.Province,
                    });
                    await _userMappingRepository.InsertAsync(new UserMapping()
                    {
                        AppId = wechat_AppId,
                        OpenId = wxUserInfo.OpenId,
                        WxUserId = wxUserId,
                    });
                }
                else
                {
                    await _wxUserRepository.UpdateAsync(new WxUserInfo()
                    {
                        Id = mapping.WxUserId.Value,
                        Avatar = wxUserInfo.Avatar,
                        City = wxUserInfo.City,
                        Country = wxUserInfo.Country,
                        Gender = wxUserInfo.Gender,
                        NickName = wxUserInfo.NickName,
                        Province = wxUserInfo.Province,
                        UpdateTime = DateTime.Now,
                    });
                }

                var token = GenerateToken(UserTypeEnum.Wechat, mapping.WxUserId.Value, wxUserInfo.NickName, wxUserInfo.Gender);
                return new AccessTokenOutput()
                {
                    Token = token,
                };
            }

            if (!string.IsNullOrEmpty(input.PhoneNumber) && !string.IsNullOrEmpty(input.Password))
            {
                var user = await _userRepository.GetAsync(input.PhoneNumber, input.Password);
                if (user == null)
                {
                    throw new AuthorizationException("登陆失败，请确认账号和密码");
                }

                var token = GenerateToken(UserTypeEnum.Account, user.Id, user.UserName, user.Gender);
                return new AccessTokenOutput()
                {
                    Token = token,
                };
            }

            throw new ValidationException();
        }

        private async Task<WxAccessTokenResponse> WxGetToken(string code)
        {
            var response = await http.GetStringAsync(string.Format(wechatApi_Base + wechatApi_AccessToken, code));
            var model = JsonConvert.DeserializeObject<WxAccessTokenResponse>(response);
            if (model.ErrCode.HasValue)
            {
                throw new AuthorizationException("无法获取微信账号信息，请刷新或稍后重试", new Exception("微信登陆失败：" + response));
            }
            return model;
        }
        private async Task<WxUserInfoResponse> WxGetUserInfo(string token, string openId)
        {
            var response = await http.GetStringAsync(string.Format(wechatApi_Base + wechatApi_UserInfo, token, openId));
            var model = JsonConvert.DeserializeObject<WxUserInfoResponse>(response);
            if (model.ErrCode.HasValue)
            {
                throw new AuthorizationException("无法获取微信账号信息，请刷新或稍后重试", new Exception("微信登陆失败：" + response));
            }
            return model;
        }

        private string GenerateToken(UserTypeEnum type, long id, string name, int gender)
        {
            // http://stackoverflow.com/questions/18223868/how-to-encrypt-jwt-security-token
            var claimsIdentity = new ClaimsIdentity(new[]
            {
                new Claim(JtClaimTypes.UserType, ((int)type).ToString()),
                new Claim(JtClaimTypes.Id, id.ToString()),
                new Claim(JtClaimTypes.AccountName, name),
                new Claim(JtClaimTypes.Gender, gender.ToString()),
            });

            var securityKey = new SymmetricSecurityKey(Encoding.Default.GetBytes(JtConstants.JwtSecurityKey));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            var audience = _configuration["Jwt:Audience"];
            var issuer = _configuration["Jwt:Issuer"];
            double duration = Convert.ToDouble(_configuration["Jwt:Duration"]);
            DateTime issuedAt = DateTime.UtcNow;
            DateTime expires = issuedAt.AddHours(duration);

            // create the jwt
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateJwtSecurityToken(
                audience: audience,
                issuer: issuer,
                subject: claimsIdentity,
                notBefore: issuedAt,
                expires: expires,
                signingCredentials: signingCredentials);
            var tokenString = tokenHandler.WriteToken(token);
            return tokenString;
        }
    }
}
