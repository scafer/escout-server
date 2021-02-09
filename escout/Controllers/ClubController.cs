using escout.Helpers;
using escout.Models;
using Microsoft.AspNetCore.Authorization;
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
    public class ClubController : ControllerBase
    {
        private readonly DataContext context;
        public ClubController(DataContext context) => this.context = context;

        [HttpPost]
        [Route("club")]
        public ActionResult<List<Club>> CreateClub(List<Club> club)
        {
            club.ToList().ForEach(c => c.created = Utils.GetDateTime());
            club.ToList().ForEach(c => c.updated = Utils.GetDateTime());
            context.clubs.AddRange(club);
            context.SaveChanges();
            return club;

        }

        [HttpPut]
        [Route("club")]
        public IActionResult UpdateClub(Club club)
        {
            try
            {
                club.updated = Utils.GetDateTime();
                context.clubs.Update(club);
                context.SaveChanges();
                return Ok();
            }
            catch(Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpDelete]
        [Route("club")]
        public IActionResult DeleteClub(int id)
        {
            try
            {
                var club = context.clubs.FirstOrDefault(c => c.id == id);
                context.clubs.Remove(club);
                context.SaveChanges();
                return Ok();
            }
            catch(Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpGet]
        [Route("club")]
        public ActionResult<Club> GetClub(int id)
        {
            return context.clubs.FirstOrDefault(c => c.id == id);
        }

        [HttpGet]
        [Route("clubs")]
        public ActionResult<List<Club>> GetClubs(string query)
        {
            try
            {
                List<Club> clubs;

                if (string.IsNullOrEmpty(query))
                    clubs = context.clubs.ToList();
                else
                {
                    var criteria = JsonConvert.DeserializeObject<FilterCriteria>(query);
                    var q = string.Format("SELECT * FROM clubs WHERE " + criteria.fieldName + " " + criteria.condition + " '" + criteria.value + "';");
                    clubs = context.clubs.FromSqlRaw(q).ToList();
                }

                return clubs;
            }
            catch
            {
                return new NotFoundResult();
            }
        }

        [HttpGet]
        [Route("clubStatistics")]
        public ActionResult<Statistics> GetAthleteStatistics(int clubId, int? gameId)
        {
            var gameEvents = new List<GameEvent>();
            var totalStatistics = new Statistics();
            var totalEvents = new List<GameEvent>();
            var count = new List<Counter>();

            if (gameId != null)
                gameEvents = context.gameEvents.Where(x => x.clubId == clubId && x.gameId == gameId).ToList();
            else
                gameEvents = context.gameEvents.Where(x => x.clubId == clubId).ToList();

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
    }
}