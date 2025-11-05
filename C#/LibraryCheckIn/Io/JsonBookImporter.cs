using LibraryCheckIn.Ingestion;
using LibraryCheckIn.Domain;
using System.Text.Json;
using System.Text.Json.Serialization;

public class JsonBookImporter : FileImporter<Book>
{
    public override IEnumerable<Book> Import(string path)
    {
        IList<Book> books;
        if (!File.Exists(path))
        {
            System.Console.WriteLine($"{path} not exsist");
            return [];
        }

        try
        {
            var op = new JsonSerializerOptions();
            op.Converters.Add(new JsonStringEnumConverter());
            string data = File.ReadAllText(path);
            return JsonSerializer.Deserialize<List<Book>>(data, op);
        }
        catch (System.Exception)
        {
            throw;
        }
    }
}