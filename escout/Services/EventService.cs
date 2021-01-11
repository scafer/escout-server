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
        private readonly DataContext context;
        public EventService(DataContext context) => this.context = context;

        public List<Event> CreateEvent(List<Event> e)
        {
            e.ToList().ForEach(c => c.created = Utils.GetDateTime());
            e.ToList().ForEach(c => c.updated = Utils.GetDateTime());
            context.events.AddRange(e);
            context.SaveChanges();
            return e;
        }

        public bool UpdateEvent(Event e)
        {
            try
            {
                e.updated = Utils.GetDateTime();
                context.events.Update(e);
                context.SaveChanges();
                return true;
            }
            catch { return false; }
        }

        public bool RemoveEvent(int id)
        {
            try
            {
                var evt = context.events.FirstOrDefault(e => e.id == id);
                context.events.Remove(evt);
                context.SaveChanges();
                return true;
            }
            catch { return false; }
        }

        public Event GetEvent(int id)
        {
            return context.events.FirstOrDefault(e => e.id == id);
        }

        public List<Event> GetEvents(string query)
        {
            List<Event> events;

            if (string.IsNullOrEmpty(query))
                events = context.events.ToList();
            else
            {
                var criteria = JsonConvert.DeserializeObject<FilterCriteria>(query);
                var q = string.Format("SELECT * FROM events WHERE " + criteria.fieldName + " " + criteria.condition + " '" + criteria.value + "';");
                events = context.events.FromSqlRaw(q).ToList();
            }

            return events;
        }
    }
}