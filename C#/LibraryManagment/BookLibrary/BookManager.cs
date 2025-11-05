using System.Collections.Generic;
using System.Linq;

namespace Company.Books
{
    public class BookManager
    {
        private List<Author> authors = new();
        private List<Book> books = new();

        public void AddAuthor(Author author) => authors.Add(author);
        public List<Author> GetAllAuthors() => authors;

        public void AddBook(Book book) => books.Add(book);
        public List<Book> GetAllBooks() => books;
    }
}