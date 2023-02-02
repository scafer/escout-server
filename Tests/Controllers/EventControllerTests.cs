using escout.Controllers.GameObjects;
using escout.Models.Database;
using Microsoft.AspNetCore.Mvc;
using Tests.Helpers;

namespace Tests.Controllers;

[TestClass]
public class EventControllerTests
{
    private DataContext context;
    private EventController controller;

    [TestInitialize]
    public void Setup()
    {
        context = TestUtils.GetMockContext();
        controller = new EventController(context);
        controller.ControllerContext.HttpContext = TestUtils.SetUserContext(context, 0);
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