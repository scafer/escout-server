using escout.Helpers;
using escout.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace escout.Services
{
    public class AthleteService : BaseService
    {
        readonly DataContext db;

        public AthleteService() => db = new DataContext();

        public List<Athlete> CreateAthlete(List<Athlete> athlete)
        {
            athlete.ToList().ForEach(a => a.created = Utils.GetDateTime());
            athlete.ToList().ForEach(a => a.updated = Utils.GetDateTime());
            db.athletes.AddRange(athlete);
            db.SaveChanges();
            return athlete;
        }

        public bool UpdateAthlete(Athlete athlete)
        {
            try
            {
                athlete.updated = Utils.GetDateTime();
                db.athletes.Update(athlete);
                db.SaveChanges();
                return true;
            }
            catch { return false; }
        }

        public bool RemoveAthlete(int id)
        {
            try
            {
                var athlete = db.athletes.FirstOrDefault(a => a.id == id);
                db.athletes.Remove(athlete);
                db.SaveChanges();
                return true;
            }
            catch { return false; }
        }

        public Athlete GetAthlete(int id)
        {
            return db.athletes.FirstOrDefault(a => a.id == id);
        }

        public List<Athlete> GetAthletes(string query)
        {
            List<Athlete> athletes;

            if (string.IsNullOrEmpty(query))
                athletes = db.athletes.ToList();
            else
            {
                var criteria = JsonConvert.DeserializeObject<FilterCriteria>(query);
                var q = string.Format("SELECT * FROM athletes WHERE " + criteria.fieldName + " " + criteria.condition + " '" + criteria.value + "';");
                athletes = db.athletes.FromSqlRaw(q).ToList();
            }

            return athletes;
        }

        public Statistics GetAthleteStatistics(int athleteId, int? gameId)
        {
            var gameEvents = new List<GameEvent>();
            var totalStatistics = new Statistics();
            var totalEvents = new List<GameEvent>();
            var count = new List<Counter>();

            if (gameId != null)
                gameEvents = db.gameEvents.Where(x => x.athleteId == athleteId && x.gameId == gameId).ToList();
            else
                gameEvents = db.gameEvents.Where(x => x.athleteId == athleteId).ToList();

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
                    StandardDeviation = 1,
                    Median = 1
                };

                totalStatistics.TotalStats.Add(totalStats);
            }

            return totalStatistics;
        }
    }
}
