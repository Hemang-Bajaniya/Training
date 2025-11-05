
using System.Data;
using MySql.Data.MySqlClient;

class Program
{
    public static MySqlConnection GetConnection(string s = "localhost", string db = "shopdb", string u = "root", string p = " root")
    {
        string _connectionString = $"server={s};database={db};user={u};password={p}";
        return new MySqlConnection(_connectionString);
    }

    public class CustomerSalesInfo
    {
        public string Name { get; set; } = "";
        public decimal Total { get; set; }
    }

    public static void Main()
    {
        // Create a connection(SqlConnection)
        // Create a command(SqlCommand)
        // Execute the command(using ExecuteReader, ExecuteScalar, etc.)
        // Retrieve results(through DataReader or DataSet)
        // Close the connection



        // 1.Create a connection(SqlConnection)
        using (MySqlConnection connection = GetConnection()) // auto resource disposal
        {
            try
            {
                //opens a conn
                connection.Open();
                // System.Console.WriteLine($"Mysql Version:{connection.ServerVersion}");
                // System.Console.WriteLine(connection.CanCreateBatch.ToString() + " " + connection.State);

                // connection.Open();

                string query = "Select * from catet01";
                MySqlCommand cmd = new(query, connection);

                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine($"{reader["t01f01"]} - {reader["t01f02"]} - {reader["t01f03"]:dd-MMM-yyyy}");
                }
                reader.Close();

                MySqlDataAdapter da = new("select * from prodt02", connection);
                DataSet ds = new("shopdb");
                da.Fill(ds, "Products");


                DataTable prodTable = ds.Tables["Products"];

                DataRow newRow = prodTable.NewRow();
                newRow["t02f02"] = 1;
                newRow["t02f03"] = "Mouse";
                newRow["t02f04"] = 102.32m;

                prodTable.Rows.Add(newRow);

                MySqlCommandBuilder mySqlCommandBuilder = new(da);

                da.Update(ds, "Products");

                foreach (DataRow item in prodTable.Rows)
                {
                    System.Console.WriteLine($"{item["t02f02"]} - {item["t02f03"]} - {item["t02f04"]}");
                }

                cmd.CommandText = "Select avg(t02f04) from prodt02 group by t02f02";
                dynamic avg = cmd.ExecuteScalar();
                System.Console.WriteLine(avg);

                query = @"SELECT p.t02f03 AS Product, c.t01f02 AS Category, p.t02f04 AS Price
                 FROM prodt02 p
                 JOIN catet01 c ON p.t02f02 = c.t01f01";

                cmd = new MySqlCommand(query, connection);
                MySqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Console.WriteLine($"{dr["Product"]} ({dr["Category"]}) - ₹{dr["Price"]}");
                }
                dr.Close();

                query = @"select s.t04f01 as id, c.t03f02 as cname, 
                p.t02f03 as pname, s.t04f04 as qty,
                p.t02f04 as pprice,
                 s.t04f05 as total, s.t04f06 as date
                 from SALEST04 as s
                 join
                 CUSTT03 as c
                 on s.t04f03 = c.t03f01
                 join
                 prodt02 as p
                on
                s.t04f02 = p.t02f01";

                cmd = new MySqlCommand(query, connection);
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Console.WriteLine($"Order#{dr["id"]} of {dr["cname"]} for {dr["pname"]} of qty {dr["qty"]} * {dr["pprice"]} = total - ₹{dr["total"]} on {dr["date"]:dd-MMM-yyyy}");
                }
                dr.Close();

                da = new MySqlDataAdapter("select t03f01 as id, t03f02 as name from custt03", connection);
                da.Fill(ds, "Customers");
                DataTable custTable = ds.Tables["Customers"];

                custTable.PrimaryKey = [custTable.Columns["id"]];

                newRow = custTable.NewRow();

                newRow["name"] = "Fox Walt";
                newRow["id"] = 4;

                custTable.Rows.Add(newRow);

                mySqlCommandBuilder.DataAdapter = da;

                da.Update(custTable);

                AddCustomer(Name: "Alex", Email: "alex@gmail.com");

                DataRow del = custTable.Rows.Find(5);
                del.Delete();

                mySqlCommandBuilder = new(da);

                da.Update(custTable);



                foreach (DataRow item in custTable.Rows)
                {
                    System.Console.WriteLine($"Id: {item["id"]}, {item["name"]}");
                }

                // customer total purchace

                query = @"select c.t03f02 as name, avg(s.t04f05) as total 
                from salest04 s
                join
                custt03 c 
                on c.t03f01 = s.t04f03
                group by s.t04f03
                ";

                List<CustomerSalesInfo> customerSalesInfos = [];
                using (MySqlCommand c = new(query, connection))
                using (MySqlDataReader custSaleRd = c.ExecuteReader())
                {
                    while (custSaleRd.Read())
                    {
                        customerSalesInfos.Add(new CustomerSalesInfo { Name = custSaleRd.GetString("name"), Total = custSaleRd.GetDecimal("total") });
                    }
                }

                foreach (var item in customerSalesInfos)
                {
                    System.Console.WriteLine($"{item.Name} {item.Total}");
                }




                // connection.Close();
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine("Error: " + ex.Message);
                throw;
            }

        }
    }

    public static void AddCustomer(string Name, string Email = "")
    {
        using (MySqlConnection conn = GetConnection())
        {
            string insert = "insert into custt03(t03f02, t03f03) values (@name, @email)";
            MySqlCommand cmd = new(insert, conn);

            cmd.Parameters.AddWithValue("name", Name);
            cmd.Parameters.AddWithValue("email", Email);

            conn.Open();

            var nor = cmd.ExecuteNonQuery();

            System.Console.WriteLine($"{nor} rows added");

        }
    }
}