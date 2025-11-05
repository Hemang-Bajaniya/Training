using Dapper;
using Org.BouncyCastle.Tls;
using ShopDB.DB;
using ShopDB.Models;

namespace ShopDB.Repo
{
    public class CategoryRepo
    {
        private readonly DbContext _context = new();

        public IEnumerable<Category> GetAll()
        {
            using var conn = _context.CreateConnection();
            string sql = @"select
                        t01f01 as Id,
                        t01f02 as Name,
                        t01f03 as UpdatedOn
                        from catet01";

            return conn.Query<Category>(sql);
        }

        public void Add(Category category)
        {
            using var conn = _context.CreateConnection();
            string sql = "INSERT INTO CATET01 (T01F02) VALUES (@Name)";
            conn.Execute(sql, category);
        }

        public void Update(Category category)
        {
            using var conn = _context.CreateConnection();
            string sql = "UPDATE CATET01 SET T01F02=@Name WHERE T01F01=@Id";
            conn.Execute(sql, category);
        }

        public void Delete(int id)
        {
            using var conn = _context.CreateConnection();
            string sql = "DELETE FROM CATET01 WHERE T01F01=@id";
            conn.Execute(sql, new { id });
        }
    }
}