using Company.Books;
using Company.Utils;


BookManager bookManager = new();

bookManager.AddAuthor(new Author(1, "Chetan Bhagat", "India"));
bookManager.AddAuthor(new Author(2, "J.K. Rowling", "UK"));
bookManager.AddAuthor(new Author(3, "George R.R. Martin", "USA"));

bookManager.AddBook(new Book(101, "Five Point Someone", 1, 299, (BookStatus)0, (BookFeatures)1));
bookManager.AddBook(new Book(102, "Harry Potter", 2, 499, (BookStatus)1, BookFeatures.AudioBook | BookFeatures.Ebook));

Console.WriteLine("Enter status for last book:");
var status = Console.ReadLine();
if (Enum.TryParse<BookStatus>(status, out var bookStatus))
{
    bookManager.AddBook(new Book(103, "Game of Thrones", 3, 799, bookStatus, BookFeatures.Hardcover));
}
else
{
    Console.WriteLine("Inavlid Status Value");
}


Printer.PrintList("Authors", bookManager.GetAllAuthors());

Printer.PrintList("Books", bookManager.GetAllBooks());