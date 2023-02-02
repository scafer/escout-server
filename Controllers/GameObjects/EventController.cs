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
public class EventController : ControllerBase
{
    private readonly DataContext dataContext;

    public EventController(DataContext dataContext)
    {
        this.dataContext = dataContext;
    }

    [HttpPost]
    [Route("event")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<List<Event>> CreateEvent(List<Event> e)
    {
        if (User.GetUser(dataContext).accessLevel != ConstValues.AL_ADMINISTRATOR) return Forbid();

        e.ToList().ForEach(c => c.created = GenericUtils.GetDateTime());
        e.ToList().ForEach(c => c.updated = GenericUtils.GetDateTime());
        dataContext.events.AddRange(e);
        dataContext.SaveChanges();
        return e.OrderBy(x => x.id).ToList();
    }

    [HttpPut]
    [Route("event")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult UpdateEvent(Event e)
    {
        if (User.GetUser(dataContext).accessLevel != ConstValues.AL_ADMINISTRATOR) return Forbid();

        try
        {
            e.updated = GenericUtils.GetDateTime();
            dataContext.events.Update(e);
            dataContext.SaveChanges();
            return Ok();
        }
        catch
        {
            return BadRequest();
        }
    }

    [HttpDelete]
    [Route("event")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult DeleteEvent(int id)
    {
        if (User.GetUser(dataContext).accessLevel != ConstValues.AL_ADMINISTRATOR) return Forbid();

        try
        {
            var evt = dataContext.events.FirstOrDefault(e => e.id == id);
            dataContext.events.Remove(evt);
            dataContext.SaveChanges();
            return Ok();
        }
        catch
        {
            return BadRequest();
        }
    }

    [HttpGet]
    [Route("event")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<Event> GetEvent(int id)
    {
        var evt = dataContext.events.FirstOrDefault(e => e.id == id);

        if (evt != null)
        {
            evt.displayOptions = GetEventDisplayOptions(evt);
            return evt;
        }

        return NotFound();
    }

    [HttpGet]
    [Route("events")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<List<Event>> GetEvents(string query)
    {
        try
        {
            List<Event> events;

            if (string.IsNullOrEmpty(query))
            {
                events = dataContext.events.ToList();
            }
            else
            {
                var criteria = JsonConvert.DeserializeObject<FilterCriteria>(query);
                var q = string.Format(ConstValues.QUERY, "events", criteria.fieldName, criteria.condition,
                    criteria.value);
                events = dataContext.events.FromSqlRaw(q).ToList();
            }

            foreach (var evt in events) evt.displayOptions = GetEventDisplayOptions(evt);

            return events.OrderBy(x => x.id).ToList();
        }
        catch
        {
            return new NotFoundResult();
        }
    }

    private Dictionary<string, string> GetEventDisplayOptions(Event evt)
    {
        var displayOptions = new Dictionary<string, string>();

        if (evt.imageId != null)
        {
            var image = dataContext.images.FirstOrDefault(a => a.id == evt.imageId);

            if (image != null) displayOptions.Add(ConstValues.DO_IMAGE_URL, image.imageUrl);
        }

        if (evt.sportId != 0)
        {
            var sport = dataContext.sports.FirstOrDefault(a => a.id == evt.sportId);

            if (sport != null) displayOptions.Add(ConstValues.DO_SPORT_NAME, sport.name);
        }

        return displayOptions;
    }
}