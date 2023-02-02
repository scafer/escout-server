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

namespace escout.Controllers.GameObjects;

[Authorize]
[ApiController]
[Route("api/v1/game-object")]
public class SportController : ControllerBase
{
    private readonly DataContext dataContext;

    public SportController(DataContext dataContext)
    {
        this.dataContext = dataContext;
    }

    [HttpPost]
    [Route("sport")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<List<Sport>> CreateSport(List<Sport> sport)
    {
        if (User.GetUser(dataContext).accessLevel >= ConstValues.AL_USER) return Forbid();

        sport.ToList().ForEach(s => s.created = GenericUtils.GetDateTime());
        sport.ToList().ForEach(s => s.updated = GenericUtils.GetDateTime());
        dataContext.sports.AddRange(sport);
        dataContext.SaveChanges();
        return sport.OrderBy(x => x.id).ToList();
    }

    [HttpPut]
    [Route("sport")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult UpdateSport(Sport sport)
    {
        if (User.GetUser(dataContext).accessLevel >= ConstValues.AL_USER) return Forbid();

        try
        {
            sport.updated = GenericUtils.GetDateTime();
            dataContext.sports.Update(sport);
            dataContext.SaveChanges();
            return Ok();
        }
        catch
        {
            return BadRequest();
        }
    }

    [HttpDelete]
    [Route("sport")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult DeleteSport(int id)
    {
        if (User.GetUser(dataContext).accessLevel >= ConstValues.AL_USER) return Forbid();

        try
        {
            var sport = dataContext.sports.FirstOrDefault(s => s.id == id);
            dataContext.sports.Remove(sport);
            dataContext.SaveChanges();
            return Ok();
        }
        catch
        {
            return BadRequest();
        }
    }

    [HttpGet]
    [Route("sport")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<Sport> GetSport(int id)
    {
        var sport = dataContext.sports.FirstOrDefault(s => s.id == id);

        if (sport != null)
        {
            sport.displayOptions = GetSportDisplayOptions(sport);
            return sport;
        }

        return NotFound();
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
            {
                sports = dataContext.sports.ToList();
            }
            else
            {
                var criteria = JsonConvert.DeserializeObject<FilterCriteria>(query);
                var q = string.Format(ConstValues.QUERY, "sports", criteria.fieldName, criteria.condition,
                    criteria.value);
                sports = dataContext.sports.FromSqlRaw(q).ToList();
            }

            foreach (var sport in sports) sport.displayOptions = GetSportDisplayOptions(sport);

            return sports.OrderBy(x => x.id).ToList();
        }
        catch
        {
            return new NotFoundResult();
        }
    }


    private Dictionary<string, string> GetSportDisplayOptions(Sport sport)
    {
        var displayOptions = new Dictionary<string, string>();

        if (sport.imageId != null)
        {
            var image = dataContext.images.FirstOrDefault(a => a.id == sport.imageId);

            if (image != null) displayOptions.Add(ConstValues.DO_IMAGE_URL, image.imageUrl);
        }

        return displayOptions;
    }
}