using escout.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace escout.Services
{
    public class EventService : BaseService
    {
        DataContext db;

        public EventService()
        {
            db = new DataContext();
        }

        public List<Event> CreateEvent(List<Event> e)
        {
            db.events.AddRange(e);
            db.SaveChanges();
            return e;
        }

        public bool UpdateEvent(Event evt)
        {
            try
            {
                db.events.Update(evt);
                db.SaveChanges();
                return true;
            }
            catch { return false; }
        }

        public bool RemoveEvent(int id)
        {
            try
            {
                var evt = db.events.FirstOrDefault(e => e.id == id);
                db.events.Remove(evt);
                db.SaveChanges();
                return true;
            }
            catch { return false; }
        }

        public Event GetEvent(int id)
        {
            return db.events.FirstOrDefault(e => e.id == id);
        }

        public List<Event> GetEvents(FilterCriteria criteria)
        {
            string query = string.Format("SELECT * FROM events WHERE " + criteria.fieldName + criteria.condition + "'" + criteria.value + "';");
            return db.events.FromSqlRaw(query).ToList();
        }
    }
}