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
        private readonly DataContext context;
        public ClubController(DataContext context) => this.context = context;

        /// <summary>
        /// Create club.
        /// </summary>
        [HttpPost]
        [Authorize]
        [Route("club")]
        public ActionResult<List<Club>> CreateClub(List<Club> club)
        {
            using var service = new ClubService(context);
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
            using var service = new ClubService(context);
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
            using var service = new ClubService(context);
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
            using var service = new ClubService(context);
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
                using var service = new ClubService(context);
                return service.GetClubs(query);
            }
            catch
            {
                return new NotFoundResult();
            }
        }

        /// <summary>
        /// Get club statistics.
        /// </summary>
        [HttpGet]
        [Authorize]
        [Route("clubStatistics")]
        public ActionResult<Statistics> GetAthleteStatistics(int clubId, int? gameId)
        {
            using var service = new ClubService(context);
            return service.GetClubStatistics(clubId, gameId);
        }
    }
}