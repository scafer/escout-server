using System;
using System.Collections.Generic;
using System.Linq;
using escout.Helpers;
using escout.Models.Database;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace escout.Controllers.GameStatistics;

[Authorize]
[ApiController]
[Route("api/v1/game-statistics")]
public class StatisticsController : Controller
{
    private readonly DataContext dataContext;

    public StatisticsController(DataContext dataContext)
    {
        this.dataContext = dataContext;
    }

    [HttpGet]
    [Route("athlete")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<Statistics> GetAthleteStatistics(int athleteId, int? gameId)
    {
        try
        {
            List<GameEvent> gameEvents;
            List<GameEvent> totalEvents;
            var count = new List<Counter>();
            var totalStatistics = new Statistics();

            if (gameId != null)
                gameEvents = dataContext.gameEvents.Where(x => x.athleteId == athleteId && x.gameId == gameId).ToList();
            else
                gameEvents = dataContext.gameEvents.Where(x => x.athleteId == athleteId).ToList();

            var uniqueGames = gameEvents.Select(x => x.gameId).Distinct();

            foreach (var i in uniqueGames)
            {
                var game = gameEvents.Where(x => x.gameId == i).ToList();

                foreach (var e in dataContext.events)
                {
                    var events = game.Where(x => x.eventId == e.id).ToList();
                    var gameStats = new GameStats
                    {
                        EventId = e.id,
                        Count = events.Count,
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

            foreach (var e in dataContext.events)
            {
                List<Counter> counter;
                counter = count.Where(x => x.EventId == e.id).ToList();
                totalEvents = gameEvents.Where(x => x.eventId == e.id).ToList();

                var totalStats = new TotalStats
                {
                    EventId = e.id,
                    Count = totalEvents.Count,
                    Average = counter.Average(x => x.Count),
                    StandardDeviation = GenericUtils.StdDev(counter.Select(x => x.Count).ToArray()),
                    Median = GenericUtils.Median(counter.Select(x => x.Count).ToArray())
                };

                totalStatistics.TotalStats.Add(totalStats);
            }

            return totalStatistics;
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet]
    [Route("club")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<Statistics> GetClubStatistics(int clubId, int? gameId)
    {
        List<GameEvent> gameEvents;
        var totalStatistics = new Statistics();
        var count = new List<Counter>();

        if (gameId != null)
            gameEvents = dataContext.gameEvents.Where(x => x.clubId == clubId && x.gameId == gameId).ToList();
        else
            gameEvents = dataContext.gameEvents.Where(x => x.clubId == clubId).ToList();

        var uniqueGames = gameEvents.Select(x => x.gameId).Distinct().ToList();
        var allEvents = dataContext.events.ToList();

        foreach (var i in uniqueGames)
        {
            var game = gameEvents.Where(x => x.gameId == i).ToList();
            foreach (var e in allEvents)
            {
                var events = game.Where(x => x.eventId == e.id).ToList();
                var gameStats = new GameStats
                {
                    EventId = e.id,
                    Count = events.Count,
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

        foreach (var e in allEvents)
        {
            var counter = count.Where(x => x.EventId == e.id).ToList();
            var totalEvents = gameEvents.Where(x => x.eventId == e.id).ToList();

            var totalStats = new TotalStats
            {
                EventId = e.id,
                Count = totalEvents.Count,
                Average = counter.Average(x => x.Count),
                StandardDeviation = GenericUtils.StdDev(counter.Select(x => x.Count).ToArray()),
                Median = GenericUtils.Median(counter.Select(x => x.Count).ToArray())
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
        var gameEvents = dataContext.gameEvents.Where(x => x.gameId == gameId).ToList();
        var uniqueClubs = gameEvents.Select(x => x.clubId).Distinct().ToList();
        var allEvents = dataContext.events.ToList();

        foreach (var club in uniqueClubs)
            if (club != null)
            {
                var clubEvents = gameEvents.Where(x => x.clubId == club).ToList();

                foreach (var e in allEvents)
                {
                    var evt = clubEvents.Where(x => x.eventId == e.id).ToList();
                    var stat = new ClubStats
                    {
                        Count = evt.Count,
                        ClubId = int.Parse(club.ToString()),
                        EventId = e.id
                    };
                    stats.Add(stat);
                }
            }

        return stats;
    }
}