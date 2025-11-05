using System;
using System.Data;

public class Program
{
    public static void Main()
    {
        DataTable oldTable = new();
        oldTable.Columns.Add("Id", typeof(int));
        oldTable.Columns.Add("Name", typeof(string));
        oldTable.Columns.Add("Price", typeof(decimal));

        oldTable.Rows.Add(1, "Laptop", 1200.00m);
        oldTable.Rows.Add(2, "Chair", 150.00m);

        DataTable newTable = oldTable.Copy();
        newTable.Rows[0]["Price"] = 1300.00m;  
        newTable.Rows[1]["Name"] = "Wooden Chair"; 

        IDataTableComparer comparer = new DataTableComparer();
        var differences = comparer.Compare(oldTable, newTable);

        Console.WriteLine("Differences Found:");
        foreach (var diff in differences)
        {
            Console.WriteLine(diff);
        }
    }
}
