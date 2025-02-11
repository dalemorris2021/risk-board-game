using System.Drawing;

namespace Risk;

public class NeutralBot(Color color) : IPlayer {
    public string Name { get; set; } = "Neutral";
    public IEnumerable<Card> Cards { get; set; } = [];
    public int NumArmies { get; set; }
    public int NumTerritoriesOwned { get; set; }
    public Color Color { get; set; } = color;

    public void AddArmies(int numArmies) {
        return;
    }

    public void SubArmies(int numArmies) {
        NumArmies = Math.Max(0, NumArmies - numArmies);
    }

    public void TakeTurn(Game game) {
        return;
    }
}
