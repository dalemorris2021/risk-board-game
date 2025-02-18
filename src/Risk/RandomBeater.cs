using System.Drawing;

namespace Risk;

public class RandomBeater(string name) : IPlayer {
    public string Name { get; set; } = name;
    public Color Color { get; set; }

    public void TakeTurn(Game game) {
        
    }
}
