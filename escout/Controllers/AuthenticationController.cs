using escout.Models;
using escout.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace escout.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : ControllerBase
    {
        AuthService authService = new AuthService();

        [HttpPost]
        [Route("signIn")]
        [AllowAnonymous]
        public ActionResult<AuthData> SignIn(User userData)
        {
            var user = new User();
            using (var service = new AuthService())
            {
                user = service.SignIn(userData);
            }

            if (user != null)
            {
                var token = authService.GetAuthData(user.id);
                return token;
            }
            return new NotFoundResult();
        }

        [HttpPost]
        [Route("signUp")]
        [AllowAnonymous]
        public ActionResult<string> SignUp(User user)
        {
            var userService = new UserService();
            var authService = new AuthService();

            var checkEmail = userService.CheckEmailExist(user.email);
            if (checkEmail) return "Email already in use.";

            var checkUsername = userService.CheckUsernameExist(user.username);
            if (checkUsername) return "Username already in use.";

            bool userAdded = authService.SignUp(user);
            if (!userAdded) return "An error occurred while adding the user.";

            return "User added successfully";
        }
    }
}