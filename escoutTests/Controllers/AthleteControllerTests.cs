using escout.Controllers.GameObjects;
using escout.Models;
using escoutTests.Resources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace escout.Controllers.Tests
{
    [TestClass]
    public class AthleteControllerTests
    {
        private AthleteController controller;
        private DataContext context;

        [TestInitialize]
        public void Setup()
        {
            context = TestUtils.GetMockContext();
            controller = new AthleteController(context);
            controller.ControllerContext.HttpContext = TestUtils.SetUserContext(context, 2);
        }

        [TestCleanup]
        public void TearDown()
        {
            context.Database.EnsureDeleted();
        }

        [TestMethod]
        public void CreateAthleteTest()
        {
            var athletes = new List<Athlete> { new() { name = "test" } };
            var result = controller.CreateAthlete(athletes);

            Assert.AreEqual("test", result.Value.First().name);
        }

        [TestMethod]
        public void UpdateAthleteTest()
        {
            var athlete = TestUtils.AddAthleteToContext(context);
            athlete.fullname = "test athlete";
            var result = controller.UpdateAthlete(athlete);

            Assert.AreEqual(athlete.fullname, context.athletes.First().fullname);
            Assert.AreEqual(200, ((StatusCodeResult)result).StatusCode);
        }

        [TestMethod]
        public void RemoveAthleteTest()
        {
            TestUtils.AddAthleteToContext(context);
            var result = controller.RemoveAthlete(context.athletes.First().id);

            Assert.AreEqual(0, context.athletes.Count());
            Assert.AreEqual(200, ((StatusCodeResult)result).StatusCode);
        }

        [TestMethod]
        public void GetAthleteTest()
        {
            var athlete = TestUtils.AddAthleteToContext(context);
            var result = controller.GetAthlete(context.athletes.First().id);

            Assert.AreEqual(athlete.name, result.Value.name);
        }

        [TestMethod]
        public void GetAthletesTest()
        {
            TestUtils.AddAthleteToContext(context);
            var result = controller.GetAthletes(string.Empty);

            Assert.AreEqual(1, result.Value.Count);
        }

        [Ignore]
        [TestMethod]
        public void GetAthleteStatistics()
        {

        }
    }
}