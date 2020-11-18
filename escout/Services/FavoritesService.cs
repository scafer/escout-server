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
                if (db.favorites.FirstOrDefault(a => a.userId == favorite.userId && a.athleteId == favorite.athleteId) != null && favorite.athleteId != null)
                {
                    var f = db.favorites.FirstOrDefault(a => a.userId == favorite.userId && a.athleteId == favorite.athleteId);
                    db.favorites.Remove(f);
                    db.SaveChanges();
                    return true;
                }
                else if (db.favorites.FirstOrDefault(a => a.userId == favorite.userId && a.clubId == favorite.clubId) != null && favorite.clubId != null)
                {
                    var f = db.favorites.FirstOrDefault(a => a.userId == favorite.userId && a.clubId == favorite.clubId);
                    db.favorites.Remove(f);
                    db.SaveChanges();
                    return true;
                }
                else if (db.favorites.FirstOrDefault(a => a.userId == favorite.userId && a.competitionId == favorite.competitionId) != null && favorite.competitionId != null)
                {
                    var f = db.favorites.FirstOrDefault(a => a.userId == favorite.userId && a.competitionId == favorite.competitionId);
                    db.favorites.Remove(f);
                    db.SaveChanges();
                    return true;
                }
                else if (db.favorites.FirstOrDefault(a => a.userId == favorite.userId && a.gameId == favorite.gameId) != null && favorite.gameId != null)
                {
                    var f = db.favorites.FirstOrDefault(a => a.userId == favorite.userId && a.gameId == favorite.gameId);
                    db.favorites.Remove(f);
                    db.SaveChanges();
                    return true;
                }
                else
                {
                    db.favorites.Add(favorite);
                    db.SaveChanges();
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
                var q = string.Format("SELECT * FROM favorites WHERE \"userId\"="+ userId + " AND \"" + query + "\" IS NOT NULL;");
                favorites = db.favorites.FromSqlRaw(q).ToList();
            }

            return favorites;
        }
    }
}
