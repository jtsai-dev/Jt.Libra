using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Jt.Libra.Core.Dependency
{
    public static class IocManager
    {
        public static void Init(
            this IServiceCollection services,
            params string[] assemblyNames)
        {
            foreach (var assemblyName in assemblyNames)
            {
                foreach (var item in GetClassName(assemblyName))
                {
                    foreach (var typeArray in item.Value)
                    {
                        services.AddScoped(typeArray, item.Key);
                    }
                }
            }
        }

        private static Dictionary<Type, Type[]> GetClassName(string assemblyName)
        {
            if (!string.IsNullOrEmpty(assemblyName))
            {
                Assembly assembly = Assembly.Load(assemblyName);
                List<Type> types = assembly.GetTypes().ToList();

                var result = new Dictionary<Type, Type[]>();
                foreach (var item in types.Where(s => !s.IsInterface))
                {
                    var interfaceType = item.GetInterfaces();
                    result.Add(item, interfaceType);
                }
                return result;
            }
            return new Dictionary<Type, Type[]>();
        }
    }
}
