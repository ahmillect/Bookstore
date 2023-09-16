using System.Security.Cryptography;
using System.Text;
using OnlineBookstore.Data.DTOs.Book;
using OnlineBookstore.Data.DTOs.User;
using OnlineBookstore.Data.DTOs.Author;
using OnlineBookstore.Data;
using OnlineBookstore.Models;
using HotChocolate.Authorization;

public class Mutations
{
    // Book Mutations //
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

    // User Mutations //
    public async Task<User> RegisterUser(UserInput input, [Service] DbContext dbContext)
    {
        using var hmac = new HMACSHA512();

        var user = new User
        {
            Username = input.Username,
            Email = input.Email,
            PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(input.Password)),
            PasswordSalt = hmac.Key,
            Books = new List<Book>()
        };

        await dbContext.Users.InsertOneAsync(user);
        return user;
    }

    [Authorize]
    public async Task<User> UpdateUser(UserUpdateInput input, [Service] DbContext dbContext)
    {
        using var hmac = new HMACSHA512();

        var filter = Builders<User>.Filter.Eq(u => u.Id, input.Id);
        var update = Builders<User>.Update
            .Set(u => u.Email, input.Email)
            .Set(u => u.PasswordHash, hmac.ComputeHash(Encoding.UTF8.GetBytes(input.Password)))
            .Set(u => u.PasswordSalt, hmac.Key);

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

    // Author Mutations //
    [Authorize]
    public async Task<Author> AddAuthor(AuthorInput input, [Service] DbContext dbContext)
    {
        var author = new Author
        {
            Name = input.Name,
            Bio = input.Bio,
            Books = new List<Book>()
        };

        await dbContext.Authors.InsertOneAsync(author);
        return author;
    }

    [Authorize]
    public async Task<Author> UpdateAuthor(AuthorUpdateInput input, [Service] DbContext dbContext)
    {
        var filter = Builders<Author>.Filter.Eq(u => u.Id, input.Id);
        var update = Builders<Author>.Update
            .Set(u => u.Name, input.Name)
            .Set(u => u.Bio, input.Bio);

        await dbContext.Authors.UpdateOneAsync(filter, update);
        return await dbContext.Authors.Find(u => u.Id == input.Id).FirstOrDefaultAsync();
    }

    [Authorize]
    public async Task<Author> DeleteAuthor(string id, [Service] DbContext dbContext)
    {
        var author = dbContext.Authors.Find(author => author.Id == id).FirstOrDefault();
        await dbContext.Authors.DeleteOneAsync(author => author.Id == id);
        return author;
    }
}
