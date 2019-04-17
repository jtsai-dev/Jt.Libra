using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Jt.Libra.Application.DirectoryInfo;
using Jt.Libra.Core.Security;
using Jt.Libra.Infrastructure.Exceptions;
using Jt.Libra.WebApi.Attrbutes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Jt.Libra.WebApi.Controllers
{
    [Auth]
    public class DirectoryController : JtControllerBase
    {
        private readonly IDirectoryInfoService _service;

        public DirectoryController(
             IDirectoryInfoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<object> Tree()
        {
            return null;
        }
    }
}
