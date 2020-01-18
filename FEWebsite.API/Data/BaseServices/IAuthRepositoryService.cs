using System.Threading.Tasks;
using FEWebsite.API.Models;

namespace FEWebsite.API.Data.BaseServices
{
    public interface IAuthRepositoryService
    {
         Task<User> Register(User user, string password);

         Task<User> Login(string username, string password);

         Task<bool> UserExists(string username);
    }
}
