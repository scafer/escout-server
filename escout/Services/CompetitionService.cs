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
        private readonly DataContext context;
        public CompetitionService(DataContext context) => this.context = context;

        public List<Competition> CreateCompetition(List<Competition> competition)
        {
            competition.ToList().ForEach(c => c.created = Utils.GetDateTime());
            competition.ToList().ForEach(c => c.updated = Utils.GetDateTime());
            context.competitions.AddRange(competition);
            context.SaveChanges();
            return competition;
        }

        public bool UpdateCompetition(Competition competition)
        {
            try
            {
                competition.updated = Utils.GetDateTime();
                context.competitions.Update(competition);
                context.SaveChanges();
                return true;
            }
            catch { return false; }
        }

        public bool RemoveCompetition(int id)
        {
            try
            {
                var competition = context.competitions.FirstOrDefault(c => c.id == id);
                context.competitions.Remove(competition);
                context.SaveChanges();
                return true;
            }
            catch { return false; }
        }

        public Competition GetCompetition(int id)
        {
            return context.competitions.FirstOrDefault(c => c.id == id);
        }

        public List<Competition> GetCompetitions(string query)
        {
            List<Competition> competitions;

            if (string.IsNullOrEmpty(query))
                competitions = context.competitions.ToList();
            else
            {
                var criteria = JsonConvert.DeserializeObject<FilterCriteria>(query);
                var q = string.Format("SELECT * FROM competitions WHERE " + criteria.fieldName + " " + criteria.condition + " '" + criteria.value + "';");
                competitions = context.competitions.FromSqlRaw(q).ToList();
            }

            return competitions;
        }

        public List<CompetitionBoard> CreateCompetitionBoard(List<CompetitionBoard> competitionBoard)
        {
            competitionBoard.ToList().ForEach(c => c.created = Utils.GetDateTime());
            competitionBoard.ToList().ForEach(c => c.updated = Utils.GetDateTime());
            context.competitionBoards.AddRange(competitionBoard);
            context.SaveChanges();
            return competitionBoard;
        }

        public bool UpdateCompetitionBoard(CompetitionBoard competitionBoard)
        {
            try
            {
                competitionBoard.updated = Utils.GetDateTime();
                context.competitionBoards.Update(competitionBoard);
                context.SaveChanges();
                return true;
            }
            catch { return false; }
        }

        public bool RemoveCompetitionBoard(int id)
        {
            try
            {
                var competitionBoard = context.competitionBoards.FirstOrDefault(c => c.id == id);
                context.competitionBoards.Remove(competitionBoard);
                context.SaveChanges();
                return true;
            }
            catch { return false; }
        }

        public List<CompetitionBoard> GetCompetitionBoard(int competitionId)
        {
            return context.competitionBoards.Where(c => c.competitionId == competitionId).ToList();
        }
    }
}
