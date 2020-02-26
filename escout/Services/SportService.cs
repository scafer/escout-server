using escout.Models;
using System.Collections.Generic;
using System.Linq;

namespace escout.Services
{
    public class SportService : BaseService
    {
        private DataContext db;

        public SportService()
        {
            db = new DataContext();
        }

        public List<Sport> CreateSport(List<Sport> sport)
        {
            db.sports.AddRange(sport);
            db.SaveChanges();
            return sport;
        }

        public bool UpdateSport(Sport sport)
        {
            try
            {
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

        public List<Sport> GetSports()
        {
            return db.sports.ToList();
        }
    }
}
