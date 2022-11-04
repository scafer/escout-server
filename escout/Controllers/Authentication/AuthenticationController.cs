using escout.Helpers;
using escout.Models.Database;
using escout.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace escout.Controllers.Authentication
{
    [ApiController]
    [Route("api/v1/authentication")]
    public class AuthenticationController : ControllerBase
    {
        private readonly DataContext dataContext;
        public AuthenticationController(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("sign-in")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<AuthData> SignIn(User user)
        {
            var account = dataContext.users.FirstOrDefault(u => u.username == user.username.ToLower());

            if (account != null)
            {
                if (account.accessLevel.Equals(-1))
                {
                    return Forbid(ConstValues.MSG_ACCOUNT_UNAUTHORIZED);
                }
                else if (TokenService.VerifyPassword(user.password, account.password))
                {
                    return TokenService.GenerateToken(account);
                }
                else
                {
                    return BadRequest(ConstValues.MSG_WRONG_PASSWORD);
                }
            }
            else
            {
                return BadRequest(ConstValues.MSG_ACCOUNT_NOT_FOUND);
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("sign-up")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult SignUp(User user)
        {
            if (CheckEmailExist(user.email))
            {
                return BadRequest(ConstValues.MSG_EMAIL_USED);
            }

            if (CheckUsernameExist(user.username))
            {
                return BadRequest(ConstValues.MSG_USERNAME_USED);
            }

            try
            {
                user.accessLevel = int.Parse(Configurations.GetDefaultAccessLevel());
                user.created = GenericUtils.GetDateTime();
                user.updated = GenericUtils.GetDateTime();
                user.username = user.username.ToLower();
                user.email = user.email.ToLower();
                user.password = TokenService.HashPassword(user.password);
                dataContext.users.Add(user);
                dataContext.SaveChanges();

                if (user.notifications == 1)
                {
                    _ = Notifications.SendEmail(user.email, ConstValues.NTF_TITLE_GENERIC, string.Format(ConstValues.NTF_BODY_NEW_ACCOUNT, user.username));
                }

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
            var userData = dataContext.users.FirstOrDefault(u => u.username == user.username || u.email == user.email);

            if (userData == null)
            {
                return BadRequest(ConstValues.MSG_ACCOUNT_NOT_FOUND);
            }

            var generatedPassword = GenericUtils.StringGenerator();
            userData.updated = GenericUtils.GetDateTime();
            userData.password = TokenService.HashPassword(GenericUtils.GenerateSha256String(generatedPassword));
            dataContext.users.Update(userData);
            dataContext.SaveChanges();
            _ = Notifications.SendEmail(userData.email, ConstValues.NTF_TITLE_PASSWORD, generatedPassword);

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
            var check = dataContext.users.FirstOrDefault(u => u.email == email);
            return check != null;
        }

        private bool CheckUsernameExist(string username)
        {
            var check = dataContext.users.FirstOrDefault(u => u.username == username);
            return check != null;
        }
    }
}
