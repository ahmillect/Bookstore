using MongoDB.Driver;
using OnlineBookstore.Data;
using OnlineBookstore.Models;

public class Queries
{
    public async Task<IEnumerable<Book>> GetBooks([Service] DbContext dbContext) =>
        await dbContext.Books.Find(book => true).ToListAsync();

    public async Task<Book> GetBookById([ID] string id, [Service] DbContext dbContext) =>
        await dbContext.Books.Find(book => book.Id == id).FirstOrDefaultAsync();

    public async Task<IEnumerable<User>> GetUsers([Service] DbContext dbContext) =>
        await dbContext.Users.Find(user => true).ToListAsync();

    public async Task<User> GetUserById([ID] string id, [Service] DbContext dbContext) =>
        await dbContext.Users.Find(user => user.Id == id).FirstOrDefaultAsync();

    public async Task<User> GetUserByUsername(string username, [Service] DbContext dbContext) =>
        await dbContext.Users.Find(user => user.Username == username).FirstOrDefaultAsync();
}
