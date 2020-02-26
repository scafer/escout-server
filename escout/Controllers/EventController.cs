using escout.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using escout.Services;
using Newtonsoft.Json;

namespace escout.Controllers
{
    [Route("api/v1")]
    [ApiController]
    public class EventController : ControllerBase
    {
        /// <summary>
        /// Create event.
        /// </summary>
        [HttpPost]
        [Authorize]
        [Route("event")]
        public ActionResult<List<Event>> CreateEvent(List<Event> e)
        {
            using var service = new EventService();
            return service.CreateEvent(e);
        }

        /// <summary>
        /// Update event.
        /// </summary>
        [HttpPut]
        [Authorize]
        [Route("event")]
        public ActionResult<SvcResult> UpdateEvent(Event e)
        {
            using var service = new EventService();
            return service.UpdateEvent(e) ? SvcResult.Set(0, "Success") : SvcResult.Set(1, "Error");
        }

        /// <summary>
        /// Delete event.
        /// </summary>
        [HttpDelete]
        [Authorize]
        [Route("event")]
        public ActionResult<SvcResult> DeleteEvent(int id)
        {
            using var service = new EventService();
            return service.RemoveEvent(id) ? SvcResult.Set(0, "Success") : SvcResult.Set(1, "Error");
        }

        /// <summary>
        /// Get event.
        /// </summary>
        [HttpGet]
        [Authorize]
        [Route("event")]
        public ActionResult<Event> GetEvent(int id)
        {
            using var service = new EventService();
            return service.GetEvent(id);
        }

        /// <summary>
        /// Get events.
        /// </summary>
        [HttpGet]
        [Authorize]
        [Route("events")]
        public ActionResult<List<Event>> GetEvents(string query)
        {
            try
            {
                var criteria = JsonConvert.DeserializeObject<FilterCriteria>(query);
                using var service = new EventService();
                return service.GetEvents(criteria);
            }
            catch
            {
                return new NotFoundResult();
            }
        }
    }
}