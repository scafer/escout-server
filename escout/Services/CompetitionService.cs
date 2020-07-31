using escout.Helpers;
using escout.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace escout.Services
{
    public class CompetitionService : BaseService
    {
        readonly DataContext db;

        public CompetitionService() => db = new DataContext();

        public List<Competition> CreateCompetition(List<Competition> competition)
        {
            competition.ToList().ForEach(c => c.created = Utils.GetDateTime());
            competition.ToList().ForEach(c => c.updated = Utils.GetDateTime());
            db.competitions.AddRange(competition);
            db.SaveChanges();
            return competition;
        }

        public bool UpdateCompetition(Competition competition)
        {
            try
            {
                competition.updated = Utils.GetDateTime();
                db.competitions.Update(competition);
                db.SaveChanges();
                return true;
            }
            catch { return false; }
        }

        public bool RemoveCompetition(int id)
        {
            try
            {
                var competition = db.competitions.FirstOrDefault(c => c.id == id);
                db.competitions.Remove(competition);
                db.SaveChanges();
                return true;
            }
            catch { return false; }
        }

        public Competition GetCompetition(int id)
        {
            return db.competitions.FirstOrDefault(c => c.id == id);
        }

        public List<Competition> GetCompetitions(string query)
        {
            List<Competition> competitions;

            if (string.IsNullOrEmpty(query))
                competitions = db.competitions.ToList();
            else
            {
                var criteria = JsonConvert.DeserializeObject<FilterCriteria>(query);
                var q = string.Format("SELECT * FROM competitions WHERE " + criteria.fieldName + " " + criteria.condition + " '" + criteria.value + "';");
                competitions = db.competitions.FromSqlRaw(q).ToList();
            }

            return competitions;
        }

        public List<CompetitionBoard> CreateCompetitionBoard(List<CompetitionBoard> competitionBoard)
        {
            competitionBoard.ToList().ForEach(c => c.created = Utils.GetDateTime());
            competitionBoard.ToList().ForEach(c => c.updated = Utils.GetDateTime());
            db.competitionBoards.AddRange(competitionBoard);
            db.SaveChanges();
            return competitionBoard;
        }

        public bool UpdateCompetitionBoard(CompetitionBoard competitionBoard)
        {
            try
            {
                competitionBoard.updated = Utils.GetDateTime();
                db.competitionBoards.Update(competitionBoard);
                db.SaveChanges();
                return true;
            }
            catch { return false; }
        }

        public bool RemoveCompetitionBoard(int id)
        {
            try
            {
                var competitionBoard = db.competitionBoards.FirstOrDefault(c => c.id == id);
                db.competitionBoards.Remove(competitionBoard);
                db.SaveChanges();
                return true;
            }
            catch { return false; }
        }

        public List<CompetitionBoard> GetCompetitionBoard(int competitionId)
        {
            return db.competitionBoards.Where(c => c.competitionId == competitionId).ToList();
        }
    }
}
