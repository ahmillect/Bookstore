using System.Security.Cryptography;
using System.Text;
using HotChocolate.Authorization;
using OnlineBookstore.Data;
using OnlineBookstore.Data.DTOs.User;
using OnlineBookstore.Models;

namespace OnlineBookstore.APIs.GraphQL.Types.Mutations
{
    [ExtendObjectType(typeof(Mutation))]
    public class UserMutation
    {
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
    }
}
