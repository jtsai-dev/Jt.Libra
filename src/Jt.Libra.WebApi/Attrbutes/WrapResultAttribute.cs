using Jt.Libra.Infrastructure.Enums;
using Jt.Libra.Infrastructure.Extension;
using Jt.Libra.Infrastructure.Helper;
using Jt.Libra.WebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Formatters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jt.Libra.WebApi.Attrbutes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface | AttributeTargets.Method)]
    public class WrapResultAttribute : ResultFilterAttribute
    {
        /// <summary>
        /// Wrap result on success.
        /// </summary>
        public bool IsWrap { get; set; }
        public WrapResultAttribute(bool isWrap = true)
        {
            IsWrap = isWrap;
        }

        //public override void OnResultExecuting(ResultExecutingContext context)
        //{
        //    if (IsWrap)
        //    {
        //        var result = context.Result;
        //        if (result is Result)
        //        {

        //        }
        //        else
        //        {
        //            var model = new Result<dynamic>()
        //            {
        //                Code = (int)ResultCodeEnum.Success,
        //                Data = (result as ObjectResult).Value,
        //            };
        //            context.Result = new ObjectResult(model);
        //        }
        //    }

        //    base.OnResultExecuting(context);
        //}
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface | AttributeTargets.Method)]
    public class UnWrapResultAttribue : WrapResultAttribute
    {
        public UnWrapResultAttribue() : base(false)
        {
        }
    }
}
