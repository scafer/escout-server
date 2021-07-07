using escout.Helpers;
using escout.Models.Database;
using escout.Services;
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
        private readonly DataContext dataContext;
        public ImageController(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        [HttpPost]
        [Route("image")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<Image>> CreateImage(List<Image> image)
        {
            image.ToList().ForEach(i => i.created = GenericUtils.GetDateTime());
            image.ToList().ForEach(i => i.updated = GenericUtils.GetDateTime());
            dataContext.images.AddRange(image);
            dataContext.SaveChanges();
            return image;
        }

        [HttpPut]
        [Route("image")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateImage(Image image)
        {
            if (User.GetUser(dataContext).accessLevel >= ConstValues.AL_USER)
            {
                return Forbid();
            }

            try
            {
                image.updated = GenericUtils.GetDateTime();
                dataContext.images.Update(image);
                dataContext.SaveChanges();
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        [Route("image")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DeleteImage(int id)
        {
            if (User.GetUser(dataContext).accessLevel >= ConstValues.AL_USER)
            {
                return Forbid();
            }

            try
            {
                var image = dataContext.images.FirstOrDefault(i => i.id == id);
                dataContext.images.Remove(image);
                dataContext.SaveChanges();
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("image")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<Image> GetImage(int id)
        {
            return dataContext.images.FirstOrDefault(i => i.id == id);
        }
    }
}
