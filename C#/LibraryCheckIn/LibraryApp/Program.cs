using System.Data;
using LibraryCheckIn.Domain;
using LibraryCheckIn.Ingestion;
using LibraryCheckIn.Io;
using LibraryCheckIn.IO;
using LibraryCheckIn.Extensions;

public class Program
{
    public static void Main(String[] args)
    {
        bool dryRun = args.Contains("--dry-run");

        Console.WriteLine(dryRun ? "Running in DRY-RUN mode" : "Running in NORMAL mode");

        var files = Directory.EnumerateFiles("./In", "*.*", SearchOption.AllDirectories).Where((f) => f.EndsWith(".csv") || f.EndsWith(".json")).ToList();

        if (!files.Any())
        {
            Console.WriteLine("No input files found.");
            return;
        }

        Console.WriteLine($"Found {files.Count} files:");
        foreach (var file in files)
            Console.WriteLine($" - {file}");

        if (dryRun)
        {
            Console.WriteLine("Dry run complete. No data imported.");
            return;
        }


        static void PrintData(dynamic rows)
        {
            foreach (DataRow item in rows)
            {
                foreach (var value in item.ItemArray)
                {
                    System.Console.Write(value?.ToString() + ", ");
                }
                System.Console.WriteLine();
            }
        }

        CsvLoader csvLoader = new();
        DataTable dataTable = csvLoader.LoadIntoDataTable("return_20250929.csv");

        // JsonLoader jsonLoader = new();
        // DataTable dataTable = jsonLoader.LoadIntoDataTable("return_20250929.json");

        System.Console.WriteLine("\nDatatable:");
        PrintData(dataTable.Rows);

        IList<Book> books = BookMapper.MapToBooks(dataTable);

        if (!Directory.Exists("./out"))
        {
            Directory.CreateDirectory("./out");
        }

        string outFile = $@"./out/daily_summary_{DateTime.Now.ToString("yyyyMMdd")}";

        System.Console.WriteLine("\nMapped to books:");
        var counts = BookProcessor.CountByCondition(books);

        Console.WriteLine("Book counts by condition:");
        File.WriteAllText(outFile, "Book counts by condition:\n");
        foreach (var kv in counts)
        {
            File.AppendAllText(outFile, $"{kv.Key}: {kv.Value}\n");
            Console.WriteLine($"{kv.Key}: {kv.Value}");
        }

        var penalties = BookProcessor.WithPenalties(books)
                                     .OrderByDescending(x => x.Penalty)
                                     .Take(5);

        Console.WriteLine("\nTop 5 books by penalty:");
        File.AppendAllText(outFile, "\nTop 5 books by penalty:\n");

        foreach (var (book, penalty) in penalties)
        {
            File.AppendAllText(outFile, $"{book} - Penalty: {penalty}\n");
            Console.WriteLine($"{book} - Penalty: {penalty}");
        }

        string time = BookProcessor.GetProcessingTimestamp();
        Console.WriteLine($"\nProcessed at: {time}");
        File.AppendAllText(outFile, $"\nProcessed at: {time}");

        int count = books.Count();
        System.Console.WriteLine($"\nTotal returns:{count}");
        File.AppendAllText(outFile, $"\nTotal returns:{count}");

        FileImporter<Book> importer;
        importer = new CsvBookImporter();

        foreach (var book in BookProcessor.WithPenalties(importer.Import("return_20250929.csv")))
        {
            System.Console.WriteLine(book);
        }

        importer = new JsonBookImporter();
        foreach (var book in BookProcessor.WithPenalties(importer.Import("return_20250929.json")))
        {
            System.Console.WriteLine(book);
        }

        IReportWriter<Book> writer;

        writer = new TextReportWriter();
        writer.Write(books, "./out/daily_summary.txt");

        writer = new XmlReportWriter();
        writer.Write(books, "./out/daily_summary.xml");

        Console.WriteLine("Reports generated!");

        // Top 2 books by Pen
        var topBooks = books.TopBy(b => b.Condition, 2);
        Console.WriteLine("Top Books by Title Penalties:");
        foreach (var b in topBooks)
            Console.WriteLine($"- {b.Title} ({(int)b.Condition})");

        // Condition counts
        var conditionCounts = books.ToConditionCounts();
        Console.WriteLine("\nCondition Counts:");
        foreach (var kvp in conditionCounts)
            Console.WriteLine($"{kvp.Key}: {kvp.Value}");
    }
}