using escout.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace escout.Controllers
{
    [Route("api/v1")]
    [ApiController]
    public class SportController : ControllerBase
    {
        /// <summary>
        /// Create sport.
        /// </summary>
        [HttpPost]
        [Authorize]
        [Route("sport")]
        public ActionResult<SvcResult> AddSport(Sport sport)
        {
            return new NotFoundResult();
        }

        /// <summary>
        /// Update sport.
        /// </summary>
        [HttpPut]
        [Authorize]
        [Route("sport")]
        public ActionResult<SvcResult> UpdateSport(Sport sport)
        {
            return new NotFoundResult();
        }

        /// <summary>
        /// Delete sport.
        /// </summary>
        [HttpDelete]
        [Authorize]
        [Route("sport")]
        public ActionResult<SvcResult> DeleteSport(int id)
        {
            return new NotFoundResult();
        }

        /// <summary>
        /// Get sport.
        /// </summary>
        [HttpGet]
        [Authorize]
        [Route("sport")]
        public ActionResult<Sport> GetSport(int id)
        {
            return new NotFoundResult();
        }

        /// <summary>
        /// Get sports.
        /// </summary>
        [HttpGet]
        [Authorize]
        [Route("sports")]
        public ActionResult<List<Sport>> GetSports()
        {
            return new NotFoundResult();
        }
    }
}