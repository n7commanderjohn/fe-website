using System.Threading.Tasks;
using FEWebsite.API.Data.BaseServices;
using FEWebsite.API.Models;
using Microsoft.EntityFrameworkCore;

namespace FEWebsite.API.Data.DerivedServices
{
    public class AuthRepositoryService : BaseService, IAuthRepositoryService
    {
        public AuthRepositoryService(DataContext context)
        {
            this.Context = context;
        }

        private DataContext Context { get; }

        public async Task<User> Login(string username, string password)
        {
            var user = await this.Context.Users.FirstOrDefaultAsync(u => u.Username == username).ConfigureAwait(false);

            if (user == null)
            {
                return null;
            }

            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                return null;
            }

            return user;
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i])
                    {
                        return false;
                    }
                }

                return true;
            }
        }

        public async Task<User> Register(User user, string password)
        {
            this.CreatePasswordHash(user, password);

            await this.Context.Users.AddAsync(user).ConfigureAwait(false);
            await Context.SaveChangesAsync().ConfigureAwait(false);

            return user;
        }

        private void CreatePasswordHash(User user, string password)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                user.PasswordSalt = hmac.Key;
                user.PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public async Task<bool> UserExists(string username)
        {
            return await this.Context.Users.AnyAsync(u => u.Username == username).ConfigureAwait(false);
        }
    }
}
