namespace LibraryCheckIn.Domain
{
    public class BookProcessor
    {
        public static Dictionary<string, int> CountByCondition(IEnumerable<Book> books)
        {
            Dictionary<string, int> result = new();

            var conditionGroup = books.GroupBy(b => b.Condition);

            foreach (var group in conditionGroup)
            {
                result.Add(group.Key.ToString(), group.Count());
            }

            return result;
        }

        public static void ShowAllBooks(List<Book> books)
        {
            foreach (Book book in books)
                System.Console.WriteLine(book);
        }

        public static IEnumerable<(Book Book, int Penalty)> WithPenalties(IEnumerable<Book> books)
        {
            return books.Select(b => (b, Math.Clamp((int)b.Condition, 0, 100)));
        }

        public static string GetProcessingTimestamp()
        {
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }
    }
}