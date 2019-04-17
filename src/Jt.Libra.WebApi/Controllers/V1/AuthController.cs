using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Jt.Libra.Application;
using Jt.Libra.WebApi.Attrbutes;
using Microsoft.AspNetCore.Mvc;

namespace Jt.Libra.WebApi.Controllers
{
    public class AuthController : JtControllerBase
    {
        private readonly IUserService accountService;

        public AuthController(
             IUserService accountService)
        {
            this.accountService = accountService;
        }

        /// <summary>
        /// 登录凭证
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<AccessTokenOutput> SessionKey(AccessTokenInput input)
        {
            return await accountService.SessionKey(input);
        }
    }
}
