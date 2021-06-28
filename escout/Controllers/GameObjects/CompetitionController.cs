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

namespace escout.Controllers.GameObjects
{
    [Authorize]
    [ApiController]
    [Route("api/v1/game-object")]
    public class CompetitionController : ControllerBase
    {
        private readonly DataContext dataContext;
        public CompetitionController(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        [HttpPost]
        [Route("competition")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<Competition>> CreateCompetition(List<Competition> competition)
        {
            if (User.GetUser(dataContext).accessLevel >= ConstValues.AL_USER)
            {
                return Forbid();
            }

            competition.ToList().ForEach(c => c.created = GenericUtils.GetDateTime());
            competition.ToList().ForEach(c => c.updated = GenericUtils.GetDateTime());
            dataContext.competitions.AddRange(competition);
            dataContext.SaveChanges();
            return competition;
        }

        [HttpPut]
        [Route("competition")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateCompetition(Competition competition)
        {
            if (User.GetUser(dataContext).accessLevel >= ConstValues.AL_USER)
            {
                return Forbid();
            }

            try
            {
                competition.updated = GenericUtils.GetDateTime();
                dataContext.competitions.Update(competition);
                dataContext.SaveChanges();
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        [Route("competition")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DeleteCompetition(int id)
        {
            if (User.GetUser(dataContext).accessLevel >= ConstValues.AL_USER)
            {
                return Forbid();
            }

            try
            {
                var competition = dataContext.competitions.FirstOrDefault(c => c.id == id);
                dataContext.competitions.Remove(competition);
                dataContext.SaveChanges();
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("competition")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<Competition> GetCompetition(int id)
        {
            return dataContext.competitions.FirstOrDefault(c => c.id == id);
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
                {
                    competitions = dataContext.competitions.ToList();
                }
                else
                {
                    var criteria = JsonConvert.DeserializeObject<FilterCriteria>(query);
                    var q = string.Format(ConstValues.QUERY, "competitions", criteria.fieldName, criteria.condition, criteria.value);
                    competitions = dataContext.competitions.FromSqlRaw(q).ToList();
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
            if (User.GetUser(dataContext).accessLevel >= ConstValues.AL_USER)
            {
                return Forbid();
            }

            competitionBoard.ToList().ForEach(c => c.created = GenericUtils.GetDateTime());
            competitionBoard.ToList().ForEach(c => c.updated = GenericUtils.GetDateTime());
            dataContext.competitionBoards.AddRange(competitionBoard);
            dataContext.SaveChanges();
            return competitionBoard;
        }

        [HttpPut]
        [Route("competition-board")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateCompetitionBoard(CompetitionBoard competitionBoard)
        {
            if (User.GetUser(dataContext).accessLevel >= ConstValues.AL_USER)
            {
                return Forbid();
            }

            try
            {
                competitionBoard.updated = GenericUtils.GetDateTime();
                dataContext.competitionBoards.Update(competitionBoard);
                dataContext.SaveChanges();
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        [Route("competition-board")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DeleteCompetitionBoard(int id)
        {
            if (User.GetUser(dataContext).accessLevel >= ConstValues.AL_USER)
            {
                return Forbid();
            }

            try
            {
                var competitionBoard = dataContext.competitionBoards.FirstOrDefault(c => c.id == id);
                dataContext.competitionBoards.Remove(competitionBoard);
                dataContext.SaveChanges();
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("competition-board")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<CompetitionBoard>> GetCompetitionBoard(int id)
        {
            return dataContext.competitionBoards.Where(c => c.competitionId == id).ToList();
        }
    }
}
