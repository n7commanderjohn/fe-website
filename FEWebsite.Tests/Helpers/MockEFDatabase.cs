using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using FEWebsite.API.Data;
using FEWebsite.API.Models;

namespace FEWebsite.Tests.Helpers
{
    public static class MockEFDatabase
    {
        public const string DOESNTEXIST = "doesn't exist";

        private static DataContext _dataContext;

        public static DataContext GetMockDataContext()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "fewebsite-test")
                .EnableSensitiveDataLogging()
                .Options;
            var dataContext = new DataContext(options);
            DBSeeding.SeedUsers(dataContext);

            _dataContext = dataContext;
            return dataContext;
        }

        public static string[] GetMockTestPasswords()
        {
            return new string[] { "igud", "pp4pp", "123&)(*&", "hahhaha" };
        }

        public async static Task<IEnumerable<User>> GetMockUsers()
        {
            return await _dataContext.Users.ToListAsync().ConfigureAwait(false);
        }
    }
}
