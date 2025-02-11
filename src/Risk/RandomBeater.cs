using System.Drawing;
using System.Security.Cryptography;

namespace Risk;

public class RandomBeater : IPlayer {
    public string Name { get; set; } = "Random";
    public IEnumerable<Card> Cards { get; set; } = [];
    public int NumTerritoriesOwned { get; set; }
    public Color Color { get; set; }

    public void TakeTurn(Game game) {
        
    }
}
