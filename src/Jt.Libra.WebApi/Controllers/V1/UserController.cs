using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Jt.Libra.Application;
using Jt.Libra.Infrastructure.Exceptions;
using Jt.Libra.WebApi.Attrbutes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Jt.Libra.WebApi.Controllers
{
    [Auth]
    public class UserController : JtControllerBase
    {
        private readonly IUserService _userService;

        public UserController(
             IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public UserOutput UserInfo(long id)
        {
            return new UserOutput()
            {
                Id = 1,
                NickName = "Jt",
            };
        }

        [HttpGet, Route("List")]
        public IEnumerable<UserOutput> UserInfo()
        {
            return new List<UserOutput>() {
                new UserOutput(){
                    Id = 1,
                    NickName = "Jt",
                }
            };
        }

        public class UserOutput
        {
            public long Id { get; set; }
            public string NickName { get; set; }
        }
    }
}
