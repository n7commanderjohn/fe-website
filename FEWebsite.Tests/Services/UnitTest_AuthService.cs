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
            string[] pws = {"igud", "pp4pp", "123&)(*&", "hahhaha"};

            foreach (var pw in pws) {
                this.AuthService.CreatePasswordHash(user, pw);

                Assert.IsTrue(this.AuthService.ComparePassword(user, pw));
            }

            foreach (var pw in pws) {
                this.AuthService.CreatePasswordHash(user, pw);

                Assert.IsFalse(this.AuthService.ComparePassword(user, "shouldfail"));
            }
        }

        [TestMethod]
        public void Test_CreateUserToken()
        {
            var user = new User() {
                Id = 1,
                Username = "n7cmdrjohn"
            };

            var token = this.AuthService.CreateUserToken(user, "1fXr*D!iVNF!VK0Ib93@^n$7sq3wPF");

            Assert.IsTrue(token.Length > 0);
        }

        [TestMethod]
        public async Task Test_EmailExists()
        {
            string[] emails = { "iloveeirika6969@gmail.com", "kimgears2@gmail.com", "kimgears3@gmail.com" };
            foreach (var email in emails)
            {
                Assert.IsTrue(await this.AuthService.EmailExists(email).ConfigureAwait(false));
            }

            Assert.IsFalse(await this.AuthService.EmailExists("doesn't exist").ConfigureAwait(false));
        }

        [TestMethod]
        public async Task Test_EmailExistsForAnotherUser()
        {
            string[] emails = { "iloveeirika6969@gmail.com", "kimgears2@gmail.com", "kimgears3@gmail.com" };
            var user = new User() {
                Id = 9999
            };
            foreach (var email in emails)
            {
                user.Email = email;
                Assert.IsTrue(await this.AuthService.EmailExistsForAnotherUser(user).ConfigureAwait(false));
            }

            user.Email = "email doesn't exist";
            Assert.IsFalse(await this.AuthService.EmailExistsForAnotherUser(user).ConfigureAwait(false));
        }
    }
}
