using Jt.Libra.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Jt.Libra.Domain.IRepository
{
    public interface IUserMappingRepository
    {
        Task<UserMapping> GetAsync(string openId, string appId);

        Task<IEnumerable<UserMapping>> GetAsync(long? userId, long? wxUserId);

        Task InsertAsync(UserMapping entity);
    }
}