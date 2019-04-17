using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Jt.Libra.Application.DirectoryInfo
{
    public interface IDirectoryInfoService : IServices
    {
        Task<string> Tree();
    }
}
