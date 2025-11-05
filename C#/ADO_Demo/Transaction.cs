using System.ComponentModel.Design;
using System.Data;
using MySql.Data.MySqlClient;

public class Program3
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

            MySqlTransaction transaction = conn.BeginTransaction();

            try
            {
                MySqlCommand cmd1 = new MySqlCommand(
                    "INSERT INTO prodt02 (t02f02, t02f03, t02f04) VALUES (1, 'Smart watch', 232.00)", conn, transaction);
                cmd1.ExecuteNonQuery();

                System.Console.WriteLine("Added watch");

                // throw new Exception("Intended one");

                MySqlCommand cmd2 = new MySqlCommand(
                    "INSERT INTO salest04 (t04f02, t04f03, t04f04, t04f06) VALUES (LAST_INSERT_ID(), 1, 1, CURDATE())",
                    conn, transaction);
                cmd2.ExecuteNonQuery();

                transaction.Commit();
                Console.WriteLine("Transaction completed");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Transaction failed: {ex.Message}");
                transaction.Rollback();
            }
        }
    }
}