using escout.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace escout.Services
{
    public class FavoritesService : BaseService
    {
        readonly DataContext db;

        public FavoritesService() => db = new DataContext();

        public bool ToogleFavorite(Favorite favorite)
        {
            try
            {
                if (db.favorites.FirstOrDefault(a => a.userId == favorite.userId && a.athleteId == favorite.athleteId) == null)
                {
                    db.favorites.Add(favorite);
                    db.SaveChanges();
                }
                else if (db.favorites.FirstOrDefault(a => a.userId == favorite.userId && a.clubId == favorite.clubId) == null)
                {
                    db.favorites.Add(favorite);
                    db.SaveChanges();
                }
                else if (db.favorites.FirstOrDefault(a => a.userId == favorite.userId && a.competitionId == favorite.competitionId) == null)
                {
                    db.favorites.Add(favorite);
                    db.SaveChanges();
                }
                else if (db.favorites.FirstOrDefault(a => a.userId == favorite.userId && a.gameId == favorite.gameId) == null)
                {
                    db.favorites.Add(favorite);
                    db.SaveChanges();
                }
                else
                {
                    var f = db.favorites.FirstOrDefault(a => a.id == favorite.id);
                    db.favorites.Remove(f);
                    db.SaveChanges();
                    return true;
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
                favorites = db.favorites.ToList();
            else
            {
                var criteria = JsonConvert.DeserializeObject<FilterCriteria>(query);
                var q = string.Format("SELECT * FROM favorites WHERE " + "userId=" + userId + " AND " + criteria.fieldName + " " + criteria.condition + " '" + criteria.value + "';");
                favorites = db.favorites.FromSqlRaw(q).ToList();
            }

            return favorites;
        }

        public List<Favorite> GetFavorites(int userId, string query)
        {
            List<Favorite> favorites;

            if (string.IsNullOrEmpty(query))
                favorites = db.favorites.ToList();
            else
            {
                var criteria = JsonConvert.DeserializeObject<FilterCriteria>(query);
                var q = string.Format("SELECT * FROM favorites WHERE " + "userId=" + userId + " AND " + criteria.fieldName + " IS NOT NULL;");
                favorites = db.favorites.FromSqlRaw(q).ToList();
            }

            return favorites;
        }
    }
}
