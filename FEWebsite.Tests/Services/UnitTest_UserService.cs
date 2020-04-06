using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using FEWebsite.API.Data.DerivedServices;
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
            var users = GetMockUsers();
            var user = await this.UserService.GetUser(users[0].Id).ConfigureAwait(false);

            Assert.IsNotNull(user);

            user = await this.UserService.GetUser(-1).ConfigureAwait(false);

            Assert.IsNull(user);
        }
    }
}
