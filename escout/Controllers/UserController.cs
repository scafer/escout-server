using escout.Helpers;
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
    public class UserController : ControllerBase
    {
        private readonly DataContext context;
        public UserController(DataContext context) => this.context = context;

        [HttpPost]
        [Route("changePassword")]
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
        public IActionResult DeleteUser(User user)
        {
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
        public ActionResult<User> GetUser()
        {
            return User.GetUser(context);
        }

        [HttpGet]
        [Route("users")]
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