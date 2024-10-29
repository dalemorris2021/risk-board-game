using System.Drawing;

namespace Risk;

public class Territory {
    string Name { get; set; }
    string Continent { get; set; }
    int NumArmies { get; set; }
    ICollection<string> NeighborNames { get; set; }
    Player? OccupyingPlayer { get; set; }
    int[] Coordinates { get; set; }
    Color Color { get; set; }
    int TerrNum { get; set; }

    public Territory(string name, string continent, ICollection<string> neighborNames) {
        Name = name;
        Continent = continent;
        NumArmies = 0;
        NeighborNames = neighborNames;
        OccupyingPlayer = null;
        Coordinates = [0, 0];
        Color = Color.Black;
        TerrNum = 0;
    }
}
