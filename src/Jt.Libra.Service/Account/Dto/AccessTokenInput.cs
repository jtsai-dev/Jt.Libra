using System;
using System.Collections.Generic;
using System.Text;

namespace Jt.Libra.Application
{
    public class AccessTokenInput
    {
        /// <summary>
        /// 微信用户登陆凭证
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
    }
}
