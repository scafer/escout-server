using escout.Models;
using escout.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace escout.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpPost]
        [Route("resetPassword")]
        public ActionResult<SvcResult> ResetPassword(User user)
        {
            using var service = new UserService();
            var result = service.ResetPassword(user.username, user.email);

            return result ? SvcResult.Get(0, "Success") : SvcResult.Get(1, "Error");
        }

        [HttpPost]
        [Authorize]
        [Route("changePassword")]
        public ActionResult<SvcResult> ChangePassword(string oldPassword, string newPassword)
        {
            var user = User.GetUser();

            if (user == null)
            {
                return new NotFoundResult();
            }

            using var userService = new UserService();
            var result = userService.ChangePassword(oldPassword, newPassword);

            return result ? SvcResult.Get(0, "Success") : SvcResult.Get(1, "Error");
        }

        [HttpGet]
        [Route("getUserInfo")]
        [Authorize]
        public ActionResult<User> GetUserInfo()
        {
            return User.GetUser();
        }

        [HttpGet]
        [Authorize]
        [Route("getAllUsers")]
        public ActionResult<List<User>> GetAllUsers()
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