using System.Security.Cryptography;
using System.Text;
using OnlineBookstore.Data.DTOs;
using OnlineBookstore.Data;
using OnlineBookstore.Models;
using OnlineBookstore.Services.Auth;
using HotChocolate.Authorization;

public class Mutations
{
    // Book Mutations
    [Authorize]
    public async Task<Book> AddBook(Book book, [Service] DbContext dbContext)
    {
        await dbContext.Books.InsertOneAsync(book);
        return book;
    }

    [Authorize]
    public async Task<Book> UpdateBook(string id, Book book, [Service] DbContext dbContext)
    {
        await dbContext.Books.ReplaceOneAsync(book => book.Id == id, book);
        return book;
    }

    [Authorize]
    public async Task<Book> DeleteBook(string id, [Service] DbContext dbContext)
    {
        var book = dbContext.Books.Find(book => book.Id == id).FirstOrDefault();
        await dbContext.Books.DeleteOneAsync(book => book.Id == id);
        return book;
    }

    // User Mutations
    public async Task<User> RegisterUser(UserInput input, [Service] DbContext dbContext)
    {
        using var hmac = new HMACSHA512();

        var user = new User
        {
            Username = input.Username,
            Email = input.Email,
            PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(input.Password)),
            PasswordSalt = hmac.Key
        };

        await dbContext.Users.InsertOneAsync(user);
        return user;
    }

    public string Login(UserLoginInput input, [Service] IAuthService authService, [Service] DbContext dbContext)
    {
        if (!authService.ValidateUserCredentials(input.Username, input.Password))
            throw new Exception("Invalid username or password.");

        var user = dbContext.Users.Find(user => user.Username == input.Username).FirstOrDefault();

        if (user == null)
            throw new Exception("User not found.");

        return authService.GenerateJwt(user);
    }

    [Authorize]
    public async Task<User> UpdateUser(UserUpdateInput input, [Service] DbContext dbContext)
    {
        var filter = Builders<User>.Filter.Eq(u => u.Id, input.Id);
        var update = Builders<User>.Update
            .Set(u => u.Username, input.Username)
            .Set(u => u.Email, input.Email)
            .Set(u => u.PasswordHash, input.PasswordHash)
            .Set(u => u.PasswordSalt, input.PasswordSalt);

        await dbContext.Users.UpdateOneAsync(filter, update);
        return await dbContext.Users.Find(u => u.Id == input.Id).FirstOrDefaultAsync();
    }

    [Authorize]
    public async Task<User> DeleteUser(string id, [Service] DbContext dbContext)
    {
        var user = dbContext.Users.Find(user => user.Id == id).FirstOrDefault();
        await dbContext.Users.DeleteOneAsync(user => user.Id == id);
        return user;
    }
}
