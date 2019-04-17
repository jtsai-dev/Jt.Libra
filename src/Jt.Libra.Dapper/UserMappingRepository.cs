using Jt.Libra.Domain.Entity;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using System.Threading.Tasks;
using Jt.Libra.Domain.IRepository;

namespace Jt.Libra.Repository
{
    public class UserMappingRepository : JtBaseRepository, IUserMappingRepository
    {
        private readonly string columns = " Id, UserId, WxUserId, OpenId, AppId, CreateTime, CreateBy ";

        public async Task<UserMapping> GetAsync(string openId, string appId)
        {
            string sql = $"SELECT {columns} FROM usermapping WHERE OpenId = @openId AND AppId = @appId;";
            var entity = await Instance.QueryFirstOrDefaultAsync<UserMapping>(sql, new { openId, appId });
            return entity;
        }

        public async Task<IEnumerable<UserMapping>> GetAsync(long? userId, long? wxUserId)
        {
            string sql = $"SELECT {columns} FROM usermapping ";
            if (userId.HasValue)
            {
                string where = " WHERE UserId = @userId;";
                var entities = await Instance.QueryAsync<UserMapping>(sql + where, new { userId });
                return entities;
            }
            else
            {
                string where = " WHERE WxUserId = @wxUserId;";
                var entities = await Instance.QueryAsync<UserMapping>(sql + where, new { wxUserId });
                return entities;
            }
        }

        public async Task InsertAsync(UserMapping entity)
        {
            string sql = @"
INSERT INTO usermapping(UserId, WxUserId, OpenId, AppId, CreateTime, CreateBy) 
VALUE(@UserId, @WxUserId, @OpenId, @AppId, @CreateTime, @CreateBy);";
            await Instance.ExecuteAsync(sql, entity);
        }
    }
}
