using OnlineBookstore.APIs.GraphQL.Interfaces;
using OnlineBookstore.Data;
using OnlineBookstore.Data.DTOs.User;
using OnlineBookstore.Models;
using OnlineBookstore.Services.Auth;

namespace OnlineBookstore.APIs.GraphQL.Types.Queries
{
    public class Query
    {
        public async Task<string> Login(UserLoginInput input, [Service] IAuthService authService) =>
        await authService.Login(input.Username, input.Password);

        public async Task<IEnumerable<ISearchResultType>> Search(string term, [Service] DbContext context)
        {
            IEnumerable<Book> books = await context.Books.Find(b => b.Title.Contains(term)).ToListAsync();

            IEnumerable<Author> authors = await context.Authors.Find(a => a.Name.Contains(term)).ToListAsync();

            List<ISearchResultType> results = new List<ISearchResultType>();
            results.AddRange(books);
            results.AddRange(authors);

            return results;
        }
    }
}
