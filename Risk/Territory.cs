using System.Drawing;

namespace Risk;

public class Territory {
    public string Name { get; set; }
    public string Continent { get; set; }
    public int NumArmies { get; set; }
    public ICollection<Territory> Neighbors { get; set; }
    public Player? OccupyingPlayer { get; set; }
    public int[] Coordinates { get; set; }
    public Color Color { get; set; }
    public int TerrNum { get; set; }

    public Territory(string name, string continent) {
        Name = name;
        Continent = continent;
        NumArmies = 0;
        Neighbors = [];
        OccupyingPlayer = null;
        Coordinates = [0, 0];
        Color = Color.Black;
        TerrNum = 0;
    }

    public bool IsNeighbor(Territory terr) {
        return Neighbors.Contains(terr);
    }

    public void PrintNeighbors() {
        Console.WriteLine(Name);
        foreach(Territory neighbor in Neighbors) {
            Console.WriteLine(neighbor.Name);
        }
    }
}
