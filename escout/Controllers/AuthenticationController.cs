﻿using escout.Models;
using escout.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace escout.Controllers
{
    [ApiController]
    [Route("api/v1")]
    public class AuthenticationController : ControllerBase
    {
        private readonly DataContext context;
        public AuthenticationController(DataContext context) => this.context = context;

        /// <summary>
        /// Generate authentication token.
        /// </summary>
        [HttpPost]
        [AllowAnonymous]
        [Route("signIn")]
        public ActionResult<AuthData> SignIn(User userData)
        {
            using var service = new AuthService(context);
            var user = service.SignIn(userData);

            if (user != null)
            {
                var token = TokenService.GenerateToken(user);
                return token;
            }

            return new NotFoundResult();
        }

        /// <summary>
        /// Create user.
        /// </summary>
        [HttpPost]
        [AllowAnonymous]
        [Route("signUp")]
        public ActionResult<SvcResult> SignUp(User user)
        {
            using var userService = new UserService(context);
            using var authService = new AuthService(context);

            if (userService.CheckEmailExist(user.email)) return SvcResult.Set(1, "Email in use");
            if (userService.CheckUsernameExist(user.username)) return SvcResult.Set(1, "User in use");
            return authService.SignUp(user) ? SvcResult.Set(0, "Success") : SvcResult.Set(1, "Error while adding user");
        }

        /// <summary>
        /// Test authentication token.
        /// </summary>
        [HttpGet]
        [Authorize]
        [Route("authenticated")]
        public ActionResult<SvcResult> Authenticated()
        {
            return SvcResult.Set(0, "Success");
        }
    }
}