using System.Drawing;

namespace Risk;

public interface IPlayer {
    public string Name { get; set; }
    public IEnumerable<Card> Cards { get; set; }
    public int NumArmies { get; set; }
    public int NumTerritoriesOwned { get; set; }
    public Color Color { get; set; }
    void TakeTurn(Game game);
    public IEnumerable<Territory> TerritoriesConquered(IDictionary<string, Territory> terrsDict);
    public void PlaceArmy(Territory terr, int numArmies = 1);
    public void PlaceArmyFortify(Territory from, Territory to);
}
