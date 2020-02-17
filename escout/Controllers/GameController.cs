using escout.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace escout.Controllers
{
    [Route("api/v1")]
    [ApiController]
    public class GameController : ControllerBase
    {
        /// <summary>
        /// Create game.
        /// </summary>
        [HttpPost]
        [Authorize]
        [Route("game")]
        public ActionResult<SvcResult> AddGame(Game game)
        {
            return new NotFoundResult();
        }

        /// <summary>
        /// Update game.
        /// </summary>
        [HttpPut]
        [Authorize]
        [Route("game")]
        public ActionResult<SvcResult> UpdateGame(Game game)
        {
            return new NotFoundResult();
        }

        /// <summary>
        /// Delete game.
        /// </summary>
        [HttpDelete]
        [Authorize]
        [Route("game")]
        public ActionResult<SvcResult> DeleteGame(int id)
        {
            return new NotFoundResult();
        }

        /// <summary>
        /// Get game.
        /// </summary>
        [HttpGet]
        [Authorize]
        [Route("game")]
        public ActionResult<Game> GetGame(int id)
        {
            return new NotFoundResult();
        }

        /// <summary>
        /// Get games.
        /// </summary>
        [HttpGet]
        [Authorize]
        [Route("games")]
        public ActionResult<List<Game>> GetGames()
        {
            return new NotFoundResult();
        }
    }
}