using escout.Models;
using escoutTests.Resources;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace escout.Controllers.Tests
{
    [TestClass()]
    public class AuthenticationControllerTests
    {
        private DataContext db = new DataContext();
        AuthenticationController controller = new AuthenticationController();

        [TestInitialize]
        public void Setup()
        {
            Environment.SetEnvironmentVariable("DATABASE_URL", "postgres://postgres:password@localhost:5432/postgres");
            db.Database.ExecuteSqlRaw(Queries.CreateDatabase);
        }

        [Ignore]
        [TestMethod()]
        public void SignInTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void SignUpTest()
        {
            var user = new User { username = "test", password = "test", email = "test" };
            var result = controller.SignUp(user);

            Assert.IsNotNull(db.users.FirstOrDefault(t => t.id == user.id));
            Assert.AreEqual(result.Value.errorCode, 0);
        }

        [Ignore]
        [TestMethod()]
        public void AuthenticatedTest()
        {
            Assert.Fail();
        }
    }
}