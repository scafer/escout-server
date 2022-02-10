using System;
using System.Collections.Generic;
using System.Linq;
using escout.Helpers;
using escout.Models.Database;
using escout.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace escout.Controllers.GameObjects
{
    [Authorize]
    [ApiController]
    [Route("api/v1/game-object")]
    public class GameController : ControllerBase
    {
        private readonly DataContext dataContext;

        public GameController(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        [HttpPost]
        [Route("game")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<Game>> CreateGame(List<Game> game)
        {
            var user = User.GetUser(dataContext);

            if (user.accessLevel >= 3)
            {
                return Forbid();
            }

            game.ToList().ForEach(g => g.created = GenericUtils.GetDateTime());
            game.ToList().ForEach(g => g.updated = GenericUtils.GetDateTime());
            game.ToList().ForEach(g => g.userId = user.id);
            dataContext.games.AddRange(game);
            dataContext.SaveChanges();
            return game;
        }

        [HttpPut]
        [Route("game")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateGame(Game game)
        {
            if (User.GetUser(dataContext).accessLevel >= 3)
            {
                return Forbid();
            }

            try
            {
                game.updated = GenericUtils.GetDateTime();
                dataContext.games.Update(game);
                dataContext.SaveChanges();
                return Ok();
            }
            catch { return BadRequest(); }
        }

        [HttpDelete]
        [Route("game")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DeleteGame(int id)
        {
            if (User.GetUser(dataContext).accessLevel >= 3)
            {
                return Forbid();
            }

            try
            {
                var game = dataContext.games.FirstOrDefault(g => g.id == id);
                dataContext.games.Remove(game);
                dataContext.SaveChanges();
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("game")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<Game> GetGame(int id)
        {
            var game = dataContext.games.FirstOrDefault(g => g.id == id);
            game.status = GetGameStatus(game);
            game.displayOptions = GetGameDisplayOptions(game);
            return game;
        }

        [HttpGet]
        [Route("games")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<Game>> GetGames(string query)
        {
            try
            {
                List<Game> games;

                if (string.IsNullOrEmpty(query))
                {
                    games = dataContext.games.ToList();
                }
                else
                {
                    var criteria = JsonConvert.DeserializeObject<FilterCriteria>(query);
                    var q = string.Format(ConstValues.QUERY, "games", criteria.fieldName, criteria.condition, criteria.value);
                    games = dataContext.games.FromSqlRaw(q).ToList();
                }

                foreach (var game in games)
                {
                    game.status = GetGameStatus(game);
                    game.displayOptions = GetGameDisplayOptions(game);
                }

                return games;
            }
            catch
            {
                return new NotFoundResult();
            }
        }

        [HttpPost]
        [Route("game-event")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult CreateGameEvent(List<GameEvent> gameEvent)
        {
            gameEvent.ToList().ForEach(g => g.userId = User.GetUser(dataContext).id);
            gameEvent.ToList().ForEach(g => g.created = GenericUtils.GetDateTime());
            gameEvent.ToList().ForEach(g => g.updated = GenericUtils.GetDateTime());
            dataContext.gameEvents.AddRange(gameEvent);
            dataContext.SaveChanges();
            return gameEvent.Count > 0 ? Ok() : BadRequest();
        }

        [HttpPut]
        [Route("game-event")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateGameEvent(GameEvent gameEvent)
        {
            try
            {
                gameEvent.updated = GenericUtils.GetDateTime();
                dataContext.gameEvents.Update(gameEvent);
                dataContext.SaveChanges();
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        [Route("game-event")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DeleteGameEvent(int id)
        {
            try
            {
                var gameEvent = dataContext.gameEvents.FirstOrDefault(g => g.id == id);
                dataContext.gameEvents.Remove(gameEvent);
                dataContext.SaveChanges();
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("game-event")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<GameEvent> GetGameEvent(int id)
        {
            var gameEvent = dataContext.gameEvents.FirstOrDefault(g => g.id == id);
            gameEvent.displayOptions = GetGameEventDisplayOptions(gameEvent);
            return gameEvent;
        }

        [HttpGet]
        [Route("game-events")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<GameEvent>> GetGameEvents(int gameId)
        {
            var gameEvents = dataContext.gameEvents.Where(g => g.gameId == gameId).ToList();

            foreach (var gameEvent in gameEvents)
            {
                gameEvent.displayOptions = GetGameEventDisplayOptions(gameEvent);
            }

            return gameEvents;
        }

        [HttpPost]
        [Route("game-athlete")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<GameAthlete>> CreateGameAthlete(List<GameAthlete> gameAthlete)
        {
            gameAthlete.ToList().ForEach(g => g.created = GenericUtils.GetDateTime());
            gameAthlete.ToList().ForEach(g => g.updated = GenericUtils.GetDateTime());
            dataContext.gameAthletes.AddRange(gameAthlete);
            dataContext.SaveChanges();
            return gameAthlete;
        }

        [HttpPut]
        [Route("game-athlete")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateGameAthlete(GameAthlete gameAthlete)
        {
            try
            {
                gameAthlete.updated = GenericUtils.GetDateTime();
                dataContext.gameAthletes.Update(gameAthlete);
                dataContext.SaveChanges();
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        [Route("game-athlete")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DeleteGameAthlete(int id)
        {
            try
            {
                var gameAthlete = dataContext.gameAthletes.FirstOrDefault(g => g.id == id);
                dataContext.gameAthletes.Remove(gameAthlete);
                dataContext.SaveChanges();
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("game-athletes")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<GameAthlete>> GetGameAthletes(int gameId)
        {
            return dataContext.gameAthletes.Where(g => g.gameId == gameId).ToList();
        }

        [HttpGet]
        [Route("game-user")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<GameUser>> GetGameUser(string query)
        {
            try
            {
                List<GameUser> gamesUsers;

                if (string.IsNullOrEmpty(query))
                {
                    gamesUsers = dataContext.gameUsers.ToList();
                }
                else
                {
                    var criteria = JsonConvert.DeserializeObject<FilterCriteria>(query);
                    var q = string.Format(ConstValues.QUERY, "gameUsers", criteria.fieldName, criteria.condition, criteria.value);
                    gamesUsers = dataContext.gameUsers.FromSqlRaw(q).ToList();
                }

                return gamesUsers;
            }
            catch
            {
                return new NotFoundResult();
            }
        }

        [HttpPost]
        [Route("game-user")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<GameUser>> CreateGameUser(List<GameUser> gameUsers)
        {
            foreach (var gameUser in gameUsers)
            {
                var obj = dataContext.gameUsers.Where(c => c.gameId == gameUser.gameId && c.userId == gameUser.userId && c.athleteId == gameUser.athleteId).ToList();
                if (obj.Count != 0)
                {
                    gameUsers.Remove(gameUser);
                }
            }

            gameUsers.ToList().ForEach(g => g.created = GenericUtils.GetDateTime());
            gameUsers.ToList().ForEach(g => g.updated = GenericUtils.GetDateTime());
            dataContext.gameUsers.AddRange(gameUsers);
            dataContext.SaveChanges();
            return gameUsers;
        }

        [HttpPut]
        [Route("game-user")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateGameUser(GameUser gameUser)
        {
            try
            {
                gameUser.updated = GenericUtils.GetDateTime();
                dataContext.gameUsers.Update(gameUser);
                dataContext.SaveChanges();
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        [Route("game-user")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DeleteGameUser(GameUser gameUser)
        {
            try
            {
                dataContext.gameUsers.Remove(gameUser);
                dataContext.SaveChanges();
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("athlete-game-events")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<GameEvent>> AthleteGameEvents(int athleteId, int gameId)
        {
            return dataContext.gameEvents.Where(g => g.athleteId == athleteId && g.gameId == gameId).ToList();
        }

        [HttpGet]
        [Route("game-data")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<GameData> GetGameData(int gameId)
        {
            var gameData = new GameData();
            gameData.game = dataContext.games.FirstOrDefault(g => g.id == gameId);
            gameData.game.status = GetGameStatus(gameData.game);
            gameData.clubs = dataContext.clubs.Where(t => t.id == gameData.game.homeId || t.id == gameData.game.visitorId).ToList();
            gameData.athletes = dataContext.athletes.Where(t => t.clubId == gameData.game.homeId || t.clubId == gameData.game.visitorId).ToList();

            if (gameData.game.competitionId != null)
            {
                gameData.competition = dataContext.competitions.FirstOrDefault(t => t.id == gameData.game.competitionId);
                gameData.sport = dataContext.sports.FirstOrDefault(t => t.id == gameData.competition.sportId);
            }
            else
            {
                gameData.sport = dataContext.sports.FirstOrDefault(t => t.name == "Soccer");
            }

            gameData.events = dataContext.events.Where(t => t.sportId == gameData.sport.id).ToList();
            gameData.gameEvents = dataContext.gameEvents.Where(t => t.gameId == gameId).ToList();

            UpdateGameStatus();
            return gameData;
        }

        private void UpdateGameStatus()
        {
            var games = dataContext.games.Where(g => g.status == 0 || g.status == 1).ToList();
            foreach (var game in games)
            {
                game.status = GetGameStatus(game);
                dataContext.games.Update(game);
                dataContext.SaveChanges();
            }
        }

        private int GetGameStatus(Game game)
        {
            try
            {
                var actualTime = DateTime.Parse(GenericUtils.GetDateTime());
                var gameStart = DateTime.Parse(game.timeStart);
                var gameEnd = DateTime.Parse(game.timeEnd);

                if (DateTime.Compare(actualTime, gameStart) < 0)
                {
                    return ConstValues.GS_PENDING;
                }
                else if (DateTime.Compare(actualTime, gameEnd) > 0)
                {
                    return ConstValues.GS_FINISHED;
                }
                else if ((DateTime.Compare(actualTime, gameStart) >= 0) && (DateTime.Compare(actualTime, gameEnd) <= 0))
                {
                    return ConstValues.GS_ACTIVE;
                }
                else
                {
                    return game.status;
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }
            return game.status;
        }

        private Dictionary<string, string> GetGameDisplayOptions(Game game)
        {
            var displayOptions = new Dictionary<string, string>();

            if (game.imageId != null)
            {
                var imageUrl = dataContext.images.FirstOrDefault(a => a.id == game.imageId).imageUrl;
                displayOptions.Add(ConstValues.DO_IMAGE_URL, imageUrl);
            }

            if (game.competitionId != null)
            {
                var competitionName = dataContext.competitions.FirstOrDefault(a => a.id == game.competitionId).name;
                displayOptions.Add(ConstValues.DO_COMPETITION_NAME, competitionName);
            }

            if (game.userId != 0)
            {
                var userName = dataContext.users.FirstOrDefault(a => a.id == game.userId).username;
                displayOptions.Add(ConstValues.DO_USER_NAME, userName);
            }

            if (game.homeId != 0)
            {
                var homeClub = dataContext.clubs.FirstOrDefault(a => a.id == game.homeId);
                displayOptions.Add(ConstValues.DO_HOME_CLUB_NAME, homeClub.name);

                if (homeClub.imageId != null)
                {
                    var imageUrl = dataContext.images.FirstOrDefault(a => a.id == homeClub.imageId).imageUrl;
                    displayOptions.Add(ConstValues.DO_HOME_IMAGE_URL, imageUrl);
                }

            }

            if (game.visitorId != 0)
            {
                var visitorClub = dataContext.clubs.FirstOrDefault(a => a.id == game.visitorId);
                displayOptions.Add(ConstValues.DO_VISITOR_CLUB_NAME, visitorClub.name);

                if (visitorClub.imageId != null)
                {
                    var imageUrl = dataContext.images.FirstOrDefault(a => a.id == visitorClub.imageId).imageUrl;
                    displayOptions.Add(ConstValues.DO_VISITOR_IMAGE_URL, imageUrl);
                }
            }

            return displayOptions;
        }

        private Dictionary<string, string> GetGameEventDisplayOptions(GameEvent gameEvent)
        {
            var displayOptions = new Dictionary<string, string>();

            if (gameEvent.athleteId != null)
            {
                var athleteName = dataContext.athletes.FirstOrDefault(a => a.id == gameEvent.athleteId).name;
                displayOptions.Add(ConstValues.DO_ATHLETE_NAME, athleteName);
            }

            if (gameEvent.clubId != null)
            {
                var clubName = dataContext.athletes.FirstOrDefault(a => a.id == gameEvent.clubId).name;
                displayOptions.Add(ConstValues.DO_CLUB_NAME, clubName);
            }

            if (gameEvent.eventId != 0)
            {
                var eventName = dataContext.events.FirstOrDefault(a => a.id == gameEvent.eventId).name;
                displayOptions.Add(ConstValues.DO_EVENT_NAME, eventName);
            }

            return displayOptions;
        }
    }
}
