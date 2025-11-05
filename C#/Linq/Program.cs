using System.Net.NetworkInformation;

class Product
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Category { get; set; }
    public decimal Price { get; set; }
}

class SalesRecord
{
    public int ProductId { get; set; }
    public int ItemsSold { get; set; }
}

class Program
{
    static void Main()
    {

        // Lang Integarted Query
        // C# 3.0 (.NET Framework 3.5)

        // works on 
        // IEnumerable<T> → for in-memory collections (e.g., List, Array)
        // IQueryable<T> → for external data sources (e.g., databases)

        List<int> nos = [4, 35, 443, 44, 6, 56565, 200, 465, 846, 820];

        IEnumerable<string> strings = ["ab", "Df", "new", "old"];

        System.Console.WriteLine($"{nos.Count}, {strings.Count((ele) => ele.Length > 2)}");

        // Manual

        foreach (var item in nos)
        {
            if (item >= 200)
            {
                System.Console.WriteLine($"{item} is >= 200");
            }
        }

        // op on any coll impl ienumerbale
        // lazy eval
        // defereed exe

        nos.Sort();

        IEnumerable<int> filtered = from no in nos // must
                                    where no >= 200
                                    orderby no descending
                                    select no; // must
        //().ToList(); // for immediate exe

        nos[1] = 500; // chnage later
        foreach (var item in filtered) // deferred exe
        {
            System.Console.WriteLine($"{item} is >= 200");
        }


        IEnumerable<string> filteredVals = from no in nos
                                           where no >= 200
                                           orderby no descending
                                           select $"The val {no} is >= 200";


        foreach (var item in filteredVals)
        {
            System.Console.WriteLine(item);
            // System.Console.WriteLine($"{item} is >= 200");
        }

        // instead of using outsider lang like sql 
        // linq integrated with c#

        List<Product> products = [
            new Product { Id = 100, Name = "Calculator", Price = 23.43m, Category="Stationary" },
            new Product { Id = 101, Name = "Tv", Price = 67.23m, Category="Household" },
            new Product { Id = 103, Name = "Watch", Price = 34.8m, Category="Household" },
            new Product { Id = 103, Name = "SmartFan", Price = 50.8m, Category="Stationary" },
        ];

        List<SalesRecord> salesRecords = [
            new SalesRecord{ProductId=100, ItemsSold=12},
            new SalesRecord{ProductId=103, ItemsSold=34},
            new SalesRecord{ProductId=104, ItemsSold=45},
        ];

        var ExpensiveProducts = from p in products
                                let newPrice = p.Price + (p.Price * p.Price > 50 ? 0.1m : 0.6m)
                                orderby p.Price descending
                                select $"Id:{p.Id} Name:{p.Name} Price:{p.Price} bcz of {newPrice}";

        var SalesItem = from p in products
                        select new { p.Id, p.Name, p.Price, DiscountedPrice = p.Price >= 50 ? (p.Price - p.Price * 0.15m) : (p.Price - 0.056m) };

        foreach (var item in ExpensiveProducts)
        {
            System.Console.WriteLine(item);
        }

        foreach (var item in SalesItem)
        {
            System.Console.WriteLine($"{item.Name} have new Price {item.DiscountedPrice} insted of older {item.Price}");
        }

        var SalesProductsData = from p in products
                                join sr in salesRecords on p.Id equals sr.ProductId
                                select new { Product = p, Recored = sr };

        foreach (var item in SalesProductsData)
        {
            System.Console.WriteLine($"{item.Product.Name} sold {item.Recored.ItemsSold}");
        }

        var CatAvgPrice = from p in products
                          group p by p.Category into c
                          select new { c.Key, AvgPrice = c.Max((p) => p.Price) / c.Count() };
        // select new { c.Key, MaxPrice = c.Max((p) => p.Price) }; // maxPrice

        foreach (var item in CatAvgPrice)
        {
            System.Console.WriteLine($"Category {item.Key} has avg price {item.AvgPrice}");
        }

        System.Console.WriteLine("\n-----Method Syntax-----\n");
        // Method syntax

        var prods = products.Where(p => p.Price > 50).Select(p => $"{p.Price} of {p.Name}"); // dont chnage original one
        foreach (var p in prods)
        {
            System.Console.WriteLine(p);
        }
    }
}
