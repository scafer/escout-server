using escout.Models;
using escout.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace escout.Controllers
{
    [Route("api/v1")]
    [ApiController]
    public class ClubController : ControllerBase
    {
        /// <summary>
        /// Create club.
        /// </summary>
        [HttpPost]
        [Authorize]
        [Route("club")]
        public ActionResult<List<Club>> CreateClub(List<Club> club)
        {
            using var service = new ClubService();
            return service.CreateClub(club);

        }

        /// <summary>
        /// Update club.
        /// </summary>
        [HttpPut]
        [Authorize]
        [Route("club")]
        public ActionResult<SvcResult> UpdateClub(Club club)
        {
            using var service = new ClubService();
            return service.UpdateClub(club) ? SvcResult.Set(0, "Success") : SvcResult.Set(1, "Error");
        }

        /// <summary>
        /// Delete club.
        /// </summary>
        [HttpDelete]
        [Authorize]
        [Route("club")]
        public ActionResult<SvcResult> DeleteClub(int id)
        {
            using var service = new ClubService();
            return service.RemoveClub(id) ? SvcResult.Set(0, "Success") : SvcResult.Set(1, "Error");
        }

        /// <summary>
        /// Get club.
        /// </summary>
        [HttpGet]
        [Authorize]
        [Route("club")]
        public ActionResult<Club> GetClub(int id)
        {
            using var service = new ClubService();
            return service.GetClub(id);
        }

        /// <summary>
        /// Get clubs.
        /// </summary>
        [HttpGet]
        [Authorize]
        [Route("clubs")]
        public ActionResult<List<Club>> GetClubs(string query)
        {
            try
            {
                using var service = new ClubService();
                return service.GetClubs(query);
            }
            catch
            {
                return new NotFoundResult();
            }
        }
    }
}