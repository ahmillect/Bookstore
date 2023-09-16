using OnlineBookstore.Data;
using OnlineBookstore.Models;

namespace OnlineBookstore.APIs.GraphQL.Types.Queries
{
    [ExtendObjectType(typeof(Query))]
    public class AuthorQuery
    {
        //[UsePaging]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public async Task<IEnumerable<Author>> GetAuthors([Service] DbContext dbContext) =>
        await dbContext.Authors.Find(author => true).ToListAsync();

        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public async Task<Author> GetAuthorById([ID] string id, [Service] DbContext dbContext) =>
        await dbContext.Authors.Find(author => author.Id == id).FirstOrDefaultAsync();

        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public async Task<Author> GetAuthorByName(string name, [Service] DbContext dbContext) =>
        await dbContext.Authors.Find(author => author.Name == name).FirstOrDefaultAsync();
    }
}
