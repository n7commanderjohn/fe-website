using System.Threading.Tasks;
using FEWebsite.API.Models;

namespace FEWebsite.API.Data.BaseServices
{
    public interface IAuthService
    {
         Task<User> Register(User user, string password);

         Task<User> Login(string username, string password);

         bool ComparePassword(User user, string password);

         Task<bool> UserNameExists(string username);

         Task<bool> UserNameExistsForAnotherUser(User user);

         Task<bool> EmailExists(string email);

         Task<bool> EmailExistsForAnotherUser(User user);
    }
}
