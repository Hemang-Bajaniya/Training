using Dapper;
using ShopDB.DB;
using ShopDB.Models;

namespace ShopDapperApp.Repositories
{
    public class SaleRepo
    {
        private readonly DbContext _context = new();

        public IEnumerable<Sale> GetAll()
        {
            using var conn = _context.CreateConnection();
            string sql = @"SELECT 
                            s.T04F01 AS Id,
                            s.T04F02 AS ProductId,
                            s.T04F03 AS CustomerId,
                            s.T04F04 AS Quantity,
                            s.T04F05 AS Total,
                            s.T04F06 AS SaleDate
                           FROM SaleST04 s";
            return conn.Query<Sale>(sql);
        }

        public void Add(Sale Sale)
        {
            using var conn = _context.CreateConnection();
            string sql = @"INSERT INTO SaleST04 (T04F02, T04F03, T04F04, T04F06)
                           VALUES (@ProductId, @CustomerId, @Quantity, @SaleDate)";
            conn.Execute(sql, Sale);
        }

        public void Delete(int id)
        {
            using var conn = _context.CreateConnection();
            string sql = "DELETE FROM SaleST04 WHERE T04F01=@id";
            conn.Execute(sql, new { id });
        }
    }
}
