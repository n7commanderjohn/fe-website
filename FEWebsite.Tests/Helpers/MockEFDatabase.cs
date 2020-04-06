using FEWebsite.API.Data;
using FEWebsite.API.Models;
using Microsoft.EntityFrameworkCore;

namespace FEWebsite.Tests.Helpers
{
    public static class MockEFDatabase
    {
        public const string DOESNTEXIST = "doesn't exist";

        public static DataContext GetMockDataContext()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "fewebsite-test")
                .Options;
            var dataContext = new DataContext(options);
            DBSeeding.SeedUsers(dataContext);

            return dataContext;
        }

        public static string[] GetMockTestPasswords()
        {
            return new string[] { "igud", "pp4pp", "123&)(*&", "hahhaha" };
        }

        public static User[] GetMockUsers()
        {
            return new User[]
            {
                new User() {
                    Username = "iloveeirika6969",
                    Email = "iloveeirika6969@gmail.com"
                },
                new User() {
                    Username = "n7cmdrjohn",
                    Email = "kimgears2@gmail.com"
                },
                new User() {
                    Username = "n7cmdrjohn3",
                    Email = "kimgears3@gmail.com"
                }
            };
        }
    }
}
