using OnlineBookstore.Data;
using OnlineBookstore.Models;

namespace OnlineBookstore.Services.Users
{
    public class UserService : IUserService
    {
        private readonly DbContext _dbContext;

        public UserService(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _dbContext.Users.Find(user => true).ToListAsync();
        }

        public async Task<User> GetUserById(string id)
        {
            var user = await _dbContext.Users.Find(user => user.Id == id).FirstOrDefaultAsync();
            return user;
        }

        public async Task<User> GetUserByUsername(string username)
        {
            var user = await _dbContext.Users.Find(user => user.Username == username).FirstOrDefaultAsync();
            return user;
        }

        public async Task<User> AddUser(User user)
        {
            await _dbContext.Users.InsertOneAsync(user);
            return user;
        }

        public async Task<User> UpdateUser(string id, User user)
        {
            await _dbContext.Users.ReplaceOneAsync(user => user.Id == id, user);
            return user;
        }

        public async void DeleteUser(string id)
        {
            await _dbContext.Users.DeleteOneAsync(user => user.Id == id);
            return;
        }
    }
}
