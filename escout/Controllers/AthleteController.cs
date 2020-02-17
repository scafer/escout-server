using escout.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace escout.Controllers
{
    [Route("api/v1")]
    [ApiController]
    public class AthleteController : ControllerBase
    {
        /// <summary>
        /// Create athlete.
        /// </summary>
        [HttpPost]
        [Authorize]
        [Route("athlete")]
        public ActionResult<SvcResult> AddAthlete(Athlete athlete)
        {
            return new NotFoundResult();
        }

        /// <summary>
        /// Update athlete.
        /// </summary>
        [HttpPut]
        [Authorize]
        [Route("athlete")]
        public ActionResult<SvcResult> UpdateAthlete(Athlete athlete)
        {
            return new NotFoundResult();
        }

        /// <summary>
        /// Delete athlete.
        /// </summary>
        [HttpDelete]
        [Authorize]
        [Route("athlete")]
        public ActionResult<SvcResult> DeleteAthlete(int id)
        {
            return new NotFoundResult();
        }

        /// <summary>
        /// Get 1thlete by id.
        /// </summary>
        [HttpGet]
        [Authorize]
        [Route("athlete")]
        public ActionResult<Athlete> GetAthlete(int id)
        {
            return new NotFoundResult();
        }

        /// <summary>
        /// Get athletes.
        /// </summary>
        [HttpGet]
        [Authorize]
        [Route("athletes")]
        public ActionResult<List<Athlete>> GetAthletes()
        {
            return new NotFoundResult();
        }
    }
}