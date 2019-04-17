using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace Jt.Libra.Repository
{
    public abstract class JtBaseRepository
    {
        // TODO: get connectString from config
        public static string ConnectionStrnig = "xxx";
        private static IDbConnection connection;

        public JtBaseRepository(string connectionString = "")
        {
            if (connection == null)
            {
                if (string.IsNullOrEmpty(connectionString))
                    connectionString = ConnectionStrnig;
                connection = new MySqlConnection(connectionString);
            }
            if (connection.State != ConnectionState.Open)
                connection.Open();
        }

        public IDbConnection Instance => connection;
    }
}
