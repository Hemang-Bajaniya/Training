using LibraryCheckIn.Ingestion;
using LibraryCheckIn.Domain;

public class CsvBookImporter : FileImporter<Book>
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
            string[] lines = File.ReadAllLines(path);
            string[] columns = lines[0].Split(",");

            books = lines.Skip(1).Select((v) => Book.FromCsv(v)).ToList();
        }
        catch (Exception ex)
        {
            System.Console.WriteLine(ex);
            throw;
        }

        return books;
    }
}