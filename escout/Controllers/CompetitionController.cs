using escout.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace escout.Controllers
{
    [Route("api/v1")]
    [ApiController]
    public class CompetitionController : ControllerBase
    {
        /// <summary>
        /// Create competition.
        /// </summary>
        [HttpPost]
        [Authorize]
        [Route("competition")]
        public ActionResult<SvcResult> AddCompetition(Competition competition)
        {
            return new NotFoundResult();
        }

        /// <summary>
        /// Update competition.
        /// </summary>
        [HttpPut]
        [Authorize]
        [Route("competition")]
        public ActionResult<SvcResult> UpdateCompetition(Competition competition)
        {
            return new NotFoundResult();
        }

        /// <summary>
        /// Delete competition.
        /// </summary>
        [HttpDelete]
        [Authorize]
        [Route("competition")]
        public ActionResult<SvcResult> DeleteCompetition(int id)
        {
            return new NotFoundResult();
        }

        /// <summary>
        /// Get competition.
        /// </summary>
        [HttpGet]
        [Authorize]
        [Route("competition")]
        public ActionResult<Competition> GetCompetition(int id)
        {
            return new NotFoundResult();
        }

        /// <summary>
        /// Get competitions.
        /// </summary>
        [HttpGet]
        [Authorize]
        [Route("competitions")]
        public ActionResult<List<Competition>> GetCompetitions()
        {
            return new NotFoundResult();
        }
    }
}