using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Jt.Libra.Infrastructure.Helper
{
    public static class ReflectionHelper
    {
        public static List<Type> ObjectResultTypes { get; }

        static ReflectionHelper()
        {
            ObjectResultTypes = new List<Type>
            {
                typeof(JsonResult),
                typeof(ObjectResult),
                typeof(NoContentResult)
            };
        }

        public static bool IsObjectResult(Type returnType)
        {
            if (returnType == typeof(Task))
            {
                returnType = typeof(void);
            }

            if (returnType.GetTypeInfo().IsGenericType && returnType.GetGenericTypeDefinition() == typeof(Task<>))
            {
                returnType = returnType.GenericTypeArguments[0];
            }

            if (!typeof(IActionResult).IsAssignableFrom(returnType))
            {
                return true;
            }

            return ObjectResultTypes.Any(t => t.IsAssignableFrom(returnType));
        }

        public static T GetAttributeOrDefault<T>(MethodInfo methodInfo,
            T defaultValue = default(T),
            bool inherit = true)
            where T : class
        {
            return methodInfo.GetCustomAttributes(true).OfType<T>().FirstOrDefault()
                   ?? methodInfo.ReflectedType?.GetTypeInfo().GetCustomAttributes(true).OfType<T>().FirstOrDefault()
                   ?? defaultValue;
        }
    }
}
