using escout.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace escout.Services
{
    public class FavoritesService : BaseService
    {
        private readonly DataContext context;
        public FavoritesService(DataContext context) => this.context = context;

        public bool ToogleFavorite(Favorite favorite)
        {
            try
            {
                if (context.favorites.FirstOrDefault(a => a.userId == favorite.userId && a.athleteId == favorite.athleteId) != null && favorite.athleteId != null)
                {
                    var f = context.favorites.FirstOrDefault(a => a.userId == favorite.userId && a.athleteId == favorite.athleteId);
                    context.favorites.Remove(f);
                    context.SaveChanges();
                    return true;
                }
                else if (context.favorites.FirstOrDefault(a => a.userId == favorite.userId && a.clubId == favorite.clubId) != null && favorite.clubId != null)
                {
                    var f = context.favorites.FirstOrDefault(a => a.userId == favorite.userId && a.clubId == favorite.clubId);
                    context.favorites.Remove(f);
                    context.SaveChanges();
                    return true;
                }
                else if (context.favorites.FirstOrDefault(a => a.userId == favorite.userId && a.competitionId == favorite.competitionId) != null && favorite.competitionId != null)
                {
                    var f = context.favorites.FirstOrDefault(a => a.userId == favorite.userId && a.competitionId == favorite.competitionId);
                    context.favorites.Remove(f);
                    context.SaveChanges();
                    return true;
                }
                else if (context.favorites.FirstOrDefault(a => a.userId == favorite.userId && a.gameId == favorite.gameId) != null && favorite.gameId != null)
                {
                    var f = context.favorites.FirstOrDefault(a => a.userId == favorite.userId && a.gameId == favorite.gameId);
                    context.favorites.Remove(f);
                    context.SaveChanges();
                    return true;
                }
                else
                {
                    context.favorites.Add(favorite);
                    context.SaveChanges();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<Favorite> GetFavorite(int userId, string query)
        {
            List<Favorite> favorites;

            if (string.IsNullOrEmpty(query))
                favorites = context.favorites.ToList();
            else
            {
                var criteria = JsonConvert.DeserializeObject<FilterCriteria>(query);
                var q = string.Format("SELECT * FROM favorites WHERE " + "userId=" + userId + " AND " + criteria.fieldName + " " + criteria.condition + " '" + criteria.value + "';");
                favorites = context.favorites.FromSqlRaw(q).ToList();
            }

            return favorites;
        }

        public List<Favorite> GetFavorites(int userId, string query)
        {
            List<Favorite> favorites;

            if (string.IsNullOrEmpty(query))
                favorites = context.favorites.ToList();
            else
            {
                var q = string.Format("SELECT * FROM favorites WHERE \"userId\"=" + userId + " AND \"" + query + "\" IS NOT NULL;");
                favorites = context.favorites.FromSqlRaw(q).ToList();
            }

            return favorites;
        }
    }
}
