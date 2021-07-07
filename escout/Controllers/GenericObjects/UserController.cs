using escout.Helpers;
using escout.Models.Database;
using escout.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace escout.Controllers.GenericObjects
{
    [Authorize]
    [ApiController]
    [Route("api/v1/generic-object")]
    public class UserController : ControllerBase
    {
        private readonly DataContext dataContext;
        public UserController(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        [HttpPost]
        [Route("change-password")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult ChangePassword(string newPassword)
        {
            var user = User.GetUser(dataContext);

            if (user == null)
            {
                return new NotFoundResult();
            }

            try
            {
                user.password = BCrypt.Net.BCrypt.HashPassword(newPassword);
                user.updated = GenericUtils.GetDateTime();
                dataContext.users.Update(user);
                dataContext.SaveChanges();
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut]
        [Route("user")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateUser(User user)
        {
            try
            {
                user.updated = GenericUtils.GetDateTime();
                dataContext.users.Update(user);
                dataContext.SaveChanges();
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        [Route("user")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DeleteUser(User user)
        {
            if (User.GetUser(dataContext).accessLevel != ConstValues.AL_ADMINISTRATOR)
            {
                return Forbid();
            }

            try
            {
                dataContext.users.Remove(user);
                dataContext.SaveChanges();
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("user")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<User> GetUser()
        {
            var user = User.GetUser(dataContext);
            user.displayoptions = GetUserDisplayOptions(user);
            return user;
        }

        [HttpGet]
        [Route("users")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<User>> GetUsers(string query)
        {
            if (User.GetUser(dataContext).accessLevel == ConstValues.AL_ADMINISTRATOR)
            {
                if (string.IsNullOrEmpty(query))
                {
                    var allUsers = dataContext.users.ToList();

                    foreach (var user in allUsers)
                    {
                        user.displayoptions = GetUserDisplayOptions(user);
                    }

                    return allUsers;
                }

                var criteria = JsonConvert.DeserializeObject<FilterCriteria>(query);
                var q = string.Format(ConstValues.QUERY, "users", criteria.fieldName, criteria.condition, criteria.value);
                var filteredUsers = dataContext.users.FromSqlRaw(q).ToList();

                foreach (var user in filteredUsers)
                {
                    user.displayoptions = GetUserDisplayOptions(user);
                }

                return filteredUsers;
            }
            else
            {
                return new UnauthorizedResult();
            }
        }


        private Dictionary<string, string> GetUserDisplayOptions(User user)
        {
            var displayOptions = new Dictionary<string, string>();

            if (user.imageId != null)
            {
                var imageUrl = dataContext.images.FirstOrDefault(a => a.id == user.imageId).imageUrl;
                displayOptions.Add("imageUrl", imageUrl);
            }

            return displayOptions;
        }
    }
}
