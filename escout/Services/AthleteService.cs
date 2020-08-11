using escout.Helpers;
using escout.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
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

        public ActionResult<Statistics> GetAthleteStatistics(int athleteId, int? gameId)
        {
            var gameEvents = new List<GameEvent>();
            var totalStatistics = new Statistics();
            var totalEvents = new List<GameEvent>();
            var count = new List<xpto>();    
            
            if (gameId != null)
                gameEvents = db.gameEvents.Where(x => x.athleteId == athleteId && x.gameId == gameId).ToList();
            else
                gameEvents = db.gameEvents.Where(x => x.athleteId == athleteId).ToList();

            var uniqueGames = gameEvents.Select(x => x.gameId).Distinct();

            foreach(int i in uniqueGames)
            {
                var game = gameEvents.Where(x => x.gameId == i);
                foreach(Event e in db.events)
                {
                    var events = game.Where(x => x.eventId == e.id);
                    GameStats gameStats = new GameStats();
                    gameStats.evt = e;
                    gameStats.count = events.Count();
                    gameStats.gameId = i;
                    totalStatistics.gameStats.Add(gameStats);
                    var x = new xpto();
                    x.count = gameStats.count;
                    x.eventId = e.id;
                    count.Add(x);
                }
            }

            foreach (Event e in db.events)
            {
                var x = new List<xpto>();
                x = count.Where(x => x.eventId == e.id).ToList();
                totalEvents = db.gameEvents.Where(x => x.eventId == e.id).ToList();

                TotalStats totalStats = new TotalStats();
                totalStats.evt = e;
                totalStats.count = totalEvents.Count();
                totalStats.average = x.Average(x => x.count);
                totalStats.standardDeviation = 1;
                totalStats.median = 1;
                totalStatistics.totalStats.Add(totalStats);
            }

            return totalStatistics;
        }
    }

    public class xpto
    {
        public int count { get; set; }
        public int eventId { get; set; }
    }
}
