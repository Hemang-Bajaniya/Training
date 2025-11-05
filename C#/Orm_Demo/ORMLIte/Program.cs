using ServiceStack.OrmLite;
using ServiceStack.Text;

namespace OrmLiteDemo;

class Program
{
    public static void Main()
    {
        using (var db = DbConnProvider.GetConnection())
        {
            // var category = new Category { Name = "Stationary" };
            // db.Insert(category);

            // var newId = db.LastInsertId();
            // System.Console.WriteLine($"Category with id {newId} added");

            List<Category> categories = db.Select<Category>();
            categories = db.Select<Category>("Select t01f02 from catet01 where t01f02 like 'E%'"); // sql
            categories = db.Select<Category>(c => c.Name.StartsWith("E")); // linq


            foreach (var item in categories)
            {
                System.Console.WriteLine($"{item.Name} {item.UpdatedOn:dd-MMM-yyyy}\n");
            }


            Category cat = db.SingleById<Category>(4); // linq
            System.Console.WriteLine($"Cat Id 4 => {cat.Name} {cat.UpdatedOn:dd-MMM-yyyy}\n");

            db.CreateTable<Employee>(overwrite: false);

            System.Console.WriteLine(db.TableExists<Employee>());

            var q = db.From<Category>()
            .Join<Category, Product>((c, p) => c.Id == p.CategoryId)
            .Join<Product, Sale>((p, s) => p.Id == s.ProductId)
            .GroupBy(c => c.Id)
            .Select<Category, Product, Sale>((c, p, s) => new { CatId = c.Id, Name = c.Name, Total = Sql.Sum(p.Price * s.Quantity) });

            var result = db.Select<CatSalesInfo>(q);

            foreach (var item in result)
            {
                System.Console.WriteLine($"Id#: {item.CatId}, {item.Name} total sales: {item.Total}");
            }

            var products = db.Select<Product>(p => p.Price > 200);

            var sorted = db.Select(db.From<Product>().OrderBy(p => p.Price));

            var paged = db.Select(db.From<Product>().Limit(0, 10)); // first 10 rows
        }
    }
}