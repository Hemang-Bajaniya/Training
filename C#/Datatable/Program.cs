using System;
using System.Data;

class Program
{
    public static void Main()
    {
        DataSet dataSet = new("ShopDB");

        DataTable customers = new("Customers");

        DataColumn cId = new("CustomerId", typeof(int))
        {
            AutoIncrement = true,
            AutoIncrementSeed = 1,
            AutoIncrementStep = 1,
            ReadOnly = true,
            Unique = true
        };
        customers.Columns.Add(cId);
        customers.Columns.Add("Name", typeof(string));
        customers.Columns.Add("City", typeof(string));
        customers.Columns.Add("Email", typeof(string));

        customers.PrimaryKey = new[] { customers.Columns["CustomerId"] };

        customers.Rows.Add(null, "Ravi", "Baroda", "ravi@gmail.com");
        customers.Rows.Add(null, "Mira", "Ahemdavad", "mir@a@gmail.com");
        customers.Rows.Add(null, "Krunal", "Gandhinagar", "kru12@gmail.com");

        DataTable dt = new("prodt1");
        // DataTable dt1 = new DataTable("MyTable", "sp-1");
        // string name, nspace;
        // name = dt.TableName;
        // nspace = dt1.Namespace;

        // System.Console.WriteLine(name + " " + nspace);
        // System.Console.WriteLine("Ok");

        // Define columns
        DataColumn t1c1 = new("Id", typeof(int))
        {
            AutoIncrement = true,
            AutoIncrementSeed = 100,
            AutoIncrementStep = 1,
            ReadOnly = true,
            Unique = true
        };

        DataColumn t1c2 = new("Name", typeof(string));
        DataColumn t1c3 = new("Price", typeof(decimal)) { DefaultValue = 0m };
        DataColumn t1c4 = new("Quantity", typeof(int)) { DefaultValue = 0 };
        DataColumn t1c5 = new("Total", typeof(decimal), "Price * Quantity");
        DataColumn t1c6 = new("Time", typeof(DateTime)) { DefaultValue = DateTime.Now };

        dt.Columns.Add(t1c1);
        dt.Columns.Add(t1c2);
        dt.Columns.Add(t1c3);
        dt.Columns.Add(t1c4);
        dt.Columns.Add(t1c5);
        dt.Columns.Add(t1c6);
        dt.Columns.Add("CustId", typeof(int)); // foreign

        dt.PrimaryKey = [dt.Columns["Id"]];

        dataSet.Tables.Add(dt);
        dataSet.Tables.Add(customers);

        DataRelation custProdRel = new("CustomerProduct",
        customers.Columns["CustomerId"],
        dt.Columns["CustId"]
        );

        dataSet.Relations.Add(custProdRel);

        // Add rows
        string[] names = { "Calculator", "Washing Machine", "AC" };
        decimal[] prices = { 23.34m, 453.233m, 567.34m };
        int[] quantities = { 3, 2, 1 };

        for (int i = 0; i < names.Length; i++)
        {
            DataRow row = dt.NewRow();
            row["CustId"] = i + 1;
            row["Name"] = names[i];
            row["Price"] = prices[i];
            row["Quantity"] = quantities[i];
            dt.Rows.Add(row);
        }

        DataRow dataRow = dt.NewRow();
        dataRow["CustId"] = 1;
        dataRow["Name"] = "TWS";
        dataRow["Price"] = 12.22m;
        dataRow["Quantity"] = 1;
        dt.Rows.Add(dataRow);

        // foreach (DataRow cust in customers.Rows)
        // {
        //     Console.WriteLine($"\nCustomer {cust["CustomerId"]}: {cust["Name"]} ({cust["City"]}) - {cust["Email"]}");
        //     DataRow[] childRows = cust.GetChildRows("CustomerProduct");

        //     if (childRows.Length == 0)
        //     {
        //         Console.WriteLine("   No products found.");
        //         continue;
        //     }

        //     foreach (DataRow item in childRows)
        //     {
        //         Console.WriteLine($"   {item["Id"]}: {item["Name"]} - Price: {item["Price"]}, Qty: {item["Quantity"]}, Total: {item["Total"]}, Time: {((DateTime)item["Time"]).ToShortDateString()}");
        //     }
        // }

        // Print rows
        // PrintData(dt.Rows);

        // Select and sorting
        // DataRow[] acRows = dt.Select("Name = 'AC'");
        // foreach (var item in acRows)
        // {
        //     item["Price"] = (decimal)item["Price"] - (decimal)item["Price"] * 0.1m;
        // }

        // DataRow[] toBeDel = dt.Select("Price <= 50");
        // foreach (var item in toBeDel)
        // {
        //     item.Delete();
        // }

        // PrintData(dt.Rows);
        // foreach (var item in toBeDel)
        // {
        //     // item.Delete();
        //     // System.Console.WriteLine(item.RowState);
        // }

        DataRow[] expensivProducts = dt.Select("Price > 200", "Price DESC");
        foreach (var item in expensivProducts)
        {
            System.Console.WriteLine($"{item["Name"]} - {item["Price"]}");
        }

        dynamic avg = dt.Compute("AVG(Price)", "");
        dynamic totalExp = dt.Compute("SUM(Price)", "Price >= 200");
        dynamic max = dt.Compute("MAX(Price)", "");

        System.Console.WriteLine($"avg:{avg} max:{max} toatal exp item price:{totalExp}");

        // DataRow[] tobeDel = dt.Select("Price <= 100");
        // foreach (var item in tobeDel)
        // {
        //     item.Delete();
        // }

        foreach (DataRow row in dt.Rows)
        {
            Console.WriteLine($"{row["Name"],-10} -> {row.RowState}");
        }

        dt.AcceptChanges();

        foreach (DataRow row in dt.Rows)
        {
            Console.WriteLine($"{row["Name"],-10} -> {row.RowState}");
        }

        DataTable copy = dt.Copy();
        DataTable clone = dt.Clone();

        clone.Rows.Add(null, "Speaker", 2000m, 4);

        dt.Merge(clone);

        PrintData(copy.Rows);
        PrintData(clone.Rows);
        PrintData(dt.Rows);

        var found = customers.Rows.Find(2);
        found = dt.Rows.Find(102);
        System.Console.WriteLine(found["Name"]);

        clone.ImportRow(found);
        PrintData(clone.Rows);

        var rows = dt.AsEnumerable();

        // Quantifiers
        bool anyExpensive = rows.Any(r => r.Field<decimal>("Price") > 200);
        bool allPositive = rows.All(r => r.Field<decimal>("Price") > 0);
        int count = rows.Count();
        decimal sumTotal = rows.Sum(r => r.Field<decimal>("Total"));

        Console.WriteLine($"\nAny expensive? {anyExpensive}, All positive? {allPositive}, Count={count}, Total={sumTotal}");


        DataView view = new(dt)
        {
            RowFilter = "Price > 200",
            Sort = "Name ASC"
        };

        Console.WriteLine("\nFiltered DataView:");
        foreach (DataRowView drv in view)
            Console.WriteLine($"{drv["Name"]} - {drv["Price"]}");

    }

    public static void PrintData(DataRowCollection dataRowCollection)
    {
        System.Console.WriteLine();
        foreach (DataRow item in dataRowCollection)
        {
            int id = item.Field<int>("Id");
            string name = item.Field<string>("Name");
            decimal price = item.Field<decimal>("Price");
            int quantity = item.Field<int>("Quantity");
            decimal total = item.Field<decimal>("Total");
            DateTime time = item.Field<DateTime>("Time").Date;

            Console.WriteLine($"{id}: {name} - Price: {price}, Qty: {quantity}, Total: {total}, Time: {time:MMM, dd, yyyy}");
        }
    }
}
