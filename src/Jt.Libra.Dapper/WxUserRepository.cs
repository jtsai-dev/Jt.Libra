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
    public class WxUserRepository : JtBaseRepository, IWxUserRepository
    {
        private readonly string columns = " Id, UnionId, NickName, Avatar, Gender, Country, Province, City, CreateTime, CreateBy, UpdateTime, UpdateBy ";

        public async Task<WxUserInfo> GetAsync(string unionId)
        {
            string sql = $"SELECT {columns} FROM wxuserinfo WHERE UnionId = @unionId;";
            var entity = await Instance.QueryFirstOrDefaultAsync<WxUserInfo>(sql, new { unionId });
            return entity;
        }

        public async Task<int> InsertAsync(WxUserInfo entity)
        {
            string sql = @"
INSERT INTO wxuserinfo(UnionId, NickName, Avatar, Gender, Country, Province, City, CreateTime, CreateBy) 
VALUE(@UnionId, @NickName, @Avatar, @Gender, @Country, @Province, @City, @CreateTime, @CreateBy);
SELECT @@Identity;";
            var id = await Instance.ExecuteScalarAsync<int>(sql, entity);
            return id;
        }

        public async Task<int> UpdateAsync(WxUserInfo entity)
        {
            string sql = @"
UPDATE wxuserinfo SET 
  UnionId = @UnionId,
  NickName = @NickName,
  Avatar = @Avatar,
  Gender = @Gender,
  Country = @Country,
  Province = @Province,
  City = @City,
  UpdateTime = @UpdateTime,
  UpdateBy = @UpdateBy
WHERE Id = @Id;";
            var id = await Instance.ExecuteAsync(sql, entity);
            return id;
        }
    }
}
