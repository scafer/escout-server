using escout.Controllers.GenericObjects;
using escout.Models.Database;
using Microsoft.AspNetCore.Mvc;
using Tests.Helpers;

namespace Tests.Controllers;

[TestClass]
public class UserControllerTests
{
    private DataContext context;
    private UserController controller;

    [TestInitialize]
    public void Setup()
    {
        context = TestUtils.GetMockContext();
        controller = new UserController(context);
        controller.ControllerContext.HttpContext = TestUtils.SetUserContext(context, 0);
    }

    [TestCleanup]
    public void TearDown()
    {
        context.Database.EnsureDeleted();
    }

    [Ignore]
    [TestMethod]
    public void ResetPasswordTest()
    {
        Assert.Fail();
    }

    [Ignore]
    [TestMethod]
    public void ChangePasswordTest()
    {
        Assert.Fail();
    }

    [TestMethod]
    public void UpdateUserTest()
    {
        var user = TestUtils.AddUserToContext(context);
        user.username = "testuser";
        var result = controller.UpdateUser(user);

        Assert.IsNotNull(context.users.First(u => u.username == user.username));
        Assert.AreEqual(200, ((StatusCodeResult)result).StatusCode);
    }

    [TestMethod]
    public void RemoveUserTest()
    {
        var user = TestUtils.AddUserToContext(context);
        var result = controller.DeleteUser(user);

        Assert.AreEqual(1, context.users.Count());
        Assert.AreEqual(200, ((StatusCodeResult)result).StatusCode);
    }

    [Ignore]
    [TestMethod]
    public void GetUserTest()
    {
        var user = TestUtils.AddUserToContext(context);
        var result = controller.GetUser();

        Assert.AreEqual(user.username, result.Value.username);
        Assert.AreEqual(200, ((StatusCodeResult)result.Result).StatusCode);
    }

    [Ignore]
    [TestMethod]
    public void GetUsersTest()
    {
        TestUtils.AddUserToContext(context);
        var result = controller.GetUsers(string.Empty);

        Assert.AreEqual(1, result.Value.Count);
        Assert.AreEqual(200, ((StatusCodeResult)result.Result).StatusCode);
    }
}