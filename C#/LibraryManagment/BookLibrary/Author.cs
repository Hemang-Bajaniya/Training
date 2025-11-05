namespace Company.Books
{
    public class Author
    {
        private int AuthorId { get; set; }
        private string AuthorName { get; set; }
        private string Country { get; set; }

        public string GetDescription
        {
            get
            {
                return $"Id: {AuthorId}, Name: {AuthorName}, Country: {Country}";
            }
        }

        public Author(int id, string name, string country)
        {
            AuthorId = id;
            AuthorName = name;
            Country = country;
        }

        // public override string ToString() => $"{AuthorName} ({Country})";
    }
}