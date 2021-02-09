using System.Collections.Generic;
using System.Linq;
using escout.Models;
using escoutTests.Resources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace escout.Controllers.Tests
{
    [TestClass()]
    public class SportControllerTests
    {
        private SportController controller;
        private DataContext context;

        [TestInitialize]
        public void Setup()
        {
            context = TestUtils.GetMockContext();
            controller = new SportController(context);
        }

        [TestCleanup]
        public void TearDown()
        {
            context.Database.EnsureDeleted();
        }

        [TestMethod]
        public void CreateSportTest()
        {
            var sport = new List<Sport> { new() { name = "test" } };
            var result = controller.CreateSport(sport);

            Assert.AreEqual(1, result.Value.Count);
            Assert.AreEqual("test", result.Value.First().name);
        }

        [TestMethod]
        public void UpdateSportTest()
        {
            var sport = TestUtils.AddSportToContext(context);
            sport.name = "test event";
            var result = controller.UpdateSport(sport);

            Assert.AreEqual(sport.name, context.sports.First().name);
            Assert.AreEqual(200, ((StatusCodeResult)result).StatusCode);
        }

        [TestMethod]
        public void DeleteSportTest()
        {
            TestUtils.AddSportToContext(context);
            var result = controller.DeleteSport(context.sports.First().id);

            Assert.AreEqual(0, context.sports.Count());
            Assert.AreEqual(200, ((StatusCodeResult)result).StatusCode);
        }

        [TestMethod]
        public void GetSportTest()
        {
            var sport = TestUtils.AddSportToContext(context);
            var result = controller.GetSport(context.sports.First().id);

            Assert.AreEqual(sport.name, result.Value.name);
        }

        [TestMethod]
        public void GetSportsTest()
        {
            TestUtils.AddSportToContext(context);
            var result = controller.GetSports(string.Empty);

            Assert.AreEqual(1, result.Value.Count);
        }
    }
}