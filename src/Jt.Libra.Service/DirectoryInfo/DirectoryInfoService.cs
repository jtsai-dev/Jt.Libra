using Jt.Libra.Application.DirectoryInfo;
using Jt.Libra.Core.Security;
using Jt.Libra.Domain.IRepository;
using Jt.Libra.Infrastructure.Exceptions;
using Jt.Libra.Infrastructure.Extension;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jt.Libra.Application.DirectoryInfo
{
    public class DirectoryInfoService : IDirectoryInfoService
    {
        private IDirectoryInfoRepository _repository;
        public DirectoryInfoService(
            IDirectoryInfoRepository repository)
        {
            _repository = repository;
        }

        public async Task<string> Tree()
        {
            var directories = await _repository.GetListAsync(1);
            return "asd";
        }

        //private List<DirectoryTree> GenerateTree(List<DirectoryInfo> data)
        //{
        //    if (data == null || data.Count < 1)
        //        return null;

        //    var roots = data.GetAndRemove(p => p.ParentId == 0)
        //        .Select(p=>new DirectoryTree() {
        //            Id = p.Id,
        //            Name = p.Name,
        //            Logitude = p.Logitude,
        //            Latitude = p.Latitude,
        //            CreateTime = p.CreateTime,
        //        }).ToList();
        //    foreach (var root in roots)
        //    {
        //        var children = data.GetAndRemove(p => p.ParentId == root.Id)
        //            .Select(p => new DirectoryTree()
        //            {
        //                Id = p.Id,
        //                Name = p.Name,
        //                Logitude = p.Logitude,
        //                Latitude = p.Latitude,
        //                CreateTime = p.CreateTime,
        //            }).ToList();
        //        root.Children = children;
        //    }

        //    return roots;
        //}
    }
}
