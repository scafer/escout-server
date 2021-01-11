using escout.Models;
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
            Assert.AreEqual(404, ((StatusCodeResult)result.Result).StatusCode);
        }

        [TestMethod]
        public void SignUpTest()
        {
            var user = new User() { username = "test", email = "test@email.com", password = "test" };
            var result = controller.SignUp(user);
            var userRow = context.users.FirstOrDefault();

            Assert.AreEqual(0, result.Value.errorCode);
            Assert.AreEqual(userRow.username, user.username);
            Assert.AreEqual(userRow.email, user.email);
        }

        [TestMethod]
        public void SignUpTestDuplicatedUsername()
        {
            TestUtils.AddUserToContext(context);
            var result = controller.SignUp(new User() { username = "test", email = "test1@email.com", password = "test" });

            Assert.AreEqual(1, result.Value.errorCode);
            Assert.AreEqual("User in use", result.Value.errorMessage);
        }

        [TestMethod]
        public void SignUpTestDuplicatedEmail()
        {
            TestUtils.AddUserToContext(context);
            var result = controller.SignUp(new User() { username = "test1", email = "test@email.com", password = "test" });

            Assert.AreEqual(1, result.Value.errorCode);
            Assert.AreEqual("Email in use", result.Value.errorMessage);
        }
    }
}