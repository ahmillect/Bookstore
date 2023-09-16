using OnlineBookstore.Data;
using OnlineBookstore.Models;

namespace OnlineBookstore.Services.Authors
{
    public class AuthorService : IAuthorService
    {
        private readonly DbContext _dbContext;

        public AuthorService(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Author>> GetAuthors()
        {
            return await _dbContext.Authors.Find(author => true).ToListAsync();
        }

        public async Task<Author> GetAuthorById(string id)
        {
            var author = await _dbContext.Authors.Find(author => author.Id == id).FirstOrDefaultAsync();
            return author;
        }

        public async Task<Author> GetAuthorByName(string name)
        {
            var author = await _dbContext.Authors.Find(author => author.Name == name).FirstOrDefaultAsync();
            return author;
        }

        public async Task<Author> AddAuthor(Author author)
        {
            await _dbContext.Authors.InsertOneAsync(author);
            return author;
        }

        public async Task<Author> UpdateAuthor(string id, Author author)
        {
            await _dbContext.Authors.ReplaceOneAsync(author => author.Id == id, author);
            return author;
        }

        public async void DeleteAuthor(string id)
        {
            await _dbContext.Authors.DeleteOneAsync(author => author.Id == id);
            return;
        }
    }
}
