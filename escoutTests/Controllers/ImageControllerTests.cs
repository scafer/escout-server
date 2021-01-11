using System.Collections.Generic;
using System.Linq;
using escout.Models;
using escoutTests.Resources;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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

            Assert.AreEqual(0, result.Value.errorCode);
            Assert.AreEqual(image.imageUrl, context.images.First().imageUrl);
        }

        [TestMethod]
        public void DeleteImageTest()
        {
            TestUtils.AddImageToContext(context);
            var result = controller.DeleteImage(context.images.First().id);

            Assert.AreEqual(0, result.Value.errorCode);
            Assert.AreEqual(0, context.images.Count());
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