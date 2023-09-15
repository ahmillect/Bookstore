using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using OnlineBookstore.Data.DTOs;
using OnlineBookstore.Data;
using OnlineBookstore.Models;

public class Mutations
{
    public async Task<Book> AddBook(Book book, [Service] DbContext dbContext)
    {
        await dbContext.Books.InsertOneAsync(book);
        return book;
    }

    public async Task<Book> UpdateBook(string id, Book book, [Service] DbContext dbContext)
    {
        await dbContext.Books.ReplaceOneAsync(book => book.Id == id, book);
        return book;
    }

    public async Task<Book> DeleteBook(string id, [Service] DbContext dbContext)
    {
        var book = dbContext.Books.Find(book => book.Id == id).FirstOrDefault();
        await dbContext.Books.DeleteOneAsync(book => book.Id == id);
        return book;
    }

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

    public string Login(UserLoginInput input, [Service] DbContext dbContext, [Service] IConfiguration configuration)
    {
        var user = dbContext.Users.Find(u => u.Username == input.Username).FirstOrDefault();

        if (user == null)
            throw new Exception("Invalid username.");

        using var hmac = new HMACSHA512(user.PasswordSalt);

        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(input.Password));

        for (int i = 0; i < computedHash.Length; i++)
        {
            if (computedHash[i] != user.PasswordHash[i])
                throw new Exception("Invalid password.");
        }

        // If we reach here, the user is authenticated. Now, let's generate a JWT.
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(configuration["JwtSettings:Key"]);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

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

    public async Task<User> DeleteUser(string id, [Service] DbContext dbContext)
    {
        var user = dbContext.Users.Find(user => user.Id == id).FirstOrDefault();
        await dbContext.Users.DeleteOneAsync(user => user.Id == id);
        return user;
    }
}
