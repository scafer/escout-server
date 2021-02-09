using escout.Models;
using escoutTests.Resources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace escout.Controllers.Tests
{
    [TestClass]
    public class EventControllerTests
    {
        private EventController controller;
        private DataContext context;

        [TestInitialize]
        public void Setup()
        {
            context = TestUtils.GetMockContext();
            controller = new EventController(context);
        }

        [TestCleanup]
        public void TearDown()
        {
            context.Database.EnsureDeleted();
        }

        [TestMethod]
        public void CreateEventTest()
        {
            var evt = new List<Event> { new() { name = "test" } };
            var result = controller.CreateEvent(evt);

            Assert.AreEqual(1, result.Value.Count);
            Assert.AreEqual("test", result.Value.First().name);
        }

        [TestMethod]
        public void UpdateEventTest()
        {
            var evt = TestUtils.AddEventToContext(context);
            evt.name = "test event";
            var result = controller.UpdateEvent(evt);

            Assert.AreEqual(evt.name, context.events.First().name);
            Assert.AreEqual(200, ((StatusCodeResult)result).StatusCode);
        }

        [TestMethod]
        public void DeleteEventTest()
        {
            TestUtils.AddEventToContext(context);
            var result = controller.DeleteEvent(context.events.First().id);

            Assert.AreEqual(0, context.events.Count());
            Assert.AreEqual(200, ((StatusCodeResult)result).StatusCode);
        }

        [TestMethod]
        public void GetEventTest()
        {
            var evt = TestUtils.AddEventToContext(context);
            var result = controller.GetEvent(context.events.First().id);

            Assert.AreEqual(evt.name, result.Value.name);
        }

        [TestMethod]
        public void GetEventsTest()
        {
            TestUtils.AddEventToContext(context);
            var result = controller.GetEvents(string.Empty);

            Assert.AreEqual(1, result.Value.Count);
        }
    }
}