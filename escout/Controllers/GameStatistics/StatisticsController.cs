using System;
using System.Collections.Generic;
using System.Linq;
using escout.Helpers;
using escout.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace escout.Controllers.GameStatistics
{
    [Authorize]
    [ApiController]
    [Route("api/v1/game-statistics")]
    public class StatisticsController : Controller
    {
        private readonly DataContext context;
        public StatisticsController(DataContext context)
        {
            this.context = context;
        }

        [HttpGet]
        [Route("athlete")]
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
                        StandardDeviation = Utils.StdDev(counter.Select(x => x.Count).ToArray()),
                        Median = Utils.Median(counter.Select(x => x.Count).ToArray())
                    };

                    totalStatistics.TotalStats.Add(totalStats);
                }

                return totalStatistics;
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpGet]
        [Route("club")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<Statistics> GetClubStatistics(int clubId, int? gameId)
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
                    StandardDeviation = Utils.StdDev(counter.Select(x => x.Count).ToArray()),
                    Median = Utils.Median(counter.Select(x => x.Count).ToArray())
                };

                totalStatistics.TotalStats.Add(totalStats);
            }

            return totalStatistics;
        }

        [HttpGet]
        [Route("game")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<ClubStats>> GetGameStatistics(int gameId)
        {
            var stats = new List<ClubStats>();

            var gameEvents = context.gameEvents.Where(x => x.gameId == gameId).ToList();
            var uniqueClubs = gameEvents.Select(x => x.clubId).Distinct().ToList();

            foreach (var i in uniqueClubs)
            {
                if (i != null)
                {
                    var clubEvents = gameEvents.Where(x => x.clubId == i).ToList();
                    foreach (Event e in context.events)
                    {
                        var evt = clubEvents.Where(x => x.eventId == e.id).ToList();
                        var stat = new ClubStats
                        {
                            Count = evt.Count(),
                            ClubId = int.Parse(i.ToString()),
                            EventId = e.id
                        };
                        stats.Add(stat);
                    }
                }
            }

            return stats;
        }
    }
}
