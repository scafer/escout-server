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
    public class ClubController : ControllerBase
    {
        private readonly DataContext dataContext;
        public ClubController(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        [HttpPost]
        [Route("club")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<Club>> CreateClub(List<Club> club)
        {
            if (User.GetUser(dataContext).accessLevel >= ConstValues.AL_USER)
            {
                return Forbid();
            }

            club.ToList().ForEach(c => c.created = GenericUtils.GetDateTime());
            club.ToList().ForEach(c => c.updated = GenericUtils.GetDateTime());
            dataContext.clubs.AddRange(club);
            dataContext.SaveChanges();
            return club.OrderBy(x => x.id).ToList();
        }

        [HttpPut]
        [Route("club")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateClub(Club club)
        {
            if (User.GetUser(dataContext).accessLevel >= ConstValues.AL_USER)
            {
                return Forbid();
            }

            try
            {
                club.updated = GenericUtils.GetDateTime();
                dataContext.clubs.Update(club);
                dataContext.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("club")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DeleteClub(int id)
        {
            if (User.GetUser(dataContext).accessLevel >= ConstValues.AL_USER)
            {
                return Forbid();
            }

            try
            {
                var club = dataContext.clubs.FirstOrDefault(c => c.id == id);
                dataContext.clubs.Remove(club);
                dataContext.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("club")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<Club> GetClub(int id)
        {
            var club = dataContext.clubs.FirstOrDefault(c => c.id == id);
            club.displayOptions = GetClubDisplayOptions(club);
            return club;
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
                {
                    clubs = dataContext.clubs.ToList();
                }
                else
                {
                    var criteria = JsonConvert.DeserializeObject<FilterCriteria>(query);
                    var q = string.Format(ConstValues.QUERY, "clubs", criteria.fieldName, criteria.condition, criteria.value);
                    clubs = dataContext.clubs.FromSqlRaw(q).ToList();
                }

                foreach (var club in clubs)
                {
                    club.displayOptions = GetClubDisplayOptions(club);
                }

                return clubs.OrderBy(x => x.id).ToList();
            }
            catch
            {
                return new NotFoundResult();
            }
        }

        private Dictionary<string, string> GetClubDisplayOptions(Club club)
        {
            var displayOptions = new Dictionary<string, string>();

            if (club.imageId != null)
            {
                var imageUrl = dataContext.images.FirstOrDefault(a => a.id == club.imageId).imageUrl;
                displayOptions.Add(ConstValues.DO_IMAGE_URL, imageUrl);
            }

            return displayOptions;
        }
    }
}
