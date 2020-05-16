using escout.Models;
using escoutTests.Resources;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace escout.Controllers.Tests
{
    [TestClass]
    public class AthleteControllerTests
    {
        private DataContext db = new DataContext();
        AthleteController controller = new AthleteController();

        [TestInitialize]
        public void Setup()
        {
            Environment.SetEnvironmentVariable("DATABASE_URL", "postgres://postgres:password@localhost:5432/postgres");
            db.Database.ExecuteSqlRaw(Queries.CreateDatabase);
        }

        [Ignore]
        [TestMethod]
        public void CreateAthleteTest()
        {
            List<Athlete> athletes = new List<Athlete>();
            var athlete = new Athlete();

            athletes.Add(athlete);
            var result = controller.CreateAthlete(athletes);

            Assert.IsNotNull(db.athletes.FirstOrDefault(t => t.Equals(athletes[0])));
            Assert.IsNotNull(result.Value);
        }

        [Ignore]
        [TestMethod]
        public void UpdateAthleteTest()
        {
            var athlete = new Athlete();
            db.athletes.Add(athlete);
            db.SaveChanges();

            athlete.key = string.Empty;
            var result = controller.UpdateAthlete(athlete);

            Assert.IsNotNull(db.athletes.FirstOrDefault(t => t.id == athlete.id)?.key);
            Assert.AreEqual(result.Value.errorCode, 0);
        }

        [Ignore]
        [TestMethod]
        public void RemoveAthleteTest()
        {
            var athlete = new Athlete();
            db.athletes.Add(athlete);
            db.SaveChanges();

            Assert.IsNotNull(db.athletes.FirstOrDefault(t => t.id == athlete.id));

            var result = controller.RemoveAthlete(athlete.id);

            Assert.IsNull(db.athletes.FirstOrDefault(t => t.id == athlete.id));
            Assert.AreEqual(result.Value.errorCode, 0);
        }

        [Ignore]
        [TestMethod]
        public void GetAthleteTest()
        {

        }

        [Ignore]
        [TestMethod]
        public void GetAthletesTest()
        {

        }
    }
}