using escout.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace escout.Controllers
{
    [Route("api/v1")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        /// <summary>
        /// Create image.
        /// </summary>
        [HttpPost]
        [Authorize]
        [Route("image")]
        public ActionResult<SvcResult> AddImage(Image image)
        {
            return new NotFoundResult();
        }

        /// <summary>
        /// Update image.
        /// </summary>
        [HttpPut]
        [Authorize]
        [Route("image")]
        public ActionResult<SvcResult> UpdateImage(Image image)
        {
            return new NotFoundResult();
        }

        /// <summary>
        /// Delete image.
        /// </summary>
        [HttpDelete]
        [Authorize]
        [Route("image")]
        public ActionResult<SvcResult> DeleteImage(int id)
        {
            return new NotFoundResult();
        }

        /// <summary>
        /// Get image.
        /// </summary>
        [HttpGet]
        [Authorize]
        [Route("image")]
        public ActionResult<Image> GetImage(int id)
        {
            return new NotFoundResult();
        }
    }
}