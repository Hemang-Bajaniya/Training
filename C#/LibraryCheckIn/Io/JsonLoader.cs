using System.Data;
using System.Text.Json;
using LibraryCheckIn.Domain;

namespace LibraryCheckIn.Io;

public class JsonLoader : ILoader
{
    public DataTable LoadIntoDataTable(string fileName)
    {
        DataTable dt = new();

        if (!File.Exists(fileName))
        {
            System.Console.WriteLine($"{fileName} not exsist");
            return dt;
        }

        try
        {
            string data = File.ReadAllText(fileName);
            var books = JsonSerializer.Deserialize<List<BookDto>>(data);

            dt.Columns.Add("Id", typeof(int));
            dt.Columns.Add("Title", typeof(string));
            dt.Columns.Add("Author", typeof(string));
            dt.Columns.Add("Condition", typeof(string));

            foreach (var b in books)
            {
                dt.Rows.Add(b.Id, b.Title, b.Author, b.Condition);
            }
        }
        catch (System.Exception e)
        {
            System.Console.WriteLine($"Error: {e}");
            throw;
        }

        return dt;
    }
}