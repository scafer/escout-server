using escout.Models;
using escout.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace escout.Controllers
{
    [Route("api/v1")]
    [ApiController]
    public class AthleteController : ControllerBase
    {
        /// <summary>
        /// Create athlete.
        /// </summary>
        [HttpPost]
        [Authorize]
        [Route("athlete")]
        public ActionResult<List<Athlete>> CreateAthlete(List<Athlete> athlete)
        {
            using var service = new AthleteService();
            return service.CreateAthlete(athlete);
        }

        /// <summary>
        /// Update athlete.
        /// </summary>
        [HttpPut]
        [Authorize]
        [Route("athlete")]
        public ActionResult<SvcResult> UpdateAthlete(Athlete athlete)
        {
            using var service = new AthleteService();
            return service.UpdateAthlete(athlete) ? SvcResult.Set(0, "Success") : SvcResult.Set(1, "Error");
        }

        /// <summary>
        /// Delete athlete.
        /// </summary>
        [HttpDelete]
        [Authorize]
        [Route("athlete")]
        public ActionResult<SvcResult> RemoveAthlete(int id)
        {
            using var service = new AthleteService();
            return service.RemoveAthlete(id) ? SvcResult.Set(0, "Success") : SvcResult.Set(1, "Error");
        }

        /// <summary>
        /// Get athlete by id.
        /// </summary>
        [HttpGet]
        [Authorize]
        [Route("athlete")]
        public ActionResult<Athlete> GetAthlete(int id)
        {
            using var service = new AthleteService();
            return service.GetAthlete(id);
        }

        /// <summary>
        /// Get athletes.
        /// </summary>
        [HttpGet]
        [Authorize]
        [Route("athletes")]
        public ActionResult<List<Athlete>> GetAthletes(string query)
        {
            try
            {
                var criteria = JsonConvert.DeserializeObject<FilterCriteria>(query);
                using var service = new AthleteService();
                return service.GetAthletes(criteria);
            }
            catch
            {
                return new NotFoundResult();
            }
        }
    }
}