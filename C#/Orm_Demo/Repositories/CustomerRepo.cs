using Dapper;
using ShopDB.DB;
using ShopDB.Models;

namespace ShopDapperApp.Repositories
{
    public class CustomerRepo
    {
        private readonly DbContext _context = new();

        public IEnumerable<Customer> GetAll()
        {
            using var conn = _context.CreateConnection();
            string sql = "SELECT T03F01 AS Id, T03F02 AS Name, T03F03 AS Email FROM CUSTT03";
            return conn.Query<Customer>(sql);
        }

        public void Add(Customer Customer)
        {
            using var conn = _context.CreateConnection();
            string sql = "INSERT INTO CUSTT03 (T03F02, T03F03) VALUES (@Name, @Email)";
            conn.Execute(sql, Customer);
        }

        public void Update(Customer Customer)
        {
            using var conn = _context.CreateConnection();
            string sql = "UPDATE CUSTT03 SET T03F02=@Name, T03F03=@Email WHERE T03F01=@Id";
            conn.Execute(sql, Customer);
        }

        public void Delete(int id)
        {
            using var conn = _context.CreateConnection();
            string sql = "DELETE FROM CUSTT03 WHERE T03F01=@id";
            conn.Execute(sql, new { id });
        }
    }
}
