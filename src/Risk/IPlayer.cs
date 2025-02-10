using System.Drawing;

namespace Risk;

public interface IPlayer {
    public string Name { get; set; }
    public IEnumerable<Card> Cards { get; set; }
    public int NumArmies { get; }
    public void AddArmies(int numArmies);
    public void SubArmies(int numArmies);
    public const int MAX_ARMIES = 999;
    public int NumTerritoriesOwned { get; set; }
    public Color Color { get; set; }
    public void TakeTurn(Game game);
}
