using escout.Controllers.GenericObjects;
using escout.Models;
using escoutTests.Resources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace escout.Controllers.Tests
{
    [TestClass]
    public class ImageControllerTests
    {
        private ImageController controller;
        private DataContext context;

        [TestInitialize]
        public void Setup()
        {
            context = TestUtils.GetMockContext();
            controller = new ImageController(context);
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
}