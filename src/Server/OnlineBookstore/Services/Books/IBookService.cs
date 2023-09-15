using OnlineBookstore.Models;

namespace OnlineBookstore.Services.Books
{
    public interface IBookService
    {
        Task<IEnumerable<Book>> GetBooks();
        Task<Book> GetBookById(string id);
        Task<Book> AddBook(Book book);
        Task<Book> UpdateBook(string id, Book book);
        void DeleteBook(string id);
    }
}
