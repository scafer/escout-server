using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace escout.Models
{
    public class Statistics
    {
        public List<GameStats> gameStats { get; set; }
        public List<TotalStats> totalStats { get; set; }
    }

    public class GameStats
    {
        public int gameId { get; set; }
        public Event evt { get; set; }
        public int count { get; set; }
    }

    public class TotalStats
    {
        public int count { get; set; }
        public double average { get; set; }
        public double median { get; set; }
        public double standardDeviation { get; set; }
        public Event evt { get; set; }
    }
}
