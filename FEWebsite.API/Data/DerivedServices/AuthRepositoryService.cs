using System.Globalization;
using System.Runtime.CompilerServices;
using System;
using System.Threading.Tasks;
using FEWebsite.API.Data.BaseServices;
using FEWebsite.API.Models;

namespace FEWebsite.API.Data.DerivedServices
{
    public class AuthRepositoryService : BaseService, IAuthRepositoryService
    {
        public AuthRepositoryService(DataContext context)
        {
            this.Context = context;
        }

        private DataContext Context { get; }

        public Task<User> Login(string username, string password)
        {
            throw new System.NotImplementedException();
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
            using var hmac = new System.Security.Cryptography.HMACSHA512();

            user.PasswordSalt = hmac.Key;
            user.PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }

        public Task<bool> UserExists(string username)
        {
            throw new System.NotImplementedException();
        }
    }
}