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
    public class SportController : ControllerBase
    {
        /// <summary>
        /// Create sport.
        /// </summary>
        [HttpPost]
        [Authorize]
        [Route("sport")]
        public ActionResult<List<Sport>> CreateSport(List<Sport> sport)
        {
            using var service = new SportService();
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
            using var service = new SportService();
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
            using var service = new SportService();
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
            using var service = new SportService();
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
                var criteria = JsonConvert.DeserializeObject<FilterCriteria>(query);
                using var service = new SportService();
                return service.GetSports(criteria);
            }
            catch
            {
                return new NotFoundResult();
            }
        }
    }
}