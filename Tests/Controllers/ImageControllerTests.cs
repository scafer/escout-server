﻿using escout.Controllers.GenericObjects;
using escout.Models.Database;
using Microsoft.AspNetCore.Mvc;
using Tests.Helpers;

namespace Tests.Controllers;

[TestClass]
public class ImageControllerTests
{
    private DataContext context;
    private ImageController controller;

    [TestInitialize]
    public void Setup()
    {
        context = TestUtils.GetMockContext();
        controller = new ImageController(context);
        controller.ControllerContext.HttpContext = TestUtils.SetUserContext(context, 0);
    }

    [TestCleanup]
    public void TearDown()
    {
        context.Database.EnsureDeleted();
    }

    [TestMethod]
    public void CreateImageTest()
    {
        var evt = new List<Image> { new() { imageUrl = "test" } };
        var result = controller.CreateImage(evt);

        Assert.AreEqual(1, result.Value.Count);
        Assert.AreEqual("test", result.Value.First().imageUrl);
    }

    [TestMethod]
    public void UpdateImageTest()
    {
        var image = TestUtils.AddImageToContext(context);
        image.imageUrl = "test image";
        var result = controller.UpdateImage(image);

        Assert.AreEqual(image.imageUrl, context.images.First().imageUrl);
        Assert.AreEqual(200, ((StatusCodeResult)result).StatusCode);
    }

    [TestMethod]
    public void DeleteImageTest()
    {
        TestUtils.AddImageToContext(context);
        var result = controller.DeleteImage(context.images.First().id);

        Assert.AreEqual(0, context.images.Count());
        Assert.AreEqual(200, ((StatusCodeResult)result).StatusCode);
    }

    [TestMethod]
    public void GetImageTest()
    {
        var image = TestUtils.AddImageToContext(context);
        var result = controller.GetImage(context.images.First().id);

        Assert.AreEqual(image.imageUrl, result.Value.imageUrl);
    }
}