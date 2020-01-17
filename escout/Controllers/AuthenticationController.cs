using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using escout.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace escout.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : ControllerBase
    {
        [HttpPost]
        [Route("signIn")]
        [AllowAnonymous]
        public ActionResult<AuthData> SignIn(User user)
        {
            return new NotFoundResult();
        }

        [HttpPost]
        [Route("signUp")]
        [AllowAnonymous]
        public ActionResult<string> SignUp(User user)
        {
            return new NotFoundResult();
        }
    }
}