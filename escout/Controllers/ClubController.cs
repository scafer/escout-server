using escout.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

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
        public ActionResult<SvcResult> AddClub(Club club)
        {
            return new NotFoundResult();
        }

        /// <summary>
        /// Update club.
        /// </summary>
        [HttpPut]
        [Authorize]
        [Route("club")]
        public ActionResult<SvcResult> UpdateClub(Club club)
        {
            return new NotFoundResult();
        }

        /// <summary>
        /// Delete club.
        /// </summary>
        [HttpDelete]
        [Authorize]
        [Route("club")]
        public ActionResult<SvcResult> DeleteClub(int id)
        {
            return new NotFoundResult();
        }

        /// <summary>
        /// Get club.
        /// </summary>
        [HttpGet]
        [Authorize]
        [Route("club")]
        public ActionResult<Club> GetClub(int id)
        {
            return new NotFoundResult();
        }

        /// <summary>
        /// Get clubs.
        /// </summary>
        [HttpGet]
        [Authorize]
        [Route("clubs")]
        public ActionResult<List<Club>> GetClubs()
        {
            return new NotFoundResult();
        }
    }
}