using escout.Helpers;
using escout.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace escout.Services
{
    public class EventService : BaseService
    {
        readonly DataContext db;

        public EventService() => db = new DataContext();

        public List<Event> CreateEvent(List<Event> e)
        {
            e.ToList().ForEach(c => c.created = Utils.GetDateTime());
            e.ToList().ForEach(c => c.updated = Utils.GetDateTime());
            db.events.AddRange(e);
            db.SaveChanges();
            return e;
        }

        public bool UpdateEvent(Event e)
        {
            try
            {
                e.updated = Utils.GetDateTime();
                db.events.Update(e);
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

        public List<Event> GetEvents(string query)
        {
            List<Event> events;

            if (string.IsNullOrEmpty(query))
                events = db.events.ToList();
            else
            {
                var criteria = JsonConvert.DeserializeObject<FilterCriteria>(query);
                var q = string.Format("SELECT * FROM events WHERE " + criteria.fieldName + " " + criteria.condition + " '" + criteria.value + "';");
                events = db.events.FromSqlRaw(q).ToList();
            }

            return events;
        }
    }
}