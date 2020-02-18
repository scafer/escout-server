﻿using escout.Models;
using escout.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

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
            {
                return new NotFoundResult();
            }

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

        /// <summary>
        /// Delete user.
        /// </summary>
        [HttpDelete]
        [Authorize]
        [Route("user")]
        public ActionResult<SvcResult> DeleteUser()
        {
            var user = User.GetUser();

            if (user == null)
            {
                return new NotFoundResult();
            }

            using var service = new UserService();
            var result = service.DeleteUser(user);
            return result ? SvcResult.Set(0, "Success") : SvcResult.Set(1, "Error");
        }
    }
}