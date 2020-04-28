using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using FEWebsite.API.Data.DerivedServices;
using FEWebsite.API.Helpers;
using FEWebsite.API.Core.Models;
using static FEWebsite.Tests.Helpers.MockEFDatabase;

namespace FEWebsite.Tests.Services
{
    [TestClass]
    public class UnitTest_UserRepoService
    {
        private UserRepoService UserRepoService { get; }

        private static int TestCount { get; set; }

        public UnitTest_UserRepoService()
        {
            this.UserRepoService = new UserRepoService(GetMockDataContext());
        }

        [TestMethod]
        public async Task Test_GetUser()
        {
            var users = await GetMockUsers().ConfigureAwait(false);
            var user = await this.UserRepoService.GetUser(users.First().Id).ConfigureAwait(false);

            Assert.IsNotNull(user);

            user = await this.UserRepoService.GetUser(-1).ConfigureAwait(false);

            Assert.IsNull(user);
        }

        [TestMethod]
        public async Task Test_GetUsers()
        {
            var userParams = new UserParams()
            {
                UserId = 1,
                OrderBy = nameof(User.AccountCreated).ToLower(),
                PageNumber = 1,
                PageSize = 1,
                MinAge = 18,
                MaxAge = 99
            };
            var users = await this.UserRepoService.GetUsers(userParams).ConfigureAwait(false);

            AssertCheckResults(users, userParams);

            userParams.PageSize = 3;
            users = await this.UserRepoService.GetUsers(userParams).ConfigureAwait(false);

            AssertCheckResults(users, userParams, true, 2);

            userParams.Likees = true;
            users = await this.UserRepoService.GetUsers(userParams).ConfigureAwait(false);

            AssertCheckResults(users, userParams, true, 1);

            userParams.Likees = false;
            userParams.Likers = true;
            users = await this.UserRepoService.GetUsers(userParams).ConfigureAwait(false);

            AssertCheckResults(users, userParams, true, 2);

            TestCount = 0;

            static void AssertCheckResults(PagedList<User> users, UserParams userParams, bool useExpectedCount = false, int expectedCount = 0)
            {
                Console.WriteLine();
                Console.WriteLine($"Test Number: {++TestCount}");
                foreach (var user in users)
                {
                    Console.WriteLine($"user.Id: {user.Id}");
                }
                if (useExpectedCount) // use expected count if there is a likee/liker filter
                {
                    Console.WriteLine($"users.Count: {users.Count}, expectedCount: {expectedCount}");
                    Assert.IsTrue(users.Count == expectedCount); // should be x users found based on parameters
                }
                else
                {
                    Console.WriteLine($"users.Count: {users.Count}, userParams.PageSize: {userParams.PageSize}");
                    Assert.IsTrue(users.Count == userParams.PageSize); // should be x users found based on parameters
                }
                Assert.IsFalse(users.Any(u => u.Id == userParams.UserId)); // should not include self in results
            }
        }
    }
}
