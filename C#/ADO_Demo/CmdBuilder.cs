using System.Data;
using MySql.Data.MySqlClient;

public class Program2
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

            dataSet.Tables["products"].PrimaryKey = [dataSet.Tables["products"].Columns[0]];

            dataSet.Relations.Add(dataRelation);

            // If you used a simple SELECT * FROM catet01 query to fill a DataSet, ADO.NET can automatically build:
            // INSERT command
            // UPDATE command
            // DELETE command

            MySqlCommandBuilder mySqlCommandBuilder = new(prodDa);

            Console.WriteLine("Before Update:");
            foreach (DataRow row in dataSet.Tables["products"].Rows)
            {
                Console.WriteLine($"{row[2]} of price {row[3]}");
                row[3] = (decimal)row[3] + (decimal)row[3] * 0.1m;
            }

            // dataSet.Tables["products"].Rows[1][3] = 160.55m;

            // mysqlcomm

            // prodDa.Update(dataSet.Tables["products"]);


            Console.WriteLine("After ate:");
            foreach (DataRow row in dataSet.Tables["products"].Rows)
                Console.WriteLine($"{row[2]} of price {row[3]}");

        }
    }
}