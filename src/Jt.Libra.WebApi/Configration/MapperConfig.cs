using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Jt.Libra.WebApi.Configration
{
    public class MapperConfig
    {
        public static void Init(params string[] assemblyNames)
        {
            Mapper.Initialize(cfg =>
            {
                cfg.ForAllMaps((typeMap, mappingExpr) =>
                {
                    //// TODO: init mapper
                    //var ignoredPropMaps = typeMap.GetPropertyMaps();
                    //foreach (var map in ignoredPropMaps)
                    //{
                    //    var sourcePropInfo = map.SourceMember as PropertyInfo;
                    //    if (sourcePropInfo == null) continue;
                    //    if (sourcePropInfo.PropertyType != map.DestinationPropertyType)
                    //        map.Ignored = true;
                    //}
                    //mappingExpr.ForAllMembers(opt =>
                    //{
                    //    if (ignoredPropMaps.All(p => opt.DestinationMember.Name != p.SourceMember.Name))
                    //    {
                    //        opt.Ignore();
                    //    }
                    //});
                });
            });
        }
    }
}
