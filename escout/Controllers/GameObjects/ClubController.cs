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
    public class ClubController : ControllerBase
    {
        private readonly DataContext context;
        public ClubController(DataContext context)
        {
            this.context = context;
        }

        [HttpPost]
        [Route("club")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<Club>> CreateClub(List<Club> club)
        {
            if (User.GetUser(context).accessLevel >= 3)
                return Forbid();

            club.ToList().ForEach(c => c.created = Utils.GetDateTime());
            club.ToList().ForEach(c => c.updated = Utils.GetDateTime());
            context.clubs.AddRange(club);
            context.SaveChanges();
            return club;

        }

        [HttpPut]
        [Route("club")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateClub(Club club)
        {
            if (User.GetUser(context).accessLevel >= 3)
                return Forbid();

            try
            {
                club.updated = Utils.GetDateTime();
                context.clubs.Update(club);
                context.SaveChanges();
                return Ok();
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpDelete]
        [Route("club")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DeleteClub(int id)
        {
            if (User.GetUser(context).accessLevel >= 3)
                return Forbid();

            try
            {
                var club = context.clubs.FirstOrDefault(c => c.id == id);
                context.clubs.Remove(club);
                context.SaveChanges();
                return Ok();
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpGet]
        [Route("club")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<Club> GetClub(int id)
        {
            return context.clubs.FirstOrDefault(c => c.id == id);
        }

        [HttpGet]
        [Route("clubs")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<Club>> GetClubs(string query)
        {
            try
            {
                List<Club> clubs;

                if (string.IsNullOrEmpty(query))
                    clubs = context.clubs.ToList();
                else
                {
                    var criteria = JsonConvert.DeserializeObject<FilterCriteria>(query);
                    var q = string.Format("SELECT * FROM clubs WHERE " + criteria.fieldName + " " + criteria.condition + " '" + criteria.value + "';");
                    clubs = context.clubs.FromSqlRaw(q).ToList();
                }

                return clubs;
            }
            catch
            {
                return new NotFoundResult();
            }
        }
    }
}
