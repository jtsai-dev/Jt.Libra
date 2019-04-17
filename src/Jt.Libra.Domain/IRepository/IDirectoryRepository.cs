using Jt.Libra.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Jt.Libra.Domain.IRepository
{
    public interface IDirectoryInfoRepository
    {
        Task<IEnumerable<DirectoryInfo>> GetListAsync(long accountId);
    }
}
