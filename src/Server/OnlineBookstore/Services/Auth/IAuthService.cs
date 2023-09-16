using OnlineBookstore.Models;

namespace OnlineBookstore.Services.Auth
{
    public interface IAuthService
    {
        string GenerateJwt(User user);
        Task<string> Login(string username, string password);
        bool ValidateUserCredentials(User user, string password);
    }
}
