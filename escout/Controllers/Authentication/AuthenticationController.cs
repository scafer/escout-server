using System;
using System.Linq;
using escout.Helpers;
using escout.Models;
using escout.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace escout.Controllers.Authentication
{
    [ApiController]
    [Route("api/v1/authentication")]
    public class AuthenticationController : ControllerBase
    {
        private readonly DataContext context;
        public AuthenticationController(DataContext context)
        {
            this.context = context;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("sign-in")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<AuthData> SignIn(User user)
        {
            var account = context.users.FirstOrDefault(u => u.username == user.username.ToLower());

            if (account != null)
            {
                if (account.accessLevel.Equals(-1))
                    return Forbid("Account unauthorized.");
                else if (TokenService.VerifyPassword(user.password, account.password))
                    return TokenService.GenerateToken(account);
                else
                    return BadRequest("Password is wrong.");
            }
            else
                return BadRequest("Account does not exist.");
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("sign-up")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult SignUp(User user)
        {
            if (CheckEmailExist(user.email))
                return BadRequest("Email in use");
            if (CheckUsernameExist(user.username))
                return BadRequest("Username in use");

            try
            {
                user.accessLevel = int.Parse(Configurations.GetDefaultAccessLevel());
                user.created = Utils.GetDateTime();
                user.updated = Utils.GetDateTime();
                user.username = user.username.ToLower();
                user.email = user.email.ToLower();
                user.password = TokenService.HashPassword(user.password);
                context.users.Add(user);
                context.SaveChanges();

                if (user.notifications == 1)
                    _ = Notifications.SendEmail(user.email, "eScout Notification", "Welcome to eScout " + user.username);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("reset-password")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult ResetPassword(User user)
        {
            var userData = context.users.FirstOrDefault(u => u.username == user.username || u.email == user.email);

            if (userData == null)
                return BadRequest("Account does not exist");

            var generatedPassword = Utils.StringGenerator();
            userData.updated = Utils.GetDateTime();
            userData.password = TokenService.HashPassword(Utils.GenerateSha256String(generatedPassword));
            context.users.Update(userData);
            context.SaveChanges();
            _ = Notifications.SendEmail(userData.email, "New eScout Password", generatedPassword);
            return Ok();
        }

        [HttpGet]
        [Authorize]
        [Route("authenticated")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Authenticated()
        {
            return Ok();
        }

        private bool CheckEmailExist(string email)
        {
            var check = context.users.FirstOrDefault(u => u.email == email);
            return check != null;
        }

        private bool CheckUsernameExist(string username)
        {
            var check = context.users.FirstOrDefault(u => u.username == username);
            return check != null;
        }
    }
}