using Microsoft.EntityFrameworkCore;

namespace escout.Models.Database
{
    public class DataContext : DbContext
    {
        public DbSet<User> users { get; set; }
        public DbSet<Athlete> athletes { get; set; }
        public DbSet<Club> clubs { get; set; }
        public DbSet<ClubAthlete> clubAthletes { get; set; }
        public DbSet<Competition> competitions { get; set; }
        public DbSet<CompetitionBoard> competitionBoards { get; set; }
        public DbSet<Event> events { get; set; }
        public DbSet<Game> games { get; set; }
        public DbSet<GameUser> gameUsers { get; set; }
        public DbSet<GameEvent> gameEvents { get; set; }
        public DbSet<GameAthlete> gameAthletes { get; set; }
        public DbSet<Sport> sports { get; set; }
        public DbSet<Image> images { get; set; }
        public DbSet<Favorite> favorites { get; set; }

        public DataContext() { }

        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
    }

    public class User
    {
        public int id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public int accessLevel { get; set; }
        public int notifications { get; set; }
        public int status { get; set; }
        public int? imageId { get; set; }
        public string created { get; set; }
        public string updated { get; set; }
    }

    public class Athlete
    {
        public int id { get; set; }
        public string key { get; set; }
        public string name { get; set; }
        public string fullname { get; set; }
        public string birthDate { get; set; }
        public string birthPlace { get; set; }
        public string citizenship { get; set; }
        public double height { get; set; }
        public double weight { get; set; }
        public string position { get; set; }
        public int positionKey { get; set; }
        public string agent { get; set; }
        public string currentInternational { get; set; }
        public string status { get; set; }
        public int? clubId { get; set; }
        public int? imageId { get; set; }
        public string created { get; set; }
        public string updated { get; set; }
    }

    public class Club
    {
        public int id { get; set; }
        public string key { get; set; }
        public string name { get; set; }
        public string fullname { get; set; }
        public string country { get; set; }
        public string founded { get; set; }
        public string colors { get; set; }
        public string members { get; set; }
        public string stadium { get; set; }
        public string address { get; set; }
        public string homepage { get; set; }
        public int? imageId { get; set; }
        public string created { get; set; }
        public string updated { get; set; }
    }

    public class ClubAthlete
    {
        public int id { get; set; }
        public int clubId { get; set; }
        public int athleteId { get; set; }
        public int status { get; set; }
        public int number { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }
        public string position { get; set; }
        public int positionKey { get; set; }
        public string data { get; set; }
        public string created { get; set; }
        public string updated { get; set; }
    }

    public class Competition
    {
        public int id { get; set; }
        public string key { get; set; }
        public string name { get; set; }
        public string edition { get; set; }
        public int sportId { get; set; }
        public int? imageId { get; set; }
        public string created { get; set; }
        public string updated { get; set; }
    }

    public class CompetitionBoard
    {
        public int id { get; set; }
        public int position { get; set; }
        public int played { get; set; }
        public int won { get; set; }
        public int drawn { get; set; }
        public int lost { get; set; }
        public int goalsFor { get; set; }
        public int goalsAgainst { get; set; }
        public int goalsDifference { get; set; }
        public int points { get; set; }
        public int clubId { get; set; }
        public int competitionId { get; set; }
        public string created { get; set; }
        public string updated { get; set; }
    }

    public class Event
    {
        public int id { get; set; }
        public string key { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int sportId { get; set; }
        public int? imageId { get; set; }
        public string created { get; set; }
        public string updated { get; set; }
    }

    public class GameEvent
    {
        public int id { get; set; }
        public string key { get; set; }
        public string time { get; set; }
        public string gameTime { get; set; }
        public string eventDescription { get; set; }
        public int gameId { get; set; }
        public int eventId { get; set; }
        public int? athleteId { get; set; }
        public int? clubId { get; set; }
        public int userId { get; set; }
        public string created { get; set; }
        public string updated { get; set; }
    }

    public class GameAthlete
    {
        public int id { get; set; }
        public int status { get; set; }
        public int gameId { get; set; }
        public int athleteId { get; set; }
        public string created { get; set; }
        public string updated { get; set; }
    }

    public class Game
    {
        public int id { get; set; }
        public string timeStart { get; set; }
        public string timeEnd { get; set; }
        public int homeScore { get; set; }
        public int visitorScore { get; set; }
        public int homePenaltyScore { get; set; }
        public int visitorPenaltyScore { get; set; }
        public int status { get; set; }
        public string type { get; set; }
        public string location { get; set; }
        public int homeId { get; set; }
        public int visitorId { get; set; }
        public int? competitionId { get; set; }
        public int? imageId { get; set; }
        public int userId { get; set; }
        public string created { get; set; }
        public string updated { get; set; }
    }

    public class GameUser
    {
        public int id { get; set; }
        public int userId { get; set; }
        public int gameId { get; set; }
        public int athleteId { get; set; }
        public string created { get; set; }
        public string updated { get; set; }
    }

    public class Sport
    {
        public int id { get; set; }
        public string name { get; set; }
        public int? imageId { get; set; }
        public string created { get; set; }
        public string updated { get; set; }
    }

    public class Image
    {
        public int id { get; set; }
        public string imageUrl { get; set; }
        public string tags { get; set; }
        public string description { get; set; }
        public string created { get; set; }
        public string updated { get; set; }
    }

    public class Favorite
    {
        public int id { get; set; }
        public int userId { get; set; }
        public int? athleteId { get; set; }
        public int? clubId { get; set; }
        public int? competitionId { get; set; }
        public int? gameId { get; set; }
        public string created { get; set; }
        public string updated { get; set; }
    }
}
