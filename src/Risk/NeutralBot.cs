using System.Drawing;

namespace Risk;

public class NeutralBot(Color color) : IPlayer {
    public string Name { get; set; } = "Neutral";
    public Color Color { get; set; } = color;

    public void TakeTurn(Game game) {
        return;
    }
}
