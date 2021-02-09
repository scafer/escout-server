﻿using escout.Helpers;
using escout.Models;
using escout.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace escout.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1")]
    public class ImageController : ControllerBase
    {
        private readonly DataContext context;
        public ImageController(DataContext context) => this.context = context;

        [HttpPost]
        [Route("image")]
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
        public ActionResult<Image> GetImage(int id)
        {
            return context.images.FirstOrDefault(i => i.id == id);
        }
    }
}