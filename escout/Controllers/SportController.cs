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

namespace escout.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1")]
    public class SportController : ControllerBase
    {
        private readonly DataContext context;
        public SportController(DataContext context) => this.context = context;

        [HttpPost]
        [Route("sport")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<Sport>> CreateSport(List<Sport> sport)
        {
            sport.ToList().ForEach(s => s.created = Utils.GetDateTime());
            sport.ToList().ForEach(s => s.updated = Utils.GetDateTime());
            context.sports.AddRange(sport);
            context.SaveChanges();
            return sport;
        }

        [HttpPut]
        [Route("sport")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateSport(Sport sport)
        {
            try
            {
                sport.updated = Utils.GetDateTime();
                context.sports.Update(sport);
                context.SaveChanges();
                return Ok();
            }
            catch { return BadRequest(); }
        }

        [HttpDelete]
        [Route("sport")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DeleteSport(int id)
        {
            try
            {
                var sport = context.sports.FirstOrDefault(s => s.id == id);
                context.sports.Remove(sport);
                context.SaveChanges();
                return Ok();
            }
            catch { return BadRequest(); }
        }

        [HttpGet]
        [Route("sport")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<Sport> GetSport(int id)
        {
            return context.sports.FirstOrDefault(s => s.id == id);
        }

        [HttpGet]
        [Route("sports")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<Sport>> GetSports(string query)
        {
            try
            {
                List<Sport> sports;

                if (string.IsNullOrEmpty(query))
                    sports = context.sports.ToList();
                else
                {
                    var criteria = JsonConvert.DeserializeObject<FilterCriteria>(query);
                    var q = string.Format("SELECT * FROM sports WHERE " + criteria.fieldName + " " + criteria.condition + " '" + criteria.value + "';");
                    sports = context.sports.FromSqlRaw(q).ToList();
                }

                return sports;
            }
            catch
            {
                return new NotFoundResult();
            }
        }
    }
}