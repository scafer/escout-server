using escout.Helpers;
using escout.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace escout.Controllers.GenericObjects
{
    [Authorize]
    [ApiController]
    [Route("api/v1/generic-object")]
    public class ImageController : ControllerBase
    {
        private readonly DataContext context;
        public ImageController(DataContext context) => this.context = context;

        [HttpPost]
        [Route("image")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<Image>> CreateImage(List<Image> image)
        {
            image.ToList().ForEach(i => i.created = Utils.GetDateTime());
            image.ToList().ForEach(i => i.updated = Utils.GetDateTime());
            context.images.AddRange(image);
            context.SaveChanges();
            return image;
        }

        [HttpPut]
        [Route("image")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateImage(Image image)
        {
            try
            {
                image.updated = Utils.GetDateTime();
                context.images.Update(image);
                context.SaveChanges();
                return Ok();
            }
            catch { return BadRequest(); }
        }

        [HttpDelete]
        [Route("image")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DeleteImage(int id)
        {
            try
            {
                var image = context.images.FirstOrDefault(i => i.id == id);
                context.images.Remove(image);
                context.SaveChanges();
                return Ok();
            }
            catch { return BadRequest(); }
        }

        [HttpGet]
        [Route("image")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<Image> GetImage(int id)
        {
            return context.images.FirstOrDefault(i => i.id == id);
        }
    }
}
