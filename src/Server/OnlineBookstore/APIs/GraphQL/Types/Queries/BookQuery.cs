using OnlineBookstore.Data;
using OnlineBookstore.Models;

namespace OnlineBookstore.APIs.GraphQL.Types.Queries
{
    [ExtendObjectType(typeof(Query))]
    public class BookQuery
    {
        //[UsePaging]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IEnumerable<Book> GetBooks([Service] DbContext dbContext)
        {
            return dbContext.Books.Find(book => true).ToList();
        }

        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public async Task<Book> GetBookById([ID] string id, [Service] DbContext dbContext) =>
        await dbContext.Books.Find(book => book.Id == id).FirstOrDefaultAsync();

        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public async Task<Book> GetBookByTitle(string title, [Service] DbContext dbContext) =>
        await dbContext.Books.Find(book => book.Title == title).FirstOrDefaultAsync();
    }
}
