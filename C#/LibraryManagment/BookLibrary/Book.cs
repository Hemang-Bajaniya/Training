namespace Company.Books
{
    // Represent the status of the books
    public enum BookStatus
    {
        Available,
        Borrowed,
        Archived = -1,
    }

    [Flags]
    public enum BookFeatures
    {
        None = 0,
        Hardcover = 1 << 0,
        Illustrated = 1 << 1,
        SignedCopy = 1 << 2,
        Ebook = 1 << 3,
        AudioBook = 1 << 4,

        Digital = Ebook | AudioBook
    }


    public class Book
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public int AuthorId { get; set; }
        public decimal Price { get; set; }
        public BookStatus Status { get; set; }
        public BookFeatures Features { get; set; } = BookFeatures.None;

        public Book(int id, string title, int authorId, decimal price, BookStatus status, BookFeatures features)
        {
            BookId = id;
            Title = title;
            AuthorId = authorId;
            Price = price;
            Status = status;
            Features = features;
        }

        public override string ToString() => $"{Title} - ₹{Price} - Status: {Status}- Features: {Features} {(Features.HasFlag(BookFeatures.Digital) ? "\nBook available in digital format\n" : "")}";
    }
}
