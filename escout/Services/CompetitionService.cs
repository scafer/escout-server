using System;
using escout.Models;
using System.Collections.Generic;
using System.Linq;

namespace escout.Services
{
    public class CompetitionService : BaseService
    {
        DataContext db;

        public CompetitionService()
        {
            db = new DataContext();
        }
        public Competition CreateCompetition(Competition competition)
        {
            db.competitions.Add(competition);
            db.SaveChanges();
            return competition;
        }

        public bool UpdateCompetition(Competition competition)
        {
            try
            {
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

        public List<Competition> GetCompetitions()
        {
            return db.competitions.ToList();
        }

        public CompetitionBoard CreateCompetitionBoard(CompetitionBoard competitionBoard)
        {
            db.competitionBoards.Add(competitionBoard);
            db.SaveChanges();
            return competitionBoard;
        }

        public bool UpdateCompetitionBoard(CompetitionBoard competitionBoard)
        {
            try
            {
                db.competitionBoards.Update(competitionBoard);
                db.SaveChanges();
                return true;
            }
            catch { return false; }
        }

        public bool RemoveCompetitionBoard(CompetitionBoard competitionBoard)
        {
            try
            {
                db.competitionBoards.Remove(competitionBoard);
                db.SaveChanges();
                return true;
            }
            catch { return false; }
        }

        public CompetitionBoard GetCompetitionBoard(int competitionId)
        {
            return db.competitionBoards.FirstOrDefault(c => c.competitionId == competitionId);
        }
    }
}
