using System;
using System.Threading.Tasks;
using FEWebsite.API.Data.BaseServices;
using FEWebsite.API.Models;
using Microsoft.EntityFrameworkCore;

namespace FEWebsite.API.Data.DerivedServices
{
    public class AuthService : BaseService, IAuthService
    {
        public AuthService(DataContext context)
        {
            this.Context = context;
        }

        private DataContext Context { get; }

        public async Task<User> Login(string username, string password)
        {
            var user = await this.Context.Users
                .Include(p => p.Photos)
                .FirstOrDefaultAsync(u => u.Username == username)
                .ConfigureAwait(false);

            if (user == null
                || !VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                return null;
            }

            user.LastLogin = DateTime.Now;
            await this.Context.SaveChangesAsync().ConfigureAwait(false);

            return user;
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            var hMACSHA512 = new System.Security.Cryptography.HMACSHA512(passwordSalt);
            using (var hmac = hMACSHA512)
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
            if (user.Name == null) {
                user.Name = user.Username;
            }
            user.Username = user.Username.ToLower();
            user.AccountCreated = DateTime.Now;
            user.LastLogin = DateTime.Now;

            await this.Context.Users.AddAsync(user).ConfigureAwait(false);
            await this.Context.SaveChangesAsync().ConfigureAwait(false);

            return user;
        }

        private void CreatePasswordHash(User user, string password)
        {
            var hMACSHA512 = new System.Security.Cryptography.HMACSHA512();
            using (var hmac = hMACSHA512)
            {
                user.PasswordSalt = hmac.Key;
                user.PasswordHash = hmac.ComputeHash(buffer: System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public async Task<bool> UserExists(string username)
        {
            return await this.Context.Users
                .AnyAsync(u => u.Username == username)
                .ConfigureAwait(false);
        }

        public async Task<bool> EmailExists(string email)
        {
            return await this.Context.Users
                .AnyAsync(u => u.Email == email)
                .ConfigureAwait(false);
        }
    }
}
