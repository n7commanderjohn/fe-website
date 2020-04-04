using FEWebsite.API.Data;
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

        public static string[] GetMockExistingEmails()
        {
            return new string[] { "iloveeirika6969@gmail.com", "kimgears2@gmail.com", "kimgears3@gmail.com" };
        }

        public static string[] GetMockUsernames()
        {
            return new string[] { "n7cmdrjohn", "iloveeirika6969", "n7cmdrjohn3" };
        }
    }
}
