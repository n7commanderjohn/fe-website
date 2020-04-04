using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using FEWebsite.API.Data;
using FEWebsite.API.Data.DerivedServices;
using FEWebsite.API.Models;

namespace FEWebsite.Tests
{
    [TestClass]
    public class UnitTest_AuthService
    {
        private const string DOESNTEXIST = "doesn't exist";

        private AuthService AuthService { get; }
        private DataContext DataContext { get; }

        public UnitTest_AuthService()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "fewebsite-test")
                .Options;
            this.DataContext = new DataContext(options);
            DBSeeding.SeedUsers(this.DataContext);

            this.AuthService = new AuthService(this.DataContext);
        }

        [TestMethod]
        public void Test_CreateAndComparePassword()
        {
            var user = new User();
            string[] passwords = GetTestPasswords();

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
            var usernames = GetUsernames();
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
            foreach (var email in GetExistingEmails())
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
            foreach (var email in GetExistingEmails())
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
            foreach (var username in GetUsernames())
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
            foreach (var username in GetUsernames())
            {
                user.Username = username;
                Assert.IsTrue(await this.AuthService.UserNameExistsForAnotherUser(user).ConfigureAwait(false));
            }

            user.Username = DOESNTEXIST;
            Assert.IsFalse(await this.AuthService.UserNameExistsForAnotherUser(user).ConfigureAwait(false));
        }

        private static string[] GetTestPasswords()
        {
            return new string[] { "igud", "pp4pp", "123&)(*&", "hahhaha" };
        }

        private static string[] GetExistingEmails()
        {
            return new string[] { "iloveeirika6969@gmail.com", "kimgears2@gmail.com", "kimgears3@gmail.com" };
        }

        private static string[] GetUsernames()
        {
            return new string[] { "n7cmdrjohn", "iloveeirika6969", "n7cmdrjohn3" };
        }
    }
}
