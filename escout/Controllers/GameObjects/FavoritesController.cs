using escout.Helpers;
using escout.Models.Database;
using escout.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace escout.Controllers.GameObjects
{
    [Authorize]
    [ApiController]
    [Route("api/v1/game-object")]
    public class FavoritesController : ControllerBase
    {
        private readonly DataContext dataContext;
        public FavoritesController(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        [HttpPost]
        [Route("favorite")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult ToogleFavorite(Favorite favorite)
        {
            try
            {
                favorite.userId = User.GetUser(dataContext).id;

                if (dataContext.favorites.FirstOrDefault(a => a.userId == favorite.userId && a.athleteId == favorite.athleteId) != null && favorite.athleteId != null)
                {
                    var f = dataContext.favorites.FirstOrDefault(a => a.userId == favorite.userId && a.athleteId == favorite.athleteId);
                    dataContext.favorites.Remove(f);
                    dataContext.SaveChanges();
                    return Ok();
                }
                else if (dataContext.favorites.FirstOrDefault(a => a.userId == favorite.userId && a.clubId == favorite.clubId) != null && favorite.clubId != null)
                {
                    var f = dataContext.favorites.FirstOrDefault(a => a.userId == favorite.userId && a.clubId == favorite.clubId);
                    dataContext.favorites.Remove(f);
                    dataContext.SaveChanges();
                    return Ok();
                }
                else if (dataContext.favorites.FirstOrDefault(a => a.userId == favorite.userId && a.competitionId == favorite.competitionId) != null && favorite.competitionId != null)
                {
                    var f = dataContext.favorites.FirstOrDefault(a => a.userId == favorite.userId && a.competitionId == favorite.competitionId);
                    dataContext.favorites.Remove(f);
                    dataContext.SaveChanges();
                    return Ok();
                }
                else if (dataContext.favorites.FirstOrDefault(a => a.userId == favorite.userId && a.gameId == favorite.gameId) != null && favorite.gameId != null)
                {
                    var f = dataContext.favorites.FirstOrDefault(a => a.userId == favorite.userId && a.gameId == favorite.gameId);
                    dataContext.favorites.Remove(f);
                    dataContext.SaveChanges();
                    return Ok();
                }
                else
                {
                    dataContext.favorites.Add(favorite);
                    dataContext.SaveChanges();
                }
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("favorite")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<Favorite>> GetFavorite(string query)
        {
            try
            {
                List<Favorite> favorites;

                if (string.IsNullOrEmpty(query))
                {
                    favorites = dataContext.favorites.ToList();
                }
                else
                {
                    var criteria = JsonConvert.DeserializeObject<FilterCriteria>(query);
                    var q = string.Format(ConstValues.QUERY_WITH_USER_ID, "favorites", User.GetUser(dataContext).id, criteria.fieldName, criteria.condition, criteria.value);
                    favorites = dataContext.favorites.FromSqlRaw(q).ToList();
                }

                return favorites.OrderBy(x => x.id).ToList();
            }
            catch { return new NotFoundResult(); }
        }

        [HttpGet]
        [Route("favorites")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<Favorite>> GetFavorites(string query)
        {
            try
            {
                List<Favorite> favorites;

                if (string.IsNullOrEmpty(query))
                    favorites = dataContext.favorites.ToList();
                else
                {
                    var q = string.Format(ConstValues.QUERY_NOT_NULL, "favorites", User.GetUser(dataContext).id, query);
                    favorites = dataContext.favorites.FromSqlRaw(q).ToList();
                }

                return favorites.OrderBy(x => x.id).ToList();
            }
            catch { return new NotFoundResult(); }
        }
    }
}
