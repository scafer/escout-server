using System;
using System.Collections.Generic;
using System.Linq;
using escout.Helpers;
using escout.Models.Database;
using escout.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace escout.Controllers.GameObjects
{
    [Authorize]
    [ApiController]
    [Route("api/v1/game-object")]
    public class AthleteController : ControllerBase
    {
        private readonly DataContext dataContext;
        public AthleteController(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        [HttpPost]
        [Route("athlete")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<List<Athlete>> CreateAthlete(List<Athlete> athletes)
        {
            if (User.GetUser(dataContext).accessLevel >= ConstValues.AL_USER)
            {
                return Forbid();
            }

            try
            {
                athletes.ToList().ForEach(a => a.created = GenericUtils.GetDateTime());
                athletes.ToList().ForEach(a => a.updated = GenericUtils.GetDateTime());
                dataContext.athletes.AddRange(athletes);
                dataContext.SaveChanges();
                return athletes;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("athlete")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult UpdateAthlete(Athlete athlete)
        {
            if (User.GetUser(dataContext).accessLevel >= ConstValues.AL_USER)
            {
                return Forbid();
            }

            try
            {
                athlete.updated = GenericUtils.GetDateTime();
                dataContext.athletes.Update(athlete);
                dataContext.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("athlete")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult RemoveAthlete(int id)
        {
            if (User.GetUser(dataContext).accessLevel >= ConstValues.AL_USER)
            {
                return Forbid();
            }

            try
            {
                var athlete = dataContext.athletes.FirstOrDefault(a => a.id == id);
                dataContext.athletes.Remove(athlete);
                dataContext.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("athlete")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<Athlete> GetAthlete(int id)
        {
            var athlete = dataContext.athletes.FirstOrDefault(a => a.id == id);
            athlete.displayOptions = GetAthleteDisplayOptions(athlete);
            return athlete;
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
                {
                    athletes = dataContext.athletes.ToList();
                }
                else
                {
                    var criteria = JsonConvert.DeserializeObject<FilterCriteria>(query);
                    var q = string.Format(ConstValues.QUERY, "athletes", criteria.fieldName, criteria.condition, criteria.value);
                    athletes = dataContext.athletes.FromSqlRaw(q).ToList();
                }

                foreach (var athlete in athletes)
                {
                    athlete.displayOptions = GetAthleteDisplayOptions(athlete);
                }

                return athletes;
            }
            catch
            {
                return new NotFoundResult();
            }
        }

        private Dictionary<string, string> GetAthleteDisplayOptions(Athlete athlete)
        {
            var displayOptions = new Dictionary<string, string>();

            if (athlete.clubId != null)
            {
                var clubName = dataContext.clubs.FirstOrDefault(a => a.id == athlete.clubId).name;
                displayOptions.Add(ConstValues.DO_CLUB_NAME, clubName);
            }

            if (athlete.imageId != null)
            {
                var imageUrl = dataContext.images.FirstOrDefault(a => a.id == athlete.imageId).imageUrl;
                displayOptions.Add(ConstValues.DO_IMAGE_URL, imageUrl);
            }

            return displayOptions;
        }
    }
}
