using escout.Models;
using escoutTests.Resources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace escout.Controllers.Tests
{
    [TestClass]
    public class UserControllerTests
    {
        private UserController controller;
        private DataContext context;

        [TestInitialize]
        public void Setup()
        {
            context = TestUtils.GetMockContext();
            controller = new UserController(context);
        }

        [TestCleanup]
        public void TearDown()
        {
            context.Database.EnsureDeleted();
        }

        [Ignore]
        [TestMethod]
        public void ResetPasswordTest()
        {
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void ChangePasswordTest()
        {
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void UpdateUserTest()
        {
            var user = TestUtils.AddUserToContext(context);
            user.username = "testuser";
            var result = controller.UpdateUser(user);

            Assert.AreEqual(user.username, context.users.First().username);
            Assert.AreEqual(200, ((StatusCodeResult)result).StatusCode);
        }

        [TestMethod]
        public void RemoveUserTest()
        {
            var user = TestUtils.AddUserToContext(context);
            var result = controller.DeleteUser(user);

            Assert.AreEqual(0, context.users.Count());
            Assert.AreEqual(200, ((StatusCodeResult)result).StatusCode);
        }

        [Ignore]
        [TestMethod]
        public void GetUserTest()
        {
            var user = TestUtils.AddUserToContext(context);
            var result = controller.GetUser();

            Assert.AreEqual(user.username, result.Value.username);
            Assert.AreEqual(200, ((StatusCodeResult)result.Result).StatusCode);
        }

        [Ignore]
        [TestMethod]
        public void GetUsersTest()
        {
            TestUtils.AddUserToContext(context);
            var result = controller.GetUsers(string.Empty);

            Assert.AreEqual(1, result.Value.Count);
            Assert.AreEqual(200, ((StatusCodeResult)result.Result).StatusCode);
        }
    }
}