using Dapper;
using MySql.Data.MySqlClient;
using ShopDB.DB;
using ShopDB.Models;

namespace ShopDB.Repo
{
    public class ProductRepo
    {
        private readonly DbContext _context = new();

        public IEnumerable<Product> GetAll()
        {
            using var conn = _context.CreateConnection();
            string sql = @"SELECT 
                            T02F01 AS Id, 
                            T02F02 AS CatId, 
                            T02F03 AS Name, 
                            T02F04 AS Price 
                           FROM PRODT02";
            return conn.Query<Product>(sql);
        }

        public void Add(Product product)
        {
            try
            {
                using var conn = _context.CreateConnection();
                string sql = @"INSERT INTO PRODT02 (T02F03, T02F02, T02F04)
                               VALUES (@Name, @CatId, @Price)";
                conn.Execute(sql, product);
            }
            catch (MySqlException ex)
            {
                System.Console.WriteLine($"Exception: {ex.Message}");
                // throw;
            }
        }

        public void Update(Product product)
        {
            using var conn = _context.CreateConnection();
            string sql = @"UPDATE PRODT02 
                           SET T02F03=@Name, T02F02=@CatId, T02F04=@Price 
                           WHERE T02F01=@Id";
            // conn.QueryMultiple()
            conn.Execute(sql, product);
        }

        public void Delete(int id)
        {
            using var conn = _context.CreateConnection();
            string sql = "DELETE FROM PRODT02 WHERE T02F01=@id";
            conn.Execute(sql, new { id });
        }
    }
}