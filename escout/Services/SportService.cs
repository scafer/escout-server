using escout.Helpers;
using escout.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace escout.Services
{
    public class SportService : BaseService
    {
        private readonly DataContext db;

        public SportService()
        {
            db = new DataContext();
        }

        public List<Sport> CreateSport(List<Sport> sport)
        {
            sport.ToList().ForEach(s => s.created = Utils.GetDateTime());
            sport.ToList().ForEach(s => s.updated = Utils.GetDateTime());
            db.sports.AddRange(sport);
            db.SaveChanges();
            return sport;
        }

        public bool UpdateSport(Sport sport)
        {
            try
            {
                sport.updated = Utils.GetDateTime();
                db.sports.Update(sport);
                db.SaveChanges();
                return true;
            }
            catch { return false; }
        }

        public bool RemoveSport(int id)
        {
            try
            {
                var sport = db.sports.FirstOrDefault(s => s.id == id);
                db.sports.Remove(sport);
                db.SaveChanges();
                return true;
            }
            catch { return false; }
        }

        public Sport GetSport(int id)
        {
            return db.sports.FirstOrDefault(s => s.id == id);
        }

        public List<Sport> GetSports(string query)
        {
            if (string.IsNullOrEmpty(query))
            {
                return db.sports.ToList();
            }
            else
            {
                var criteria = JsonConvert.DeserializeObject<FilterCriteria>(query);
                string q = string.Format("SELECT * FROM sports WHERE " + criteria.fieldName + " " + criteria.condition + " '" + criteria.value + "';");
                return db.sports.FromSqlRaw(q).ToList();
            }
        }
    }
}