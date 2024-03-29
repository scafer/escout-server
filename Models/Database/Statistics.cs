﻿using System.Collections.Generic;

namespace escout.Models.Database;

public class Statistics
{
    public Statistics()
    {
        GameStats = new List<GameStats>();
        TotalStats = new List<TotalStats>();
    }

    public List<GameStats> GameStats { get; set; }
    public List<TotalStats> TotalStats { get; set; }
}

public class GameStats
{
    public int GameId { get; set; }
    public int Count { get; set; }
    public int EventId { get; set; }
}

public class TotalStats
{
    public int Count { get; set; }
    public double Average { get; set; }
    public double Median { get; set; }
    public double StandardDeviation { get; set; }
    public int EventId { get; set; }
}

public class Counter
{
    public int Count { get; set; }
    public int EventId { get; set; }
}

public class ClubStats
{
    public int Count { get; set; }
    public int EventId { get; set; }
    public int ClubId { get; set; }
}