using System.Security.Cryptography.X509Certificates;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Dapper;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;

namespace OrmBenchmark
{
    public class OrmDemo
    {
        public string query = @"SELECT 
                s.T04F01 AS SaleId,
                c.T03F02 AS CustomerName,
                c.T03F03 AS Email,
                p.T02F03 AS ProductName,
                cat.T01F02 AS CategoryName,
                s.T04F04 AS Quantity,
                s.T04F05 AS Total,
                s.T04F06 AS SaleDate
            FROM SALEST04 s
            JOIN CUSTT03 c ON s.T04F03 = c.T03F01
            JOIN PRODT02 p ON s.T04F02 = p.T02F01
            JOIN CATET01 cat ON p.T02F02 = cat.T01F01;";

        [Benchmark]
        public async Task<List<dynamic>> EfCoreQuery()
        {
            using var db = new MyDbContext();
            return await db.sales.Include(s => s.Customer)
            .Include(s => s.Product).ThenInclude(p => p.Category)
            .Select(s => new
            {
                SaleId = s.T04F01,
                CustomerName = s.Customer.T03F02,
                Email = s.Customer.T03F03,
                ProductName = s.Product.T02F03,
                CategoryName = s.Product.Category.T01F02,
                Quantity = s.T04F04,
                Total = s.T04F05,
                SaleDate = s.T04F06
            })
            .ToListAsync<dynamic>();
        }

        [Benchmark]
        public async Task<List<object>> DapperQuery()
        {
            using var conn = new MySqlConnection(MyDbContext.connectionString);
            var data = await conn.QueryAsync<object>(query);
            return data.ToList();
        }

        [Benchmark]
        public async Task<List<dynamic>> AdoNet_Query()
        {
            var result = new List<dynamic>();
            using var conn = new MySqlConnection(MyDbContext.connectionString);
            await conn.OpenAsync();

            using var cmd = new MySqlCommand(query, conn);
            using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                result.Add(new
                {
                    SaleId = reader["SaleId"],
                    CustomerName = reader["CustomerName"],
                    Email = reader["Email"],
                    ProductName = reader["ProductName"],
                    CategoryName = reader["CategoryName"],
                    Quantity = reader["Quantity"],
                    Total = reader["Total"],
                    SaleDate = reader["SaleDate"]
                });
            }

            return result;
        }
    }

    public class Program
    {
        public static void Main()
        {
            var summary = BenchmarkRunner.Run<OrmDemo>();
        }
    }
}
