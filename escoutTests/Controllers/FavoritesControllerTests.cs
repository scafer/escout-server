using escout.Models;
using escoutTests.Resources;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace escout.Controllers.Tests
{
    [Ignore]
    [TestClass]
    public class FavoritesControllerTests
    {
        private FavoritesController controller;
        private DataContext context;

        [TestInitialize]
        public void Setup()
        {
            context = TestUtils.GetMockContext();
            controller = new FavoritesController(context);
        }

        [TestCleanup]
        public void TearDown()
        {
            context.Database.EnsureDeleted();
        }

        [TestMethod]
        public void FavoritesControllerTest()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void ToogleFavoriteTest()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void GetFavoriteTest()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void GetFavoritesTest()
        {
            Assert.Fail();
        }
    }
}