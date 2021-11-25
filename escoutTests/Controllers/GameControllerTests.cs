using System.Collections.Generic;
using System.Linq;
using escout.Controllers.GameObjects;
using escout.Models.Database;
using escoutTests.Resources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace escout.Controllers.Tests
{
    [TestClass]
    public class GameControllerTests
    {
        private GameController controller;
        private DataContext context;

        [TestInitialize]
        public void Setup()
        {
            context = TestUtils.GetMockContext();
            controller = new GameController(context);
            controller.ControllerContext.HttpContext = TestUtils.SetUserContext(context, 2);
        }

        [TestCleanup]
        public void TearDown()
        {
            context.Database.EnsureDeleted();
        }

        [TestMethod]
        public void CreateGameTest()
        {
            var club1 = TestUtils.AddClubToContext(context);
            var club2 = TestUtils.AddClubToContext(context);

            var game = new List<Game> { new() { homeId = club1.id, visitorId = club2.id } };
            var result = controller.CreateGame(game);

            Assert.AreEqual(1, result.Value.Count);
        }

        [TestMethod]
        public void UpdateGameTest()
        {
            var game = TestUtils.AddGameToContext(context);
            game.type = "test type";
            var result = controller.UpdateGame(game);

            Assert.AreEqual(game.type, context.games.First().type);
            Assert.AreEqual(200, ((StatusCodeResult)result).StatusCode);
        }

        [TestMethod]
        public void DeleteGameTest()
        {
            TestUtils.AddGameToContext(context);
            var result = controller.DeleteGame(context.games.First().id);

            Assert.AreEqual(0, context.games.Count());
            Assert.AreEqual(200, ((StatusCodeResult)result).StatusCode);
        }

        [TestMethod]
        public void GetGameTest()
        {
            var game = TestUtils.AddGameToContext(context);
            var result = controller.GetGame(game.id);

            Assert.AreEqual(game.type, result.Value.type);
        }

        [TestMethod]
        public void GetGamesTest()
        {
            TestUtils.AddGameToContext(context);
            var result = controller.GetGames(string.Empty);

            Assert.AreEqual(1, result.Value.Count);
        }

        [Ignore]
        [TestMethod]
        public void CreateGameEventTest()
        {
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void UpdateGameEventTest()
        {
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void DeleteGameEventTest()
        {
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void GetGameEventTest()
        {
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void GetGameEventsTest()
        {
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void CreateGameAthleteTest()
        {
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void UpdateGameAthleteTest()
        {
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void DeleteGameAthleteTest()
        {
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void GetGameAthletesTest()
        {
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void GetGameUserTest()
        {
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void CreateGameUserTest()
        {
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void UpdateGameUserTest()
        {
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void DeleteGameUserTest()
        {
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void AthleteGameEventsTest()
        {
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void GetGameDataTest()
        {
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void GetGameStatisticsTest()
        {
            Assert.Fail();
        }
    }
}