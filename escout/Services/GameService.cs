using escout.Helpers;
using escout.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace escout.Services
{
    public class GameService : BaseService
    {
        private readonly DataContext context;
        public GameService(DataContext context) => this.context = context;

        public List<Game> CreateGame(List<Game> game)
        {
            game.ToList().ForEach(g => g.created = Utils.GetDateTime());
            game.ToList().ForEach(g => g.updated = Utils.GetDateTime());
            context.games.AddRange(game);
            context.SaveChanges();
            return game;
        }

        public bool UpdateGame(Game game)
        {
            try
            {
                game.updated = Utils.GetDateTime();
                context.games.Update(game);
                context.SaveChanges();
                return true;
            }
            catch { return false; }
        }

        public bool DeleteGame(int id)
        {
            try
            {
                var game = context.games.FirstOrDefault(g => g.id == id);
                context.games.Remove(game);
                context.SaveChanges();
                return true;
            }
            catch { return false; }
        }

        public Game GetGame(int id)
        {
            return context.games.FirstOrDefault(g => g.id == id);
        }

        public List<Game> GetGames(string query)
        {
            List<Game> games;

            if (string.IsNullOrEmpty(query))
                games = context.games.ToList();
            else
            {
                var criteria = JsonConvert.DeserializeObject<FilterCriteria>(query);
                var q = string.Format("SELECT * FROM games WHERE " + criteria.fieldName + " " + criteria.condition + " '" + criteria.value + "';");
                games = context.games.FromSqlRaw(q).ToList();
            }

            return games;
        }

        public List<GameEvent> CreateGameEvent(List<GameEvent> gameEvent, User user)
        {
            gameEvent.ToList().ForEach(g => g.userId = user.id);
            gameEvent.ToList().ForEach(g => g.created = Utils.GetDateTime());
            gameEvent.ToList().ForEach(g => g.updated = Utils.GetDateTime());
            context.gameEvents.AddRange(gameEvent);
            context.SaveChanges();
            return gameEvent;
        }

        public bool UpdateGameEvent(GameEvent gameEvent)
        {
            try
            {
                gameEvent.updated = Utils.GetDateTime();
                context.gameEvents.Update(gameEvent);
                context.SaveChanges();
                return true;
            }
            catch { return false; }
        }

        public bool DeleteGameEvent(int id)
        {
            try
            {
                var gameEvent = context.gameEvents.FirstOrDefault(g => g.id == id);
                context.gameEvents.Remove(gameEvent);
                context.SaveChanges();
                return true;
            }
            catch { return false; }
        }

        public GameEvent GetGameEvent(int id)
        {
            return context.gameEvents.FirstOrDefault(g => g.id == id);
        }

        public List<GameEvent> GetGameEvents(int gameId)
        {
            return context.gameEvents.Where(g => g.gameId == gameId).ToList();
        }

        public List<GameAthlete> CreateGameAthlete(List<GameAthlete> gameAthlete)
        {
            gameAthlete.ToList().ForEach(g => g.created = Utils.GetDateTime());
            gameAthlete.ToList().ForEach(g => g.updated = Utils.GetDateTime());
            context.gameAthletes.AddRange(gameAthlete);
            context.SaveChanges();
            return gameAthlete;
        }

        public bool UpdateGameAthlete(GameAthlete gameAthlete)
        {
            try
            {
                gameAthlete.updated = Utils.GetDateTime();
                context.gameAthletes.Update(gameAthlete);
                context.SaveChanges();
                return true;
            }
            catch { return false; }
        }

        public bool DeleteGameAthlete(int id)
        {
            try
            {
                var gameAthlete = context.gameAthletes.FirstOrDefault(g => g.id == id);
                context.gameAthletes.Remove(gameAthlete);
                context.SaveChanges();
                return true;
            }
            catch { return false; }
        }

        public List<GameAthlete> GetGamesAthletes(int gameId)
        {
            return context.gameAthletes.Where(g => g.gameId == gameId).ToList();
        }

        public GameData GetGameData(int gameId)
        {
            var gameData = new GameData();
            gameData.game = GetGame(gameId);
            gameData.clubs = context.clubs.Where(t => t.id == gameData.game.homeId || t.id == gameData.game.visitorId).ToList();
            gameData.athletes = context.athletes.Where(t => t.clubId == gameData.game.homeId || t.clubId == gameData.game.visitorId).ToList();

            if (gameData.game.competitionId != null)
            {
                gameData.competition = context.competitions.FirstOrDefault(t => t.id == gameData.game.competitionId);
                gameData.sport = context.sports.FirstOrDefault(t => t.id == gameData.competition.sportId);
            }
            else
                gameData.sport = context.sports.FirstOrDefault(t => t.name == "Soccer");

            gameData.events = context.events.Where(t => t.sportId == gameData.sport.id).ToList();
            gameData.gameEvents = context.gameEvents.Where(t => t.gameId == gameId).ToList();

            return gameData;
        }

        public List<GameUser> GetGameUsers(string query)
        {
            List<GameUser> gamesUsers;

            if (string.IsNullOrEmpty(query))
                gamesUsers = context.gameUsers.ToList();
            else
            {
                var criteria = JsonConvert.DeserializeObject<FilterCriteria>(query);
                var q = string.Format("SELECT * FROM gameUsers WHERE " + criteria.fieldName + " " + criteria.condition + " '" + criteria.value + "';");
                gamesUsers = context.gameUsers.FromSqlRaw(q).ToList();
            }

            return gamesUsers;
        }

        public List<GameUser> CreateGameUser(List<GameUser> gameUsers)
        {
            foreach (var gameUser in gameUsers)
            {
                var obj = context.gameUsers.Where(c => c.gameId == gameUser.gameId && c.userId == gameUser.userId && c.athleteId == gameUser.athleteId).ToList();
                if (obj.Count != 0)
                    gameUsers.Remove(gameUser);
            }

            gameUsers.ToList().ForEach(g => g.created = Utils.GetDateTime());
            gameUsers.ToList().ForEach(g => g.updated = Utils.GetDateTime());
            context.gameUsers.AddRange(gameUsers);
            context.SaveChanges();
            return gameUsers;
        }

        public bool UpdateGameUser(GameUser gameUser)
        {
            try
            {
                gameUser.updated = Utils.GetDateTime();
                context.gameUsers.Update(gameUser);
                context.SaveChanges();
                return true;
            }
            catch { return false; }
        }

        public List<GameEvent> AthleteGameEvents(int athleteId, int gameId)
        {
            return context.gameEvents.Where(g => g.athleteId == athleteId && g.gameId == gameId).ToList();
        }

        public bool DeleteGameUser(GameUser gameUser)
        {
            try
            {
                context.gameUsers.Remove(gameUser);
                context.SaveChanges();
                return true;
            }
            catch { return false; }
        }

        public List<ClubStats> GetGameStatistics(int gameId)
        {
            var stats = new List<ClubStats>();

            var gameEvents = context.gameEvents.Where(x => x.gameId == gameId).ToList();
            var uniqueClubs = gameEvents.Select(x => x.clubId).Distinct().ToList();

            foreach (var i in uniqueClubs)
            {
                if (i != null)
                {
                    var clubEvents = gameEvents.Where(x => x.clubId == i).ToList();
                    foreach (Event e in context.events)
                    {
                        var evt = clubEvents.Where(x => x.eventId == e.id).ToList();
                        var stat = new ClubStats
                        {
                            Count = evt.Count(),
                            ClubId = int.Parse(i.ToString()),
                            EventId = e.id
                        };
                        stats.Add(stat);
                    }
                }
            }

            return stats;
        }
    }
}