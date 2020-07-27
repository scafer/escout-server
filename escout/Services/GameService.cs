using escout.Helpers;
using escout.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace escout.Services
{
    public class GameService : BaseService
    {
        readonly DataContext db;

        public GameService() => db = new DataContext();

        public List<Game> CreateGame(List<Game> game)
        {
            game.ToList().ForEach(g => g.created = Utils.GetDateTime());
            game.ToList().ForEach(g => g.updated = Utils.GetDateTime());
            db.games.AddRange(game);
            db.SaveChanges();
            return game;
        }

        public bool UpdateGame(Game game)
        {
            try
            {
                game.updated = Utils.GetDateTime();
                db.games.Update(game);
                db.SaveChanges();
                return true;
            }
            catch { return false; }
        }

        public bool DeleteGame(int id)
        {
            try
            {
                var game = db.games.FirstOrDefault(g => g.id == id);
                db.games.Remove(game);
                db.SaveChanges();
                return true;
            }
            catch { return false; }
        }

        public Game GetGame(int id)
        {
            return db.games.FirstOrDefault(g => g.id == id);
        }

        public List<Game> GetGames(string query)
        {
            List<Game> games;

            if (string.IsNullOrEmpty(query))
                games = db.games.ToList();
            else
            {
                var criteria = JsonConvert.DeserializeObject<FilterCriteria>(query);
                var q = string.Format("SELECT * FROM games WHERE " + criteria.fieldName + " " + criteria.condition + " '" + criteria.value + "';");
                games = db.games.FromSqlRaw(q).ToList();
            }

            return games;
        }

        public List<GameEvent> CreateGameEvent(List<GameEvent> gameEvent, User user)
        {
            gameEvent.ToList().ForEach(g => g.userId = user.id);
            gameEvent.ToList().ForEach(g => g.created = Utils.GetDateTime());
            gameEvent.ToList().ForEach(g => g.updated = Utils.GetDateTime());
            db.gameEvents.AddRange(gameEvent);
            db.SaveChanges();
            return gameEvent;
        }

        public bool UpdateGameEvent(GameEvent gameEvent)
        {
            try
            {
                gameEvent.updated = Utils.GetDateTime();
                db.gameEvents.Update(gameEvent);
                db.SaveChanges();
                return true;
            }
            catch { return false; }
        }

        public bool DeleteGameEvent(int id)
        {
            try
            {
                var gameEvent = db.gameEvents.FirstOrDefault(g => g.id == id);
                db.gameEvents.Remove(gameEvent);
                db.SaveChanges();
                return true;
            }
            catch { return false; }
        }

        public GameEvent GetGameEvent(int id)
        {
            return db.gameEvents.FirstOrDefault(g => g.id == id);
        }

        public List<GameEvent> GetGameEvents(int gameId)
        {
            return db.gameEvents.Where(g => g.gameId == gameId).ToList();
        }

        public List<GameAthlete> CreateGameAthlete(List<GameAthlete> gameAthlete)
        {
            gameAthlete.ToList().ForEach(g => g.created = Utils.GetDateTime());
            gameAthlete.ToList().ForEach(g => g.updated = Utils.GetDateTime());
            db.gameAthletes.AddRange(gameAthlete);
            db.SaveChanges();
            return gameAthlete;
        }

        public bool UpdateGameAthlete(GameAthlete gameAthlete)
        {
            try
            {
                gameAthlete.updated = Utils.GetDateTime();
                db.gameAthletes.Update(gameAthlete);
                db.SaveChanges();
                return true;
            }
            catch { return false; }
        }

        public bool DeleteGameAthlete(int id)
        {
            try
            {
                var gameAthlete = db.gameAthletes.FirstOrDefault(g => g.id == id);
                db.gameAthletes.Remove(gameAthlete);
                db.SaveChanges();
                return true;
            }
            catch { return false; }
        }

        public List<GameAthlete> GetGamesAthletes(int gameId)
        {
            return db.gameAthletes.Where(g => g.gameId == gameId).ToList();
        }

        public GameData GetGameData(int gameId)
        {
            var gameData = new GameData();
            gameData.game = GetGame(gameId);
            gameData.clubs = db.clubs.Where(t => t.id == gameData.game.homeId || t.id == gameData.game.visitorId).ToList();
            gameData.athletes = db.athletes.Where(t => t.clubId == gameData.game.homeId || t.clubId == gameData.game.visitorId).ToList();
            gameData.competition = db.competitions.FirstOrDefault(t => t.id == gameData.game.competitionId);
            gameData.sport = db.sports.FirstOrDefault(t => t.id == gameData.competition.sportId);
            gameData.events = db.events.Where(t => t.sportId == gameData.sport.id).ToList();
            gameData.gameEvents = db.gameEvents.Where(t => t.gameId == gameId).ToList();

            return gameData;
        }

        public ActionResult<List<GameUser>> GetGameUsers(string query)
        {
            List<GameUser> gamesUsers;

            if (string.IsNullOrEmpty(query))
                gamesUsers = db.gameUsers.ToList();
            else
            {
                var criteria = JsonConvert.DeserializeObject<FilterCriteria>(query);
                var q = string.Format("SELECT * FROM gameUsers WHERE " + criteria.fieldName + " " + criteria.condition + " '" + criteria.value + "';");
                gamesUsers = db.gameUsers.FromSqlRaw(q).ToList();
            }

            return gamesUsers;
        }

        public ActionResult<List<GameUser>> CreateGameUser(List<GameUser> gameUsers)
        {
            gameUsers.ToList().ForEach(g => g.created = Utils.GetDateTime());
            gameUsers.ToList().ForEach(g => g.updated = Utils.GetDateTime());
            db.gameUsers.AddRange(gameUsers);
            db.SaveChanges();
            return gameUsers;
        }

        public bool UpdateGameUser(GameUser gameUser)
        {
            try
            {
                gameUser.updated = Utils.GetDateTime();
                db.gameUsers.Update(gameUser);
                db.SaveChanges();
                return true;
            }
            catch { return false; }
        }

        internal ActionResult<List<GameEvent>> AthleteEvents(int athleteId, int numberOfGames)
        {
            return db.gameEvents.Where(g => g.athleteId == athleteId).Take(numberOfGames).ToList();
        }

        internal ActionResult<List<GameEvent>> AthleteGameEvents(int athleteId, int gameId)
        {
            return db.gameEvents.Where(g => g.athleteId == athleteId && g.gameId == gameId).ToList();
        }

        public bool DeleteGameUser(GameUser gameUser)
        {
            try
            {
                db.gameUsers.Remove(gameUser);
                db.SaveChanges();
                return true;
            }
            catch { return false; }
        }
    }
}