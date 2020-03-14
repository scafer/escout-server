using escout.Models;
using escout.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;

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
        public ActionResult<List<Competition>> CreateCompetition(List<Competition> competition)
        {
            using var service = new CompetitionService();
            return service.CreateCompetition(competition);
        }

        /// <summary>
        /// Update competition.
        /// </summary>
        [HttpPut]
        [Authorize]
        [Route("competition")]
        public ActionResult<SvcResult> UpdateCompetition(Competition competition)
        {
            using var service = new CompetitionService();
            return service.UpdateCompetition(competition) ? SvcResult.Set(0, "Success") : SvcResult.Set(1, "Error");
        }

        /// <summary>
        /// Delete competition.
        /// </summary>
        [HttpDelete]
        [Authorize]
        [Route("competition")]
        public ActionResult<SvcResult> DeleteCompetition(int id)
        {
            using var service = new CompetitionService();
            return service.RemoveCompetition(id) ? SvcResult.Set(0, "Success") : SvcResult.Set(1, "Error");
        }

        /// <summary>
        /// Get competition.
        /// </summary>
        [HttpGet]
        [Authorize]
        [Route("competition")]
        public ActionResult<Competition> GetCompetition(int id)
        {
            using var service = new CompetitionService();
            return service.GetCompetition(id);
        }

        /// <summary>
        /// Get competitions.
        /// </summary>
        [HttpGet]
        [Authorize]
        [Route("competitions")]
        public ActionResult<List<Competition>> GetCompetitions(string query)
        {
            try
            {
                using var service = new CompetitionService();
                return service.GetCompetitions(query);
            }
            catch
            {
                return new NotFoundResult();
            }
        }

        [HttpPost]
        [Authorize]
        [Route("competitionBoard")]
        public ActionResult<List<CompetitionBoard>> CreateCompetitionBoard(List<CompetitionBoard> competitionBoard)
        {
            using var service = new CompetitionService();
            return service.CreateCompetitionBoard(competitionBoard);
        }

        [HttpPut]
        [Authorize]
        [Route("competitionBoard")]
        public ActionResult<SvcResult> UpdateCompetitionBoard(CompetitionBoard competitionBoard)
        {
            using var service = new CompetitionService();
            return service.UpdateCompetitionBoard(competitionBoard) ? SvcResult.Set(0, "Success") : SvcResult.Set(1, "Error");
        }

        [HttpDelete]
        [Authorize]
        [Route("competitionBoard")]
        public ActionResult<SvcResult> DeleteCompetitionBoard(int competitionId)
        {
            using var service = new CompetitionService();
            return service.RemoveCompetitionBoard(competitionId) ? SvcResult.Set(0, "Success") : SvcResult.Set(1, "Error");
        }

        [HttpGet]
        [Authorize]
        [Route("competitionBoard")]
        public ActionResult<CompetitionBoard> GetCompetitionBoard(int id)
        {
            using var service = new CompetitionService();
            return service.GetCompetitionBoard(id);
        }
    }
}