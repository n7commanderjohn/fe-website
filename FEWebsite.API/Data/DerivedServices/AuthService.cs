using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

using FEWebsite.API.Data.BaseServices;
using FEWebsite.API.Models;

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
                // .Include(p => p.Photos)
                .FirstOrDefaultAsync(u => u.Username == username)
                .ConfigureAwait(false);

            if (!this.ComparePassword(user, password))
            {
                return null;
            }

            user.LastLogin = DateTime.Now;
            await this.Context.SaveChangesAsync().ConfigureAwait(false);

            return user;
        }

        public bool ComparePassword(User user, string password)
        {
            return user != null && VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt);
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            var hMACSHA512 = new System.Security.Cryptography.HMACSHA512(passwordSalt);
            using(var hmac = hMACSHA512)
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

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
            if (user.Name?.Equals("") != false)
            {
                user.Name = user.Username;
            }
            user.Username = user.Username.ToLower();
            user.AccountCreated = DateTime.Now;
            user.LastLogin = DateTime.Now;

            await this.Context.Users.AddAsync(user).ConfigureAwait(false);
            await this.Context.SaveChangesAsync().ConfigureAwait(false);

            return user;
        }

        public void CreatePasswordHash(User user, string password)
        {
            var hMACSHA512 = new System.Security.Cryptography.HMACSHA512();
            using(var hmac = hMACSHA512)
            {
                user.PasswordSalt = hmac.Key;
                user.PasswordHash = hmac.ComputeHash(buffer: System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public string CreateUserToken(User authenticatedUser, string appSettingsToken)
        {
            var claims = new []
            {
                new Claim(ClaimTypes.NameIdentifier, authenticatedUser.Id.ToString()),
                new Claim(ClaimTypes.Name, authenticatedUser.Username),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appSettingsToken));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds,
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public async Task<bool> UserNameExists(string username)
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

        public async Task<bool> UserNameExistsForAnotherUser(User user)
        {
            return await this.Context.Users
                .AnyAsync(u => u.Username == user.Username && u.Id != user.Id)
                .ConfigureAwait(false);
        }

        public async Task<bool> EmailExistsForAnotherUser(User user)
        {
            return await this.Context.Users
                .AnyAsync(u => u.Email == user.Email && u.Id != user.Id)
                .ConfigureAwait(false);
        }
    }
}
