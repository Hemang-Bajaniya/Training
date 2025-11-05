using System.Data;
using System.Data.Common;
using MySql.Data.MySqlClient;

namespace ShopDB.DB
{
    public class DbContext
    {
        private readonly string _connectionString;

        public DbContext()
        {
            _connectionString = "Server=localhost; User ID=root; Password=root; Database=shopdb";
        }

        public IDbConnection CreateConnection() => new MySqlConnection(_connectionString);
    }
}