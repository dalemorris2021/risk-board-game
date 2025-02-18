using System.Drawing;

namespace Risk;

public interface IPlayer {
    public string Name { get; set; }
    public Color Color { get; set; }
    public void TakeTurn(Game game);
}
