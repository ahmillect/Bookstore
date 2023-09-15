using OnlineBookstore.Data;
using OnlineBookstore.Models;

namespace OnlineBookstore.Services.Books
{
    public class BookService : IBookService
    {
        private readonly DbContext _dbContext;

        public BookService(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Book>> GetBooks()
        {
            return await _dbContext.Books.Find(book => true).ToListAsync();
        }

        public async Task<Book> GetBookById(string id)
        {
            var book = await _dbContext.Books.Find(book => book.Id == id).FirstOrDefaultAsync();
            return book;
        }

        public async Task<Book> AddBook(Book book)
        {
            await _dbContext.Books.InsertOneAsync(book);
            return book;
        }

        public async Task<Book> UpdateBook(string id, Book book)
        {
            await _dbContext.Books.ReplaceOneAsync(book => book.Id == id, book);
            return book;
        }

        public async void DeleteBook(string id)
        {
            await _dbContext.Books.DeleteOneAsync(book => book.Id == id);
            return;
        }
    }
}
