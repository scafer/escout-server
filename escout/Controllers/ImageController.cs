using escout.Models;
using escout.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace escout.Controllers
{
    [Route("api/v1")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly DataContext context;
        public ImageController(DataContext context) => this.context = context;

        /// <summary>
        /// Create image.
        /// </summary>
        [HttpPost]
        [Authorize]
        [Route("image")]
        public ActionResult<List<Image>> CreateImage(List<Image> image)
        {
            using var service = new ImageService(context);
            return service.CreateImage(image);
        }

        /// <summary>
        /// Update image.
        /// </summary>
        [HttpPut]
        [Authorize]
        [Route("image")]
        public ActionResult<SvcResult> UpdateImage(Image image)
        {
            using var service = new ImageService(context);
            return service.UpdateImage(image) ? SvcResult.Set(0, "Success") : SvcResult.Set(1, "Error");
        }

        /// <summary>
        /// Delete image.
        /// </summary>
        [HttpDelete]
        [Authorize]
        [Route("image")]
        public ActionResult<SvcResult> DeleteImage(int id)
        {
            using var service = new ImageService(context);
            return service.DeleteImage(id) ? SvcResult.Set(0, "Success") : SvcResult.Set(1, "Error");
        }

        /// <summary>
        /// Get image.
        /// </summary>
        [HttpGet]
        [Authorize]
        [Route("image")]
        public ActionResult<Image> GetImage(int id)
        {
            using var service = new ImageService(context);
            return service.GetImage(id);
        }
    }
}