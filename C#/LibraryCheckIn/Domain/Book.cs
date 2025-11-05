namespace LibraryCheckIn.Domain
{
    public enum BookCondition
    {
        New = -1, Good = 0, Worn = 3, Damaged = 10
    }

    public class Book
    {
        // Public getters, private setters → enforce immutability outside the class
        public int Id { get; private set; }
        public string Title { get; private set; }
        public string Author { get; private set; }
        public BookCondition Condition { get; private set; }

        public Book(int id, string title, string author, BookCondition condition)
        {
            Id = id;
            Title = title;
            Author = author;
            Condition = condition;
        }


        public override string ToString()
        {
            return $"{Title} by {Author} in {Condition} condition";
        }

        public static Book FromCsv(string csvLine)
        {
            string[] val = csvLine.Split(",");
            try
            {
                int id = Convert.ToInt32(val[0]);
                string title = Convert.ToString(val[1]);
                string author = Convert.ToString(val[2]);
                if (!Enum.TryParse<BookCondition>(val[3], true, out var condition))
                    throw new FormatException($"Invalid BookCondition: '{val[3]}'");

                return new Book(id, title, author, condition);
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}
