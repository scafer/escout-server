using escout.Controllers.Authentication;
using escout.Models.Database;
using escoutTests.Resources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace escout.Controllers.Tests
{
    [TestClass]
    public class AuthenticationControllerTests
    {
        private AuthenticationController controller;
        private DataContext context;

        [TestInitialize]
        public void Setup()
        {
            context = TestUtils.GetMockContext();
            controller = new AuthenticationController(context);
        }

        [TestCleanup]
        public void TearDown()
        {
            context.Database.EnsureDeleted();
        }

        [TestMethod]
        public void SignInAuthorized()
        {
            controller.SignUp(new User() { username = "test", email = "test@email.com", password = "test" });
            var result = controller.SignIn(new User() { username = "test", password = "test" });

            Assert.IsNotNull(result.Value.Token);
        }

        [TestMethod]
        public void SignInUnauthorized()
        {
            controller.SignUp(new User() { username = "test", email = "test@email.com", password = "test" });
            var result = controller.SignIn(new User() { username = "test", password = "test1" });

            Assert.IsNull(result.Value);
        }

        [TestMethod]
        public void SignUpTest()
        {
            var user = new User() { username = "test", email = "test@email.com", password = "test" };
            var result = controller.SignUp(user);
            var userRow = context.users.FirstOrDefault();

            Assert.AreEqual(userRow.username, user.username);
            Assert.AreEqual(userRow.email, user.email);
            Assert.AreEqual(200, ((StatusCodeResult)result).StatusCode);
        }

        [Ignore]
        [TestMethod]
        public void SignUpTestDuplicatedUsername()
        {
            TestUtils.AddUserToContext(context);
            var result = controller.SignUp(new User() { username = "test", email = "test1@email.com", password = "test" });
        }

        [Ignore]
        [TestMethod]
        public void SignUpTestDuplicatedEmail()
        {
            TestUtils.AddUserToContext(context);
            var result = controller.SignUp(new User() { username = "test1", email = "test@email.com", password = "test" });
        }
    }
}