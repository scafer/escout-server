using escout.Models;
using escout.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace escout.Controllers
{
    [Route("api/v1")]
    [ApiController]
    public class SportController : ControllerBase
    {
        private readonly DataContext context;
        public SportController(DataContext context) => this.context = context;

        /// <summary>
        /// Create sport.
        /// </summary>
        [HttpPost]
        [Authorize]
        [Route("sport")]
        public ActionResult<List<Sport>> CreateSport(List<Sport> sport)
        {
            using var service = new SportService(context);
            return service.CreateSport(sport);
        }

        /// <summary>
        /// Update sport.
        /// </summary>
        [HttpPut]
        [Authorize]
        [Route("sport")]
        public ActionResult<SvcResult> UpdateSport(Sport sport)
        {
            using var service = new SportService(context);
            return service.UpdateSport(sport) ? SvcResult.Set(0, "Success") : SvcResult.Set(1, "Error");
        }

        /// <summary>
        /// Delete sport.
        /// </summary>
        [HttpDelete]
        [Authorize]
        [Route("sport")]
        public ActionResult<SvcResult> DeleteSport(int id)
        {
            using var service = new SportService(context);
            return service.RemoveSport(id) ? SvcResult.Set(0, "Success") : SvcResult.Set(1, "Error");
        }

        /// <summary>
        /// Get sport.
        /// </summary>
        [HttpGet]
        [Authorize]
        [Route("sport")]
        public ActionResult<Sport> GetSport(int id)
        {
            using var service = new SportService(context);
            return service.GetSport(id);
        }

        /// <summary>
        /// Get sports.
        /// </summary>
        [HttpGet]
        [Authorize]
        [Route("sports")]
        public ActionResult<List<Sport>> GetSports(string query)
        {
            try
            {
                using var service = new SportService(context);
                return service.GetSports(query);
            }
            catch
            {
                return new NotFoundResult();
            }
        }
    }
}