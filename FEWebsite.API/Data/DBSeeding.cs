using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Linq;
using FEWebsite.API.Models;
using Newtonsoft.Json;

namespace FEWebsite.API.Data
{
    public static class DBSeeding
    {
        public static void SeedUsers(DataContext context)
        {
            if (!context.Users.Any())
            {
                var userData = System.IO.File.ReadAllText("Data/UserSeedData.json");
                var users = JsonConvert.DeserializeObject<List<User>>(userData);
                foreach (var user in users)
                {
                    CreatePasswordHash(user, "password");
                    user.Username = user.Username.ToLower();
                    context.Users.Add(user);
                }

                context.SaveChanges();
            }
        }

        private static void CreatePasswordHash(User user, string password)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                user.PasswordSalt = hmac.Key;
                user.PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}
