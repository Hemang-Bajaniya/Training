// using ShopDapperApp.Repositories;
// using ShopDB.Models;
// using ShopDB.Repo;

// CategoryRepo categoryRepo = new();
// System.Console.WriteLine("\nAll Categories:");
// foreach (var item in categoryRepo.GetAll())
// {
//     System.Console.WriteLine(item);
// }

// // categoryRepo.Add(new Category { Name = "Gadgets" });

// System.Console.WriteLine("\nLatest Categories:");
// foreach (var item in categoryRepo.GetAll())
// {
//     System.Console.WriteLine(item);
// }

// ProductRepo productRepo = new();
// productRepo.Add(new Product { CatId = 100, Name = "Smart Watch", Price = 12.34m });
// System.Console.WriteLine("\nAll Products:");
// foreach (var item in productRepo.GetAll())
// {
//     System.Console.WriteLine(item);
// }

// CustomerRepo customerRepo = new();
// Console.WriteLine("\nCustomers:");
// foreach (var cust in customerRepo.GetAll())
//     Console.WriteLine(cust);

// SaleRepo saleRepo = new();
// Console.WriteLine("\nSales:");
// foreach (var sale in saleRepo.GetAll())
//     System.Console.WriteLine(sale);
