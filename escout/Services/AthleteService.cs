﻿using escout.Helpers;
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
    }
}
