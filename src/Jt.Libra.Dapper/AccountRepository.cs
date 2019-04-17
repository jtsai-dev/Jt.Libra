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
    public class AccountRepository : JtBaseRepository, IAccountRepository
    {
        public async Task<Account> GetAsync(string accountName, string password)
        {
            string sql = "SELECT * FROM account WHERE AccountName = @accountName AND Password = @password;";
            var entity = await Instance.QueryFirstOrDefaultAsync<Account>(sql, new { accountName, password });
            return entity;
        }
    }
}
