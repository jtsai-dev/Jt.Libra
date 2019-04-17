using Jt.Libra.Domain.Entity;
using System;
using System.Collections.Generic;
using Dapper;
using System.Threading.Tasks;
using Jt.Libra.Domain.IRepository;

namespace Jt.Libra.Repository
{
    public class DirectoryInfoRepository : JtBaseRepository, IDirectoryInfoRepository
    {
        public async Task<IEnumerable<DirectoryInfo>> GetListAsync(long accountId)
        {
            string sql = "SELECT * FROM directory -- WHERE AccountId = @accoutnId;";
            var entities = await Instance.QueryAsync<DirectoryInfo>(sql, new { accountId });
            return entities;
        }
    }
}