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
    public class EventController : ControllerBase
    {
        private readonly DataContext context;
        public EventController(DataContext context)
        {
            this.context = context;
        }

        [HttpPost]
        [Route("event")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<Event>> CreateEvent(List<Event> e)
        {
            if (!User.GetUser(context).accessLevel.Equals(0))
                return Forbid();

            e.ToList().ForEach(c => c.created = Utils.GetDateTime());
            e.ToList().ForEach(c => c.updated = Utils.GetDateTime());
            context.events.AddRange(e);
            context.SaveChanges();
            return e;
        }

        [HttpPut]
        [Route("event")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateEvent(Event e)
        {
            if (!User.GetUser(context).accessLevel.Equals(0))
                return Forbid();

            try
            {
                e.updated = Utils.GetDateTime();
                context.events.Update(e);
                context.SaveChanges();
                return Ok();
            }
            catch { return BadRequest(); }
        }

        [HttpDelete]
        [Route("event")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DeleteEvent(int id)
        {
            if (!User.GetUser(context).accessLevel.Equals(0))
                return Forbid();

            try
            {
                var evt = context.events.FirstOrDefault(e => e.id == id);
                context.events.Remove(evt);
                context.SaveChanges();
                return Ok();
            }
            catch { return BadRequest(); }
        }

        [HttpGet]
        [Route("event")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<Event> GetEvent(int id)
        {
            return context.events.FirstOrDefault(e => e.id == id);
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
                    events = context.events.ToList();
                else
                {
                    var criteria = JsonConvert.DeserializeObject<FilterCriteria>(query);
                    var q = string.Format("SELECT * FROM events WHERE " + criteria.fieldName + " " + criteria.condition + " '" + criteria.value + "';");
                    events = context.events.FromSqlRaw(q).ToList();
                }

                return events;
            }
            catch
            {
                return new NotFoundResult();
            }
        }
    }
}
