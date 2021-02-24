using escout.Helpers;
using escout.Models;
using escout.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace escout.Controllers.GameObjects
{
    [Authorize]
    [ApiController]
    [Route("api/v1/game-object")]
    public class AthleteController : ControllerBase
    {
        private readonly DataContext context;
        public AthleteController(DataContext context)
        {
            this.context = context;
        }

        [HttpPost]
        [Route("athlete")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<List<Athlete>> CreateAthlete(List<Athlete> athletes)
        {
            if (User.GetUser(context).accessLevel >= 3)
                return Forbid();

            try
            {
                athletes.ToList().ForEach(a => a.created = Utils.GetDateTime());
                athletes.ToList().ForEach(a => a.updated = Utils.GetDateTime());
                context.athletes.AddRange(athletes);
                context.SaveChanges();
                return athletes;
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpPut]
        [Route("athlete")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult UpdateAthlete(Athlete athlete)
        {
            if (User.GetUser(context).accessLevel >= 3)
                return Forbid();

            try
            {
                athlete.updated = Utils.GetDateTime();
                context.athletes.Update(athlete);
                context.SaveChanges();
                return Ok();
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpDelete]
        [Route("athlete")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult RemoveAthlete(int id)
        {
            if (User.GetUser(context).accessLevel >= 3)
                return Forbid();

            try
            {
                var athlete = context.athletes.FirstOrDefault(a => a.id == id);
                context.athletes.Remove(athlete);
                context.SaveChanges();
                return Ok();
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpGet]
        [Route("athlete")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<Athlete> GetAthlete(int id)
        {
            return context.athletes.FirstOrDefault(a => a.id == id);
        }

        [HttpGet]
        [Route("athletes")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<Athlete>> GetAthletes(string query)
        {
            try
            {
                List<Athlete> athletes;
                if (string.IsNullOrEmpty(query))
                    athletes = context.athletes.ToList();
                else
                {
                    var criteria = JsonConvert.DeserializeObject<FilterCriteria>(query);
                    var q = string.Format("SELECT * FROM athletes WHERE " + criteria.fieldName + " " + criteria.condition + " '" + criteria.value + "';");
                    athletes = context.athletes.FromSqlRaw(q).ToList();
                }

                return athletes;
            }
            catch { return new NotFoundResult(); }
        }
    }
}
