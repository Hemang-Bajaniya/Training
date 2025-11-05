using System.Data;
using System.Net;
using System.Net.Security;
using LibraryCheckIn.Domain;

namespace LibraryCheckIn.Io
{
    public class BookMapper
    {
        public static List<Book> MapToBooks(DataTable dataTable)
        {
            List<Book> books = new();
            string[] reqFields = { "Id", "Title", "Author", "Condition" };

            foreach (var field in reqFields)
            {
                if (!dataTable.Columns.Contains(field))
                {
                    System.Console.WriteLine("Datatable not in proper form");
                    return books;
                }
            }

            foreach (DataRow row in dataTable.Rows)
            {
                try
                {
                    // int id = row.Field<int>("Id");
                    int id = int.Parse(row["Id"].ToString());
                    string title = row.Field<string>("Title");
                    string author = row.Field<string>("Author");

                    if (!Enum.TryParse(row["Condition"].ToString(), out BookCondition condition))
                    {
                        System.Console.WriteLine($"Invalid condition {row["Condition"]}");
                        continue;
                    }

                    books.Add(new Book(id, title, author, condition));
                }
                catch (System.Exception ex)
                {
                    System.Console.WriteLine($"Exception while mapping to books\n{ex}");
                    throw;
                }
            }

            return books;
        }
    }
}