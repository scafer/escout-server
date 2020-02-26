using escout.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace escout.Services
{
    public class AthleteService : BaseService
    {
        DataContext db;

        public AthleteService()
        {
            db = new DataContext();
        }

        public List<Athlete> CreateAthlete(List<Athlete> athlete)
        {
            db.athletes.AddRange(athlete);
            db.SaveChanges();
            return athlete;
        }

        public bool UpdateAthlete(Athlete athlete)
        {
            try
            {
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

        public List<Athlete> GetAthletes(FilterCriteria criteria)
        {
            string query = string.Format("SELECT * FROM athletes WHERE " + criteria.fieldName + criteria.condition + "'" + criteria.value + "';");
            return db.athletes.FromSqlRaw(query).ToList();
        }
    }
}
