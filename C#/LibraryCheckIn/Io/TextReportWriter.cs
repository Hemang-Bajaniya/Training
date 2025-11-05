using LibraryCheckIn.Extensions;
using LibraryCheckIn.Domain;

namespace LibraryCheckIn.IO
{
    public sealed class TextReportWriter : IReportWriter<Book>
    {
        public void Write(IEnumerable<Book> books, string path)
        {
            var now = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            var total = books.Count();
            var conditionCounts = books.ToConditionCounts();
            var topBooks = books.TopBy(b => b.Condition, 5);

            using var writer = new StreamWriter(path);

            writer.WriteLine($"Daily Summary Report");
            writer.WriteLine($"Processed: {now}");
            writer.WriteLine($"Total Returns: {total}");
            writer.WriteLine();

            writer.WriteLine("Condition Counts:");
            foreach (var kv in conditionCounts)
                writer.WriteLine($"- {kv.Key}: {kv.Value}");

            writer.WriteLine();
            writer.WriteLine("Top 5 Books by Penalty:");
            foreach (var book in topBooks)
                writer.WriteLine($"{book.Title} by {book.Author} - Penalty {(int)book.Condition}");
        }
    }
}
