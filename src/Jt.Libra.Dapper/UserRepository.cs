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
    public class UserRepository : JtBaseRepository, IUserRepository
    {
        private readonly string columns = " Id, UserName, NickName, RealName, Email, PhoneNumber, Password, Gender, CreateTime, CreateBy, UpdateTime, UpdateBy, DeleteTime, DeleteBy, IsDeleted ";

        public async Task<UserInfo> GetAsync(string phoneNumber, string password)
        {
            string sql = $"SELECT {columns} FROM userinfo WHERE PhoneNumber = @phoneNumber AND Password = @password;";
            var entity = await Instance.QueryFirstOrDefaultAsync<UserInfo>(sql, new { phoneNumber, password });
            return entity;
        }
    }
}
