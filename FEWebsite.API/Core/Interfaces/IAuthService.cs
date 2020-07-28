using System.Threading.Tasks;

using FEWebsite.API.Core.Models;

namespace FEWebsite.API.Core.Interfaces
{
    public interface IAuthService
    {
        Task<User> Register(User user, string password);

        void CreatePasswordHash(User user, string password);

        string CreateUserToken(User authenticatedUser, string appSettingsToken);

        Task<User> Login(string username, string password);

        bool ComparePassword(User user, string password);

        Task<bool> UserNameExists(string username);

        Task<bool> UserNameExistsForAnotherUser(User user);

        Task<bool> EmailExists(string email);

        Task<bool> EmailExistsForAnotherUser(User user);
    }
}
