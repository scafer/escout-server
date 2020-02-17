using escout.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

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
        public ActionResult<SvcResult> AddEvent(Event e)
        {
            return new NotFoundResult();
        }

        /// <summary>
        /// Update event.
        /// </summary>
        [HttpPut]
        [Authorize]
        [Route("event")]
        public ActionResult<SvcResult> UpdateEvent(Event e)
        {
            return new NotFoundResult();
        }

        /// <summary>
        /// Delete event.
        /// </summary>
        [HttpDelete]
        [Authorize]
        [Route("event")]
        public ActionResult<SvcResult> DeleteEvent(int id)
        {
            return new NotFoundResult();
        }

        /// <summary>
        /// Get event.
        /// </summary>
        [HttpGet]
        [Authorize]
        [Route("event")]
        public ActionResult<Event> GetEvent(int id)
        {
            return new NotFoundResult();
        }

        /// <summary>
        /// Get events.
        /// </summary>
        [HttpGet]
        [Authorize]
        [Route("events")]
        public ActionResult<List<Event>> GetEvents()
        {
            return new NotFoundResult();
        }
    }
}