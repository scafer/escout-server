using escout.Models;
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
        private readonly DataContext context;
        public FavoritesController(DataContext context)
        {
            this.context = context;
        }

        [HttpPost]
        [Route("favorite")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult ToogleFavorite(Favorite favorite)
        {
            try
            {
                favorite.userId = User.GetUser(context).id;

                if (context.favorites.FirstOrDefault(a => a.userId == favorite.userId && a.athleteId == favorite.athleteId) != null && favorite.athleteId != null)
                {
                    var f = context.favorites.FirstOrDefault(a => a.userId == favorite.userId && a.athleteId == favorite.athleteId);
                    context.favorites.Remove(f);
                    context.SaveChanges();
                    return Ok();
                }
                else if (context.favorites.FirstOrDefault(a => a.userId == favorite.userId && a.clubId == favorite.clubId) != null && favorite.clubId != null)
                {
                    var f = context.favorites.FirstOrDefault(a => a.userId == favorite.userId && a.clubId == favorite.clubId);
                    context.favorites.Remove(f);
                    context.SaveChanges();
                    return Ok();
                }
                else if (context.favorites.FirstOrDefault(a => a.userId == favorite.userId && a.competitionId == favorite.competitionId) != null && favorite.competitionId != null)
                {
                    var f = context.favorites.FirstOrDefault(a => a.userId == favorite.userId && a.competitionId == favorite.competitionId);
                    context.favorites.Remove(f);
                    context.SaveChanges();
                    return Ok();
                }
                else if (context.favorites.FirstOrDefault(a => a.userId == favorite.userId && a.gameId == favorite.gameId) != null && favorite.gameId != null)
                {
                    var f = context.favorites.FirstOrDefault(a => a.userId == favorite.userId && a.gameId == favorite.gameId);
                    context.favorites.Remove(f);
                    context.SaveChanges();
                    return Ok();
                }
                else
                {
                    context.favorites.Add(favorite);
                    context.SaveChanges();
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
                    favorites = context.favorites.ToList();
                else
                {
                    var criteria = JsonConvert.DeserializeObject<FilterCriteria>(query);
                    var q = string.Format("SELECT * FROM favorites WHERE " + "userId=" + User.GetUser(context).id + " AND " + criteria.fieldName + " " + criteria.condition + " '" + criteria.value + "';");
                    favorites = context.favorites.FromSqlRaw(q).ToList();
                }

                return favorites;
            }
            catch
            {
                return new NotFoundResult();
            }
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
                    favorites = context.favorites.ToList();
                else
                {
                    var q = string.Format("SELECT * FROM favorites WHERE \"userId\"=" + User.GetUser(context).id + " AND \"" + query + "\" IS NOT NULL;");
                    favorites = context.favorites.FromSqlRaw(q).ToList();
                }

                return favorites;
            }
            catch
            {
                return new NotFoundResult();
            }
        }
    }
}
