using HotChocolate.Authorization;
using OnlineBookstore.Data;
using OnlineBookstore.Data.DTOs.Book;
using OnlineBookstore.Models;

namespace OnlineBookstore.APIs.GraphQL.Types.Mutations
{
    [ExtendObjectType(typeof(Mutation))]
    public class BookMutation
    {
        [Authorize]
        public async Task<Book> AddBook(BookInput input, [Service] DbContext dbContext)
        {
            var author = await dbContext.Authors.Find(author => author.Id == input.AuthorId).FirstOrDefaultAsync();

            if (author == null)
                throw new ArgumentException($"Author with ID: {input.AuthorId} not found.", nameof(input.AuthorId));

            var book = new Book
            {
                Title = input.Title,
                AuthorId = input.AuthorId,
                Description = input.Description,
                Price = input.Price,
                PublishedDate = input.PublishedDate
            };

            await dbContext.Books.InsertOneAsync(book);
            return book;
        }

        [Authorize]
        public async Task<Book> UpdateBook(BookUpdateInput input, [Service] DbContext dbContext)
        {
            var filter = Builders<Book>.Filter.Eq(u => u.Id, input.Id);
            var update = Builders<Book>.Update
                    .Set(u => u.Title, input.Title)
                    .Set(u => u.AuthorId, input.AuthorId)
                    .Set(u => u.Description, input.Description)
                    .Set(u => u.Price, input.Price)
                    .Set(u => u.PublishedDate, input.PublishedDate);

            await dbContext.Books.UpdateOneAsync(filter, update);
            return await dbContext.Books.Find(u => u.Id == input.Id).FirstOrDefaultAsync();
        }

        [Authorize]
        public async Task<Book> DeleteBook(string id, [Service] DbContext dbContext)
        {
            var book = dbContext.Books.Find(book => book.Id == id).FirstOrDefault();
            await dbContext.Books.DeleteOneAsync(book => book.Id == id);
            return book;
        }
    }
}
