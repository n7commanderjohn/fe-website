using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using FEWebsite.API.Data.DerivedServices;
using FEWebsite.API.Models;
using static FEWebsite.Tests.Helpers.MockEFDatabase;

namespace FEWebsite.Tests
{
    [TestClass]
    public class UnitTest_AuthService
    {
        private AuthService AuthService { get; }

        public UnitTest_AuthService()
        {
            this.AuthService = new AuthService(GetMockDataContext());
        }

        [TestMethod]
        public void Test_CreateAndComparePassword()
        {
            var user = new User();
            string[] passwords = GetMockTestPasswords();

            foreach (var password in passwords)
            {
                this.AuthService.CreatePasswordHash(user, password);

                Assert.IsTrue(this.AuthService.ComparePassword(user, password));
            }

            foreach (var password in passwords)
            {
                this.AuthService.CreatePasswordHash(user, password);

                Assert.IsFalse(this.AuthService.ComparePassword(user, "shouldfail"));
            }
        }

        [TestMethod]
        public void Test_CreateUserToken()
        {
            var usernames = GetMockUsernames();
            var user = new User() {
                Id = 1,
                Username = usernames[0]
            };

            var token = this.AuthService.CreateUserToken(user, "1fXr*D!iVNF!VK0Ib93@^n$7sq3wPF");

            Assert.IsTrue(token.Length > 0);
        }

        [TestMethod]
        public async Task Test_EmailExists()
        {
            foreach (var email in GetMockExistingEmails())
            {
                Assert.IsTrue(await this.AuthService.EmailExists(email).ConfigureAwait(false));
            }

            Assert.IsFalse(await this.AuthService.EmailExists(DOESNTEXIST).ConfigureAwait(false));
        }

        [TestMethod]
        public async Task Test_EmailExistsForAnotherUser()
        {
            var user = new User() {
                Id = 6969
            };
            foreach (var email in GetMockExistingEmails())
            {
                user.Email = email;
                Assert.IsTrue(await this.AuthService.EmailExistsForAnotherUser(user).ConfigureAwait(false));
            }

            user.Email = DOESNTEXIST;
            Assert.IsFalse(await this.AuthService.EmailExistsForAnotherUser(user).ConfigureAwait(false));
        }

        [TestMethod]
        public async Task Test_UserNameExists()
        {
            foreach (var username in GetMockUsernames())
            {
                Assert.IsTrue(await this.AuthService.UserNameExists(username).ConfigureAwait(false));
            }

            Assert.IsFalse(await this.AuthService.UserNameExists(DOESNTEXIST).ConfigureAwait(false));
        }

        [TestMethod]
        public async Task Test_UserNameExistsForAnotherUser()
        {
            var user = new User() {
                Id = 6969
            };
            foreach (var username in GetMockUsernames())
            {
                user.Username = username;
                Assert.IsTrue(await this.AuthService.UserNameExistsForAnotherUser(user).ConfigureAwait(false));
            }

            user.Username = DOESNTEXIST;
            Assert.IsFalse(await this.AuthService.UserNameExistsForAnotherUser(user).ConfigureAwait(false));
        }
    }
}
