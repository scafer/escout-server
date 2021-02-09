using System;
using System.Linq;
using escout.Helpers;
using escout.Models;
using escout.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace escout.Controllers
{
    [ApiController]
    [Route("api/v1")]
    public class AuthenticationController : ControllerBase
    {
        private readonly DataContext context;
        public AuthenticationController(DataContext context) => this.context = context;

        [HttpPost]
        [AllowAnonymous]
        [Route("signIn")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<AuthData> SignIn(User user)
        {
            var account = context.users.FirstOrDefault(u => u.username == user.username.ToLower());

            if (account != null)
            {
                if (TokenService.VerifyPassword(user.password, account.password))
                    return TokenService.GenerateToken(account);
                else
                    return BadRequest("Password is wrong.");
            }
            else
                return BadRequest("Account does not exist.");
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("signUp")]
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
                user.accessLevel = 0;
                user.created = Utils.GetDateTime();
                user.updated = Utils.GetDateTime();
                user.username = user.username.ToLower();
                user.email = user.email.ToLower();
                user.password = TokenService.HashPassword(user.password);
                context.users.Add(user);
                context.SaveChanges();

                if (user.notifications == 1)
                    _ = NotificationHelper.SendEmail(user.email, "Welcome to eScout", "Welcome to eScout " + user.username);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("resetPassword")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult ResetPassword(User user1)
        {
            var user = context.users.FirstOrDefault(u => u.username == user1.username || u.email == user1.email);

            if (user == null)
                return BadRequest("Account does not exist");

            var generatedPassword = Utils.StringGenerator();
            user.updated = Utils.GetDateTime();
            user.password = TokenService.HashPassword(Utils.GenerateSha256String(generatedPassword));
            context.users.Update(user);
            context.SaveChanges();
            _ = NotificationHelper.SendEmail(user.email, "New eScout Password", generatedPassword);
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