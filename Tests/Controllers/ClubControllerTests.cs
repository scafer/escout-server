using escout.Controllers.GameObjects;
using escout.Models.Database;
using Microsoft.AspNetCore.Mvc;
using Tests.Helpers;

namespace Tests.Controllers;

[TestClass]
public class ClubControllerTests
{
    private DataContext context;
    private ClubController controller;

    [TestInitialize]
    public void Setup()
    {
        context = TestUtils.GetMockContext();
        controller = new ClubController(context);
        controller.ControllerContext.HttpContext = TestUtils.SetUserContext(context, 2);
    }

    [TestCleanup]
    public void TearDown()
    {
        context.Database.EnsureDeleted();
    }

    [TestMethod]
    public void CreateClubTest()
    {
        var clubs = new List<Club> { new() { name = "test" } };
        var result = controller.CreateClub(clubs);

        Assert.AreEqual(1, result.Value.Count);
        Assert.AreEqual("test", result.Value.First().name);
    }

    [TestMethod]
    public void UpdateClubTest()
    {
        var athlete = TestUtils.AddClubToContext(context);
        athlete.fullname = "test athlete";
        var result = controller.UpdateClub(athlete);

        Assert.AreEqual(athlete.fullname, context.clubs.First().fullname);
        Assert.AreEqual(200, ((StatusCodeResult)result).StatusCode);
    }

    [TestMethod]
    public void DeleteClubTest()
    {
        TestUtils.AddClubToContext(context);
        var result = controller.DeleteClub(context.clubs.First().id);

        Assert.AreEqual(0, context.clubs.Count());
        Assert.AreEqual(200, ((StatusCodeResult)result).StatusCode);
    }

    [TestMethod]
    public void GetClubTest()
    {
        var club = TestUtils.AddClubToContext(context);
        var result = controller.GetClub(context.clubs.First().id);

        Assert.AreEqual(club.name, result.Value.name);
    }

    [TestMethod]
    public void GetClubsTest()
    {
        TestUtils.AddClubToContext(context);
        var result = controller.GetClubs(string.Empty);

        Assert.AreEqual(1, result.Value.Count);
    }

    [Ignore]
    [TestMethod]
    public void GetAthleteStatisticsTest()
    {
    }
}