using OnlineBookstore.Data;
using OnlineBookstore.Data.DTOs.User;
using OnlineBookstore.Models;
using OnlineBookstore.Services.Auth;

public class Queries
{
    // Book Queries //
    //[UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IEnumerable<Book> GetBooks([Service] DbContext dbContext)
    {
        try
        {
            return dbContext.Books.Find(book => true).ToList();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
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

    // User Queries //
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

    // Author Queries //
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

    // Login Query //
    public async Task<string> Login(UserLoginInput input, [Service] IAuthService authService) =>
    await authService.Login(input.Username, input.Password);
}
