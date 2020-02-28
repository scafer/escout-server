﻿using escout.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using escout.Helpers;

namespace escout.Services
{
    public class GameService : BaseService
    {
        DataContext db;

        public GameService()
        {
            db = new DataContext();
        }

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

        public List<Game> GetGames(FilterCriteria criteria)
        {
            string query = string.Format("SELECT * FROM competitions WHERE " + criteria.fieldName + criteria.condition + "'" + criteria.value + "';");
            return db.games.FromSqlRaw(query).ToList();
        }

        public List<GameEvent> CreateGameEvent(List<GameEvent> gameEvent)
        {
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
            GameData gameData = new GameData();

            gameData.game = GetGame(gameId);
            gameData.clubs = db.clubs.Where(t => t.id == gameData.game.homeId || t.id == gameData.game.visitorId).ToList();
            gameData.athletes = db.athletes.Where(t => t.clubId == gameData.game.homeId || t.clubId == gameData.game.visitorId).ToList();
            gameData.competition = db.competitions.FirstOrDefault(t => t.id == gameData.game.competitionId);
            gameData.sport = db.sports.FirstOrDefault(t => t.id == gameData.competition.sportId);
            gameData.events = db.events.Where(t => t.sportId == gameData.sport.id).ToList();
            gameData.gameEvents = db.gameEvents.Where(t => t.gameId == gameId).ToList();

            return gameData;
        }
    }
}