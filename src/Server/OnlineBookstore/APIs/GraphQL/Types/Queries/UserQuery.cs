using OnlineBookstore.Data;
using OnlineBookstore.Models;

namespace OnlineBookstore.APIs.GraphQL.Types.Queries
{
    [ExtendObjectType(typeof(Query))]
    public class UserQuery
    {
        //[UsePaging]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public async Task<IEnumerable<User>> GetUsers([Service] DbContext dbContext) =>
        await dbContext.Users.Find(user => true).ToListAsync();

        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public async Task<User> GetUserById([ID] string id, [Service] DbContext dbContext) =>
        await dbContext.Users.Find(user => user.Id == id).FirstOrDefaultAsync();

        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public async Task<User> GetUserByUsername(string username, [Service] DbContext dbContext) =>
        await dbContext.Users.Find(user => user.Username == username).FirstOrDefaultAsync();
    }
}
