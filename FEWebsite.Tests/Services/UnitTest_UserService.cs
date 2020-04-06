using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using FEWebsite.API.Data.DerivedServices;
using FEWebsite.API.Helpers;
using FEWebsite.API.Models;
using static FEWebsite.Tests.Helpers.MockEFDatabase;

namespace FEWebsite.Tests.Services
{
    [TestClass]
    public class UnitTest_UserService
    {
        private UserService UserService { get; }

        public UnitTest_UserService()
        {
            this.UserService = new UserService(GetMockDataContext());
        }

        [TestMethod]
        public async Task Test_GetUser()
        {
            var users = await GetMockUsers().ConfigureAwait(false);
            var user = await this.UserService.GetUser(users.First().Id).ConfigureAwait(false);

            Assert.IsNotNull(user);

            user = await this.UserService.GetUser(-1).ConfigureAwait(false);

            Assert.IsNull(user);
        }

        [TestMethod]
        public async Task Test_GetUsers()
        {
            // var users = await GetMockUsers().ConfigureAwait(false);
            var userParams = new UserParams(){
                UserId = 1,
                Likees = true,
                OrderBy = nameof(User.AccountCreated).ToLower(),
                PageNumber = 1,
                PageSize = 2,
                MinAge = 18,
                MaxAge = 99
            };
            var users = await this.UserService.GetUsers(userParams).ConfigureAwait(false);

            Assert.IsNotNull(users);

            // users = await this.UserService.GetUser(-1).ConfigureAwait(false);

            // Assert.IsNull(users);
        }
    }
}
