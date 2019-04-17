using Jt.Libra.Infrastructure.Enums;
using Jt.Libra.Infrastructure.Extension;
using Jt.Libra.Infrastructure.Helper;
using Jt.Libra.WebApi.Attrbutes;
using Jt.Libra.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Reflection;

namespace Jt.Libra.WebApi.Filters
{
    public class WrapResultFilter : IResultFilter
    {
        private readonly IConfiguration _configuration;
        public WrapResultFilter(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void OnResultExecuted(ResultExecutedContext context)
        {
        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
            if (!context.ActionDescriptor.IsControllerAction())
            {
                return;
            }

            var defaultValue = new WrapResultAttribute(_configuration.GetValue<bool>("WrapResultDefault"));
            var methodInfo = context.ActionDescriptor.GetMethodInfo();
            var wrapResultAttribute = ReflectionHelper.GetAttributeOrDefault(methodInfo, defaultValue);
            if (!wrapResultAttribute.IsWrap)
            {
                return;
            }

            if (context.Result is ObjectResult)
            {
                var objectResult = context.Result as ObjectResult;
                if (objectResult == null)
                {
                    throw new ArgumentException($"{nameof(context.Result)} should be ObjectResult!");
                }

                if (!(objectResult.Value is Result))
                {
                    objectResult.Value = new Result((int)ResultCodeEnum.Success) { Data = objectResult.Value };
                }
            }
            else if (context.Result is JsonResult)
            {
                var jsonResult = context.Result as JsonResult;
                if (jsonResult == null)
                {
                    throw new ArgumentException($"{nameof(context.Result)} should be JsonResult!");
                }

                jsonResult.Value = new Result((int)ResultCodeEnum.Success) { Data = jsonResult.Value };
            }
            else if (context.Result is EmptyResult)
            {
                context.Result = new ObjectResult(new Result((int)ResultCodeEnum.Success));
            }
            else
            {
                // Null
            }
        }
    }
}
