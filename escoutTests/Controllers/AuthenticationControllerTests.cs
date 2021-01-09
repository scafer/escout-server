using escout.Models;
using escoutTests.Resources;
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
        public void SignUpTest()
        {
            var user = new User() { username = "test", email = "test@email.com", password = "test" };

            var result = controller.SignUp(user);
            var userRow = context.users.FirstOrDefault();

            Assert.AreEqual(result.Value.errorCode, 0);
            Assert.AreEqual(user.username, userRow.username);
            Assert.AreEqual(user.email, userRow.email);
        }
    }
}
