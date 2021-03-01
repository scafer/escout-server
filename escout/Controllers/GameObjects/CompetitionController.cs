using escout.Helpers;
using escout.Models;
using escout.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace escout.Controllers.GameObjects
{
    [Authorize]
    [ApiController]
    [Route("api/v1/game-object")]
    public class CompetitionController : ControllerBase
    {
        private readonly DataContext context;
        public CompetitionController(DataContext context)
        {
            this.context = context;
        }

        [HttpPost]
        [Route("competition")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<Competition>> CreateCompetition(List<Competition> competition)
        {
            if (User.GetUser(context).accessLevel >= 3)
                return Forbid();

            competition.ToList().ForEach(c => c.created = Utils.GetDateTime());
            competition.ToList().ForEach(c => c.updated = Utils.GetDateTime());
            context.competitions.AddRange(competition);
            context.SaveChanges();
            return competition;
        }

        [HttpPut]
        [Route("competition")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateCompetition(Competition competition)
        {
            if (User.GetUser(context).accessLevel >= 3)
                return Forbid();

            try
            {
                competition.updated = Utils.GetDateTime();
                context.competitions.Update(competition);
                context.SaveChanges();
                return Ok();
            }
            catch { return BadRequest(); }
        }

        [HttpDelete]
        [Route("competition")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DeleteCompetition(int id)
        {
            if (User.GetUser(context).accessLevel >= 3)
                return Forbid();

            try
            {
                var competition = context.competitions.FirstOrDefault(c => c.id == id);
                context.competitions.Remove(competition);
                context.SaveChanges();
                return Ok();
            }
            catch { return BadRequest(); }
        }

        [HttpGet]
        [Route("competition")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<Competition> GetCompetition(int id)
        {
            return context.competitions.FirstOrDefault(c => c.id == id);
        }

        [HttpGet]
        [Route("competitions")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<Competition>> GetCompetitions(string query)
        {
            try
            {
                List<Competition> competitions;

                if (string.IsNullOrEmpty(query))
                    competitions = context.competitions.ToList();
                else
                {
                    var criteria = JsonConvert.DeserializeObject<FilterCriteria>(query);
                    var q = string.Format("SELECT * FROM competitions WHERE " + criteria.fieldName + " " + criteria.condition + " '" + criteria.value + "';");
                    competitions = context.competitions.FromSqlRaw(q).ToList();
                }

                return competitions;
            }
            catch { return new NotFoundResult(); }
        }

        [HttpPost]
        [Route("competition-board")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<CompetitionBoard>> CreateCompetitionBoard(List<CompetitionBoard> competitionBoard)
        {
            competitionBoard.ToList().ForEach(c => c.created = Utils.GetDateTime());
            competitionBoard.ToList().ForEach(c => c.updated = Utils.GetDateTime());
            context.competitionBoards.AddRange(competitionBoard);
            context.SaveChanges();
            return competitionBoard;
        }

        [HttpPut]
        [Route("competition-board")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateCompetitionBoard(CompetitionBoard competitionBoard)
        {
            try
            {
                competitionBoard.updated = Utils.GetDateTime();
                context.competitionBoards.Update(competitionBoard);
                context.SaveChanges();
                return Ok();
            }
            catch { return BadRequest(); }
        }

        [HttpDelete]
        [Route("competition-board")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DeleteCompetitionBoard(int id)
        {
            try
            {
                var competitionBoard = context.competitionBoards.FirstOrDefault(c => c.id == id);
                context.competitionBoards.Remove(competitionBoard);
                context.SaveChanges();
                return Ok();
            }
            catch { return BadRequest(); }
        }

        [HttpGet]
        [Route("competition-board")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<CompetitionBoard>> GetCompetitionBoard(int id)
        {
            return context.competitionBoards.Where(c => c.competitionId == id).ToList();
        }
    }
}
