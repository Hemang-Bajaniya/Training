using System.ComponentModel.Design;
using System.Data;
using MySql.Data.MySqlClient;

public class Program1
{
    public static void Main()
    {
        using (MySqlConnection conn = Program.GetConnection())
        {
            conn.Open();

            MySqlDataAdapter catDa = new("select * from catet01", conn);
            MySqlDataAdapter prodDa = new("select * from prodt02", conn);

            DataSet dataSet = new();
            catDa.Fill(dataSet, "categories");
            prodDa.Fill(dataSet, "products");

            DataRelation dataRelation = new("CustProdRel",
            dataSet.Tables["categories"].Columns["t01f01"],
            dataSet.Tables["products"].Columns[1]);

            dataSet.Relations.Add(dataRelation);

            foreach (DataRow item in dataSet.Tables["categories"].Rows)
            {
                // DataRow[] childRows = item.GetChildRows(dataRelation);
                DataRow[] childRows = item.GetChildRows("CustProdRel");

                System.Console.WriteLine($"\nAll products of cat {item[1]}");

                foreach (var citem in childRows)
                {
                    System.Console.WriteLine($"{citem[2]} with price {citem[3]}");
                }
            }
        }
    }
}