using escout.Models;
using escout.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace escout.Controllers
{
    [Route("api/v1")]
    [ApiController]
    public class FavoritesController : ControllerBase
    {
        private readonly DataContext context;
        public FavoritesController(DataContext context) => this.context = context;

        /// <summary>
        /// Toogle favorite.
        /// </summary>
        [HttpPost]
        [Authorize]
        [Route("favorite")]
        public ActionResult<SvcResult> ToogleFavorite(Favorite favorite)
        {
            var currentUser = User.GetUser(new UserService(context));
            favorite.userId = currentUser.id;
            using var service = new FavoritesService(context);
            return service.ToogleFavorite(favorite) ? SvcResult.Set(0, "Success") : SvcResult.Set(1, "Error");
        }

        /// <summary>
        /// Get favorite.
        /// </summary>
        [HttpGet]
        [Authorize]
        [Route("favorite")]
        public ActionResult<List<Favorite>> GetFavorite(string query)
        {
            try
            {
                var currentUser = User.GetUser(new UserService(context));
                using var service = new FavoritesService(context);
                return service.GetFavorite(currentUser.id, query);
            }
            catch
            {
                return new NotFoundResult();
            }
        }

        /// <summary>
        /// Get favorites.
        /// </summary>
        [HttpGet]
        [Authorize]
        [Route("favorites")]
        public ActionResult<List<Favorite>> GetFavorites(string query)
        {
            try
            {
                var currentUser = User.GetUser(new UserService(context));
                using var service = new FavoritesService(context);
                return service.GetFavorites(currentUser.id, query);
            }
            catch
            {
                return new NotFoundResult();
            }
        }
    }
}
