using System.Drawing;
using System.Security.Cryptography;

namespace Risk;

public class RandomBeater : IPlayer {
    public string Name { get; set; } = "Random";
    public IEnumerable<Card> Cards { get; set; } = [];
    public int NumArmies { get; set; }
    public int NumTerritoriesOwned { get; set; }
    public Color Color { get; set; }

    public void AddArmies(int numArmies) {
        NumArmies = Math.Min(IPlayer.MAX_ARMIES, NumArmies + numArmies);
    }

    public void SubArmies(int numArmies) {
        NumArmies = Math.Max(0, NumArmies - numArmies);
    }

    public void TakeTurn(Game game) {
        
    }
}
