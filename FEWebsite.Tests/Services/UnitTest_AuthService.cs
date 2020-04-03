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
            var user1 = new User();
            string[] pws = {"igud", "pp4pp", "123&)(*&", "hahhaha"};

            foreach (var pw in pws) {
                this.AuthService.CreatePasswordHash(user1, pw);
                var isMatch = this.AuthService.ComparePassword(user1, pw);

                Assert.IsTrue(isMatch);
            }
        }
    }
}
