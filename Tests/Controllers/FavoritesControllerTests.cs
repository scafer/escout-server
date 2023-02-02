using escout.Controllers.GameObjects;
using escout.Models.Database;
using Tests.Helpers;

namespace Tests.Controllers;

[Ignore]
[TestClass]
public class FavoritesControllerTests
{
    private DataContext context;
    private FavoritesController controller;

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