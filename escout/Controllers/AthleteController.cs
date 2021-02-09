using escout.Helpers;
using escout.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace escout.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1")]
    public class AthleteController : ControllerBase
    {
        private readonly DataContext context;
        public AthleteController(DataContext context) => this.context = context;

        [HttpPost]
        [Route("athlete")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<List<Athlete>> CreateAthlete(List<Athlete> athletes)
        {
            try
            {
                athletes.ToList().ForEach(a => a.created = Utils.GetDateTime());
                athletes.ToList().ForEach(a => a.updated = Utils.GetDateTime());
                context.athletes.AddRange(athletes);
                context.SaveChanges();
                return athletes;
            }
            catch(Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpPut]
        [Route("athlete")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult UpdateAthlete(Athlete athlete)
        {
            try
            {
                athlete.updated = Utils.GetDateTime();
                context.athletes.Update(athlete);
                context.SaveChanges();
                return Ok();
            }
            catch(Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpDelete]
        [Route("athlete")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult RemoveAthlete(int id)
        {
            try
            {
                var athlete = context.athletes.FirstOrDefault(a => a.id == id);
                context.athletes.Remove(athlete);
                context.SaveChanges();
                return Ok();
            }
            catch(Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpGet]
        [Route("athlete")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<Athlete> GetAthlete(int id)
        {
            return context.athletes.FirstOrDefault(a => a.id == id);
        }

        [HttpGet]
        [Route("athletes")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<Athlete>> GetAthletes(string query)
        {
            try
            {
                List<Athlete> athletes;
                if (string.IsNullOrEmpty(query))
                    athletes = context.athletes.ToList();
                else
                {
                    var criteria = JsonConvert.DeserializeObject<FilterCriteria>(query);
                    var q = string.Format("SELECT * FROM athletes WHERE " + criteria.fieldName + " " + criteria.condition + " '" + criteria.value + "';");
                    athletes = context.athletes.FromSqlRaw(q).ToList();
                }

                return athletes;
            }
            catch { return new NotFoundResult(); }
        }

        [HttpGet]
        [Route("athleteStatistics")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<Statistics> GetAthleteStatistics(int athleteId, int? gameId)
        {
            try
            {
                var gameEvents = new List<GameEvent>();
                var totalStatistics = new Statistics();
                var totalEvents = new List<GameEvent>();
                var count = new List<Counter>();

                if (gameId != null)
                    gameEvents = context.gameEvents.Where(x => x.athleteId == athleteId && x.gameId == gameId).ToList();
                else
                    gameEvents = context.gameEvents.Where(x => x.athleteId == athleteId).ToList();

                var uniqueGames = gameEvents.Select(x => x.gameId).Distinct();

                foreach (int i in uniqueGames)
                {
                    var game = gameEvents.Where(x => x.gameId == i).ToList();
                    foreach (Event e in context.events)
                    {
                        var events = game.Where(x => x.eventId == e.id).ToList();
                        var gameStats = new GameStats
                        {
                            EventId = e.id,
                            Count = events.Count(),
                            GameId = i
                        };

                        var counter = new Counter
                        {
                            Count = gameStats.Count,
                            EventId = e.id
                        };

                        count.Add(counter);
                        totalStatistics.GameStats.Add(gameStats);
                    }
                }

                foreach (Event e in context.events)
                {
                    var counter = new List<Counter>();
                    counter = count.Where(x => x.EventId == e.id).ToList();
                    totalEvents = gameEvents.Where(x => x.eventId == e.id).ToList();

                    var totalStats = new TotalStats
                    {
                        EventId = e.id,
                        Count = totalEvents.Count(),
                        Average = counter.Average(x => x.Count),
                        StandardDeviation = GameStatistics.StdDev(counter.Select(x => x.Count).ToArray()),
                        Median = GameStatistics.Median(counter.Select(x => x.Count).ToArray())
                    };

                    totalStatistics.TotalStats.Add(totalStats);
                }

                return totalStatistics;
            }
            catch(Exception ex) { return BadRequest(ex.Message); }
        }
    }
}