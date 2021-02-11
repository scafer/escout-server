using escout.Helpers;
using escout.Models;
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
    public class UserController : ControllerBase
    {
        private readonly DataContext context;
        public UserController(DataContext context)
        {
            this.context = context;
        }

        [HttpPost]
        [Route("change-password")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult ChangePassword(string newPassword)
        {
            var user = User.GetUser(context);
            if (user == null)
                return new NotFoundResult();

            try
            {
                user.password = BCrypt.Net.BCrypt.HashPassword(newPassword);
                user.updated = Utils.GetDateTime();
                context.users.Update(user);
                context.SaveChanges();
                return Ok();
            }
            catch { return BadRequest(); }
        }

        [HttpPut]
        [Route("user")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateUser(User user)
        {
            try
            {
                user.updated = Utils.GetDateTime();
                context.users.Update(user);
                context.SaveChanges();
                return Ok();
            }
            catch { return BadRequest(); }
        }

        [HttpDelete]
        [Route("user")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DeleteUser(User user)
        {
            if (!User.GetUser(context).accessLevel.Equals(0))
                return Forbid();

            try
            {
                context.users.Remove(user);
                context.SaveChanges();
                return Ok();
            }
            catch { return BadRequest(); }
        }

        [HttpGet]
        [Route("user")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<User> GetUser()
        {
            return User.GetUser(context);
        }

        [HttpGet]
        [Route("users")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<User>> GetUsers(string query)
        {
            var user = User.GetUser(context);

            if (user == null)
                return new NotFoundResult();
            else if (user.accessLevel == 0)
                return context.users.ToList();
            else
                return new UnauthorizedResult();
        }
    }
}
