using System;
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
            // DBSeeding.SeedUsers(this.DataContext);

            this.AuthService = new AuthService(this.DataContext);
        }

        [TestMethod]
        public void Test_CreateAndComparePassword()
        {
            var user = new User();
            string[] pws = {"igud", "pp4pp", "123&)(*&", "hahhaha"};

            foreach (var pw in pws) {
                this.AuthService.CreatePasswordHash(user, pw);
                var isMatch = this.AuthService.ComparePassword(user, pw);

                Assert.IsTrue(isMatch);
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
    }
}
