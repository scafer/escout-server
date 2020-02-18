using escout.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace escout.Services
{
    public class GameService : BaseService
    {
        DataContext db;

        public GameService()
        {
            db = new DataContext();
        }

        public Game CreateGame(Game game)
        {
            db.games.Add(game);
            db.SaveChanges();
            return game;
        }

        public bool UpdateGame(Game game)
        {
            try
            {
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

        public List<Game> GetGames()
        {
            return db.games.ToList();
        }

        public GameEvent CreateGameEvent(GameEvent gameEvent)
        {
            db.gameEvents.Add(gameEvent);
            db.SaveChanges();
            return gameEvent;
        }

        public bool UpdateGameEvent(GameEvent gameEvent)
        {
            try
            {
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
            return db.gameEvents.ToList(); //TODO: Implement filter
        }

        public GameAthlete CreateGameAthlete(GameAthlete gameAthlete)
        {
            db.gameAthletes.Add(gameAthlete);
            db.SaveChanges();
            return gameAthlete;
        }

        public bool UpdateGameAthlete(GameAthlete gameAthlete)
        {
            try
            {
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
            return db.gameAthletes.ToList(); //TODO: Implement filter
        }
    }
}