using escout.Models;
using escoutTests.Resources;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace escout.Controllers.Tests
{
    [TestClass]
    public class ClubControllerTests
    {
        private ClubController controller;
        private DataContext context;

        [TestInitialize]
        public void Setup()
        {
            context = TestUtils.GetMockContext();
            controller = new ClubController(context);
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

            Assert.AreEqual(0, result.Value.errorCode);
            Assert.AreEqual(athlete.fullname, context.clubs.First().fullname);
        }

        [TestMethod]
        public void DeleteClubTest()
        {
            TestUtils.AddClubToContext(context);
            var result = controller.DeleteClub(context.clubs.First().id);

            Assert.AreEqual(0, result.Value.errorCode);
            Assert.AreEqual(0, context.clubs.Count());
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
}