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
                }
            }
        }
    }
}
