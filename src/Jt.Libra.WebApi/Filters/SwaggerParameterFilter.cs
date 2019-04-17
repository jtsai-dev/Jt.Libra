using Jt.Libra.WebApi.Attrbutes;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Jt.Libra.WebApi.Filters
{
    public class SwaggerParameterFilter : IOperationFilter
    {
        public List<Tuple<string, string>> whiteList = new List<Tuple<string, string>> {
            new Tuple<string, string>("Auth/SessionKey", "POST")
        };

        public void Apply(Operation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null)
                operation.Parameters = new List<IParameter>();

            MethodInfo methodInfo;
            context.ApiDescription.TryGetMethodInfo(out methodInfo);
            var authAttr = methodInfo?.GetCustomAttribute<AuthAttribute>()
                ?? methodInfo?.DeclaringType?.GetCustomAttribute<AuthAttribute>();

            if (authAttr == null || !authAttr.IsHandle)
            {
                return;
            }

            operation.Parameters.Add(new NonBodyParameter()
            {
                Name = "token",
                In = "header",
                Description = "Authorization Token",
                Required = true,
                Type = "string"
            });
        }
    }
}
