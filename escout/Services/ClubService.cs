using escout.Models;
using System.Collections.Generic;
using System.Linq;

namespace escout.Services
{
    public class ClubService : BaseService
    {
        DataContext db;

        public ClubService()
        {
            db = new DataContext();
        }

        public Club CreateClub(Club club)
        {
            db.clubs.Add(club);
            db.SaveChanges();
            return club;
        }

        public bool UpdateClub(Club club)
        {
            try
            {
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

        public List<Club> GetClubs()
        {
            return db.clubs.ToList();
        }
    }
}
