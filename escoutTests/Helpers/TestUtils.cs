using System.Security.Claims;
using escout.Models.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace escoutTests.Resources
{
    public class TestUtils
    {
        public static DataContext GetMockContext()
        {
            var dbContextOptions = new DbContextOptionsBuilder<DataContext>().UseInMemoryDatabase("escout_db");
            var context = new DataContext(dbContextOptions.Options);
            context.Database.EnsureCreated();
            return context;
        }

        public static DefaultHttpContext SetUserContext(DataContext context, int level)
        {
            var user = new User() { username = "test", email = "test@email.com", password = "test", accessLevel = level };
            context.users.Add(user);
            context.SaveChanges();

            var identity = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] { new Claim(ClaimTypes.Name, user.id.ToString()) }));
            return new DefaultHttpContext { User = identity };
        }

        public static Event AddEventToContext(DataContext context)
        {
            var evt = new Event() { name = "test" };
            context.events.Add(evt);
            context.SaveChanges();

            return evt;
        }

        public static Athlete AddAthleteToContext(DataContext context)
        {
            var athlete = new Athlete() { name = "test" };
            context.athletes.Add(athlete);
            context.SaveChanges();

            return athlete;
        }

        public static Club AddClubToContext(DataContext context)
        {
            var club = new Club() { name = "test" };
            context.clubs.Add(club);
            context.SaveChanges();

            return club;
        }

        public static Competition AddCompetitionToContext(DataContext context)
        {
            var competition = new Competition() { name = "test" };
            context.competitions.Add(competition);
            context.SaveChanges();

            return competition;
        }

        public static CompetitionBoard AddCompetitionBoardToContext(DataContext context)
        {
            var club = AddClubToContext(context);
            var competition = AddCompetitionToContext(context);
            var competitionBoard = new CompetitionBoard() { competitionId = competition.id, clubId = club.id };
            context.competitionBoards.Add(competitionBoard);
            context.SaveChanges();

            return competitionBoard;
        }

        public static Game AddGameToContext(DataContext context)
        {
            var club1 = AddClubToContext(context);
            var club2 = AddClubToContext(context);
            var game = new Game() { homeId = club1.id, visitorId = club2.id, type = "test" };
            context.games.Add(game);
            context.SaveChanges();

            return game;
        }

        public static Image AddImageToContext(DataContext context)
        {
            var image = new Image() { imageUrl = "test" };
            context.images.Add(image);
            context.SaveChanges();

            return image;
        }

        public static Sport AddSportToContext(DataContext context)
        {
            var sport = new Sport() { name = "test" };
            context.sports.Add(sport);
            context.SaveChanges();

            return sport;
        }

        public static User AddUserToContext(DataContext context)
        {
            var user = new User() { username = "test", email = "test@email.com", password = "test" };
            context.users.Add(user);
            context.SaveChanges();

            return user;
        }
    }
}
