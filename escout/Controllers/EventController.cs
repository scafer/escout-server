using escout.Helpers;
using escout.Models;
using escout.Services;
using Microsoft.AspNetCore.Authorization;
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
    public class EventController : ControllerBase
    {
        private readonly DataContext context;
        public EventController(DataContext context) => this.context = context;

        [HttpPost]
        [Route("event")]
        public ActionResult<List<Event>> CreateEvent(List<Event> e)
        {
            e.ToList().ForEach(c => c.created = Utils.GetDateTime());
            e.ToList().ForEach(c => c.updated = Utils.GetDateTime());
            context.events.AddRange(e);
            context.SaveChanges();
            return e;
        }

        [HttpPut]
        [Route("event")]
        public IActionResult UpdateEvent(Event e)
        {
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
        public IActionResult DeleteEvent(int id)
        {
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
        public ActionResult<Event> GetEvent(int id)
        {
            return context.events.FirstOrDefault(e => e.id == id);
        }

        [HttpGet]
        [Route("events")]
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