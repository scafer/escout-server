using escout.Models;
using escout.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace escout.Controllers
{
    [Route("api/v1")]
    [ApiController]
    public class UserController : ControllerBase
    {
        /// <summary>
        /// Reset user password.
        /// </summary>
        [HttpPost]
        [Route("resetPassword")]
        public ActionResult<SvcResult> ResetPassword(User user)
        {
            using var service = new UserService();
            var result = service.ResetPassword(user.username, user.email);
            return result ? SvcResult.Set(0, "Success") : SvcResult.Set(1, "Error");
        }

        /// <summary>
        /// Change user password.
        /// </summary>
        [HttpPost]
        [Authorize]
        [Route("changePassword")]
        public ActionResult<SvcResult> ChangePassword(string newPassword)
        {
            var user = User.GetUser();
            if (user == null)
                return new NotFoundResult();

            using var userService = new UserService();
            var result = userService.ChangePassword(user, newPassword);
            return result ? SvcResult.Set(0, "Success") : SvcResult.Set(1, "Error");
        }

        /// <summary>
        /// Update user.
        /// </summary>
        [HttpPut]
        [Authorize]
        [Route("user")]
        public ActionResult<SvcResult> UpdateUser(User user)
        {
            var currentUser = User.GetUser();
            if (currentUser.accessLevel != 0)
                user.id = currentUser.id;

            using var userService = new UserService();
            var result = userService.UpdateUser(user);
            return result ? SvcResult.Set(0, "Success") : SvcResult.Set(1, "Error");
        }

        /// <summary>
        /// Delete user.
        /// </summary>
        [HttpDelete]
        [Authorize]
        [Route("user")]
        public ActionResult<SvcResult> RemoveUser()
        {
            var user = User.GetUser();
            if (user == null)
                return new NotFoundResult();

            using var service = new UserService();
            var result = service.RemoveUser(user);
            return result ? SvcResult.Set(0, "Success") : SvcResult.Set(1, "Error");
        }

        /// <summary>
        /// Get user.
        /// </summary>
        [HttpGet]
        [Route("user")]
        [Authorize]
        public ActionResult<User> GetUser()
        {
            return User.GetUser();
        }

        /// <summary>
        /// Get users.
        /// </summary>
        [HttpGet]
        [Authorize]
        [Route("users")]
        public ActionResult<List<User>> GetUsers(string query)
        {
            var user = User.GetUser();

            if (user == null)
            {
                return new NotFoundResult();
            }

            if (user.accessLevel == 0)
            {
                using var service = new UserService();
                return service.GetUsers();
            }

            return new NotFoundResult();
        }
    }
}