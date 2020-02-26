using escout.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using escout.Services;
using Newtonsoft.Json;

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
                var criteria = JsonConvert.DeserializeObject<FilterCriteria>(query);
                using var service = new ClubService();
                return service.GetClubs(criteria);
            }
            catch
            {
                return new NotFoundResult();
            }
        }
    }
}