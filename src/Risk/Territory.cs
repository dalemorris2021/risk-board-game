using System.Drawing;

namespace Risk;

public class Territory(string name, string continent)
{
    public string Name { get; set; } = name;
    public string Continent { get; set; } = continent;
    public int NumArmies { get; set; } = 0;
    public IList<Territory> Neighbors { get; set; } = [];
    public IPlayer? Player { get; set; } = null;
    public int[] Coordinates { get; set; } = [0, 0];
    public Color Color { get; set; } = Color.Black;
    public int TerrNum { get; set; } = 0;

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
