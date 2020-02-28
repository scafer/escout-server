using escout.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using escout.Helpers;

namespace escout.Services
{
    public class ClubService : BaseService
    {
        DataContext db;

        public ClubService()
        {
            db = new DataContext();
        }

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

        public List<Club> GetClubs(FilterCriteria criteria)
        {
            string query = string.Format("SELECT * FROM clubs WHERE " + criteria.fieldName + criteria.condition + "'" + criteria.value + "';");
            return db.clubs.FromSqlRaw(query).ToList();
        }
    }
}
