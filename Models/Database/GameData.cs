#nullable enable
using System.Collections.Generic;

namespace escout.Models.Database;

public class GameData
{
    public Game? game { get; set; }
    public Sport? sport { get; set; }
    public List<Club>? clubs { get; set; }
    public List<Athlete>? athletes { get; set; }
    public Competition? competition { get; set; }
    public List<Event>? events { get; set; }
    public List<GameEvent>? gameEvents { get; set; }
}