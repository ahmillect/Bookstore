using OnlineBookstore.Models;

namespace OnlineBookstore.Services.Users
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetUsers();
        Task<User> GetUserById(string id);
        Task<User> GetUserByUsername(string username);
        Task<User> AddUser(User user);
        Task<User> UpdateUser(string id, User user);
        void DeleteUser(string id);
    }
}
