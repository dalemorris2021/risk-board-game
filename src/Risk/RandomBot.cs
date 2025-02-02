using System.Drawing;

namespace Risk;

public class RandomBot {
    public string Name { get; set; } = "Random";
    public IEnumerable<Card> Cards { get; set; } = [];
    public int NumArmies { get; set; }
    public int NumTerritoriesOwned { get; set; }
    public Color Color { get; set; }

    public void TakeTurn(Game game) {
        game.TakeAction(game.Actions[0]);
    }
}
