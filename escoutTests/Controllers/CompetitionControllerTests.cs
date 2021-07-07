using escout.Controllers.GameObjects;
using escout.Models.Database;
using escoutTests.Resources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace escout.Controllers.Tests
{
    [TestClass]
    public class CompetitionControllerTests
    {
        private CompetitionController controller;
        private DataContext context;

        [TestInitialize]
        public void Setup()
        {
            context = TestUtils.GetMockContext();
            controller = new CompetitionController(context);
            controller.ControllerContext.HttpContext = TestUtils.SetUserContext(context, 2);
        }

        [TestCleanup]
        public void TearDown()
        {
            context.Database.EnsureDeleted();
        }

        [TestMethod]
        public void CreateCompetitionTest()
        {
            var clubs = new List<Competition> { new() { name = "test" } };
            var result = controller.CreateCompetition(clubs);

            Assert.AreEqual(1, result.Value.Count);
            Assert.AreEqual("test", result.Value.First().name);
        }

        [TestMethod]
        public void UpdateCompetitionTest()
        {
            var competition = TestUtils.AddCompetitionToContext(context);
            competition.edition = "test edition";
            var result = controller.UpdateCompetition(competition);

            Assert.AreEqual(competition.edition, context.competitions.First().edition);
            Assert.AreEqual(200, ((StatusCodeResult)result).StatusCode);
        }

        [TestMethod]
        public void DeleteCompetitionTest()
        {
            TestUtils.AddCompetitionToContext(context);
            var result = controller.DeleteCompetition(context.competitions.First().id);

            Assert.AreEqual(0, context.competitions.Count());
            Assert.AreEqual(200, ((StatusCodeResult)result).StatusCode);
        }

        [TestMethod]
        public void GetCompetitionTest()
        {
            var competition = TestUtils.AddCompetitionToContext(context);
            var result = controller.GetCompetition(context.competitions.First().id);

            Assert.AreEqual(competition.name, result.Value.name);
        }

        [TestMethod]
        public void GetCompetitionsTest()
        {
            TestUtils.AddCompetitionToContext(context);
            var result = controller.GetCompetitions(string.Empty);

            Assert.AreEqual(1, result.Value.Count);
        }

        [TestMethod]
        public void CreateCompetitionBoardTest()
        {
            var competition = TestUtils.AddCompetitionToContext(context);
            var club = TestUtils.AddClubToContext(context);
            var boards = new List<CompetitionBoard> { new() { competitionId = competition.id, clubId = club.id } };
            var result = controller.CreateCompetitionBoard(boards);

            Assert.AreEqual(1, result.Value.Count);
        }

        [TestMethod]
        public void UpdateCompetitionBoardTest()
        {
            var board = TestUtils.AddCompetitionBoardToContext(context);
            board.points = 10;
            var result = controller.UpdateCompetitionBoard(board);

            Assert.AreEqual(board.points, context.competitionBoards.First().points);
            Assert.AreEqual(200, ((StatusCodeResult)result).StatusCode);
        }

        [TestMethod]
        public void DeleteCompetitionBoardTest()
        {
            TestUtils.AddCompetitionBoardToContext(context);
            var result = controller.DeleteCompetitionBoard(context.competitionBoards.First().id);

            Assert.AreEqual(0, context.competitionBoards.Count());
            Assert.AreEqual(200, ((StatusCodeResult)result).StatusCode);
        }

        [TestMethod]
        public void GetCompetitionBoardTest()
        {
            TestUtils.AddCompetitionBoardToContext(context);
            var result = controller.GetCompetitionBoard(context.competitions.First().id);

            Assert.AreEqual(1, result.Value.Count);
        }
    }
}