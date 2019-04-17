using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jt.Libra.WebApi
{
    internal static class ActionDescriptorHelper
    {
        public static T GetAttribute<T>(ActionDescriptor actionDescriptor) where T : class
        {
            if (actionDescriptor == null)
            {
                return default(T);
            }

            T wrapAttr;

            //Get for the action
            var t = (actionDescriptor as ControllerActionDescriptor).MethodInfo.GetCustomAttributes(typeof(T), true).FirstOrDefault();
            wrapAttr = t as T;
            if (wrapAttr != null)
            {
                return wrapAttr;
            }

            //Get for the controller
            var controllerAttrs = (actionDescriptor as ControllerActionDescriptor).MethodInfo.GetCustomAttributes(typeof(T), true);
            wrapAttr = controllerAttrs?.FirstOrDefault() as T;
            if (wrapAttr != null)
            {
                return wrapAttr;
            }

            //var wrapAttr = actionDescriptor.Properties.GetOrDefault("__KamoLhpApiDontWrapResultAttribute") as T;
            //if (wrapAttr != null)
            //{
            //    return wrapAttr;
            //}

            //Not found
            return null;
        }
    }
}
