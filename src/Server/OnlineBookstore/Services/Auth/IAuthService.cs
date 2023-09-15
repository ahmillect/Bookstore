using OnlineBookstore.Models;

namespace OnlineBookstore.Services.Auth
{
    public interface IAuthService
    {
        string GenerateJwt(User user);
        bool ValidateUserCredentials(string username, string password);
    }
}
