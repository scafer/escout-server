using escout.Models;
using escout.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;

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
        public ActionResult<List<Game>> CreateGame(List<Game> game)
        {
            using var service = new GameService();
            return service.CreateGame(game);
        }

        /// <summary>
        /// Update game.
        /// </summary>
        [HttpPut]
        [Authorize]
        [Route("game")]
        public ActionResult<SvcResult> UpdateGame(Game game)
        {
            using var service = new GameService();
            return service.UpdateGame(game) ? SvcResult.Set(0, "Success") : SvcResult.Set(1, "Error");
        }

        /// <summary>
        /// Delete game.
        /// </summary>
        [HttpDelete]
        [Authorize]
        [Route("game")]
        public ActionResult<SvcResult> DeleteGame(int id)
        {
            using var service = new GameService();
            return service.DeleteGame(id) ? SvcResult.Set(0, "Success") : SvcResult.Set(1, "Error");
        }

        /// <summary>
        /// Get game.
        /// </summary>
        [HttpGet]
        [Authorize]
        [Route("game")]
        public ActionResult<Game> GetGame(int id)
        {
            using var service = new GameService();
            return service.GetGame(id);
        }

        /// <summary>
        /// Get games.
        /// </summary>
        [HttpGet]
        [Authorize]
        [Route("games")]
        public ActionResult<List<Game>> GetGames(string query)
        {
            try
            {
                var criteria = JsonConvert.DeserializeObject<FilterCriteria>(query);
                using var service = new GameService();
                return service.GetGames(criteria);
            }
            catch
            {
                return new NotFoundResult();
            }
        }

        [HttpPost]
        [Authorize]
        [Route("gameEvent")]
        public ActionResult<List<GameEvent>> CreateGameEvent(List<GameEvent> gameEvent)
        {
            using var service = new GameService();
            return service.CreateGameEvent(gameEvent);
        }

        [HttpPut]
        [Authorize]
        [Route("gameEvent")]
        public ActionResult<SvcResult> UpdateGameEvent(GameEvent gameEvent)
        {
            using var service = new GameService();
            return service.UpdateGameEvent(gameEvent) ? SvcResult.Set(0, "Success") : SvcResult.Set(1, "Error");
        }

        [HttpDelete]
        [Authorize]
        [Route("gameEvent")]
        public ActionResult<SvcResult> DeleteGameEvent(int id)
        {
            using var service = new GameService();
            return service.DeleteGameEvent(id) ? SvcResult.Set(0, "Success") : SvcResult.Set(1, "Error");
        }

        [HttpGet]
        [Authorize]
        [Route("gameEvent")]
        public ActionResult<GameEvent> GetGameEvent(int id)
        {
            using var service = new GameService();
            return service.GetGameEvent(id);
        }

        [HttpGet]
        [Authorize]
        [Route("gameEvents")]
        public ActionResult<List<GameEvent>> GetGameEvents(int gameId)
        {
            using var service = new GameService();
            return service.GetGameEvents(gameId);
        }

        [HttpPost]
        [Authorize]
        [Route("gameAthlete")]
        public ActionResult<List<GameAthlete>> CreateGameAthlete(List<GameAthlete> gameAthlete)
        {
            using var service = new GameService();
            return service.CreateGameAthlete(gameAthlete);
        }

        [HttpPut]
        [Authorize]
        [Route("gameAthlete")]
        public ActionResult<SvcResult> UpdateGameAthlete(GameAthlete gameAthlete)
        {
            using var service = new GameService();
            return service.UpdateGameAthlete(gameAthlete) ? SvcResult.Set(0, "Success") : SvcResult.Set(1, "Error");
        }

        [HttpDelete]
        [Authorize]
        [Route("gameAthlete")]
        public ActionResult<SvcResult> DeleteGameAthlete(int id)
        {
            using var service = new GameService();
            return service.DeleteGameAthlete(id) ? SvcResult.Set(0, "Success") : SvcResult.Set(1, "Error");
        }

        [HttpGet]
        [Authorize]
        [Route("gameAthletes")]
        public ActionResult<List<GameAthlete>> GetGameAthletes(int gameId)
        {
            using var service = new GameService();
            return service.GetGamesAthletes(gameId);
        }

        [HttpGet]
        [Authorize]
        [Route("gameData")]
        public ActionResult<GameData> GetGameData(int gameId)
        {
            using var service = new GameService();
            return service.GetGameData(gameId);
        }
    }
}