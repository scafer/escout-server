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
        private readonly DataContext context;
        public SportService(DataContext context) => this.context = context;

        public List<Sport> CreateSport(List<Sport> sport)
        {
            sport.ToList().ForEach(s => s.created = Utils.GetDateTime());
            sport.ToList().ForEach(s => s.updated = Utils.GetDateTime());
            context.sports.AddRange(sport);
            context.SaveChanges();
            return sport;
        }

        public bool UpdateSport(Sport sport)
        {
            try
            {
                sport.updated = Utils.GetDateTime();
                context.sports.Update(sport);
                context.SaveChanges();
                return true;
            }
            catch { return false; }
        }

        public bool RemoveSport(int id)
        {
            try
            {
                var sport = context.sports.FirstOrDefault(s => s.id == id);
                context.sports.Remove(sport);
                context.SaveChanges();
                return true;
            }
            catch { return false; }
        }

        public Sport GetSport(int id)
        {
            return context.sports.FirstOrDefault(s => s.id == id);
        }

        public List<Sport> GetSports(string query)
        {
            List<Sport> sports;

            if (string.IsNullOrEmpty(query))
                sports = context.sports.ToList();
            else
            {
                var criteria = JsonConvert.DeserializeObject<FilterCriteria>(query);
                var q = string.Format("SELECT * FROM sports WHERE " + criteria.fieldName + " " + criteria.condition + " '" + criteria.value + "';");
                sports = context.sports.FromSqlRaw(q).ToList();
            }

            return sports;
        }
    }
}