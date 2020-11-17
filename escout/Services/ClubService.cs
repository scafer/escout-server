using escout.Helpers;
using escout.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace escout.Services
{
    public class ClubService : BaseService
    {
        readonly DataContext db;

        public ClubService() => db = new DataContext();

        public List<Club> CreateClub(List<Club> club)
        {
            club.ToList().ForEach(c => c.created = Utils.GetDateTime());
            club.ToList().ForEach(c => c.updated = Utils.GetDateTime());
            db.clubs.AddRange(club);
            db.SaveChanges();
            return club;
        }

        public bool UpdateClub(Club club)
        {
            try
            {
                club.updated = Utils.GetDateTime();
                db.clubs.Update(club);
                db.SaveChanges();
                return true;
            }
            catch { return false; }
        }

        public bool RemoveClub(int id)
        {
            try
            {
                var club = db.clubs.FirstOrDefault(c => c.id == id);
                db.clubs.Remove(club);
                db.SaveChanges();
                return true;
            }
            catch { return false; }
        }

        public Club GetClub(int id)
        {
            return db.clubs.FirstOrDefault(c => c.id == id);
        }

        public List<Club> GetClubs(string query)
        {
            List<Club> clubs;

            if (string.IsNullOrEmpty(query))
                clubs = db.clubs.ToList();
            else
            {
                var criteria = JsonConvert.DeserializeObject<FilterCriteria>(query);
                var q = string.Format("SELECT * FROM clubs WHERE " + criteria.fieldName + " " + criteria.condition + " '" + criteria.value + "';");
                clubs = db.clubs.FromSqlRaw(q).ToList();
            }

            return clubs;
        }

        public Statistics GetClubStatistics(int clubId, int? gameId)
        {
            var gameEvents = new List<GameEvent>();
            var totalStatistics = new Statistics();
            var totalEvents = new List<GameEvent>();
            var count = new List<Counter>();

            if (gameId != null)
                gameEvents = db.gameEvents.Where(x => x.clubId == clubId && x.gameId == gameId).ToList();
            else
                gameEvents = db.gameEvents.Where(x => x.clubId == clubId).ToList();

            var uniqueGames = gameEvents.Select(x => x.gameId).Distinct();

            foreach (int i in uniqueGames)
            {
                var game = gameEvents.Where(x => x.gameId == i).ToList();
                foreach (Event e in db.events)
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

            foreach (Event e in db.events)
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
