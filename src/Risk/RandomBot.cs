using System.Drawing;

namespace Risk;

public class RandomBot : IPlayer {
    public string Name { get; set; } = "Random";
    public IEnumerable<Card> Cards { get; set; } = [];
    public int NumArmies { get; set; }
    public int NumTerritoriesOwned { get; set; }
    public Color Color { get; set; }
    private readonly Random random = new Random();

    public void TakeTurn(Game game) {
        DeployAll(game);
        AttackAll(game);
    }

    private void DeployAll(Game game) {
        IList<Territory> terrs = game.TerritoriesConquered(this, game.Territories);
        while (NumArmies > 0) {
            game.Deploy(this, terrs[random.Next(terrs.Count)]);
        }
    }

    private void AttackAll(Game game) {
        const int MAX_ATTACKS = 10;
        IList<Territory> terrs;
        Territory attackTerr;
        IList<Territory> neighbors;
        Territory defendTerr;
        int i = 0;
        while (game.Actions.Contains(Action.ATTACK) && i < random.Next(MAX_ATTACKS + 1)) {
            terrs = game.TerritoriesConquered(this, game.Territories);
            attackTerr = terrs[random.Next(terrs.Count)];
            neighbors = attackTerr.Neighbors;
            defendTerr = neighbors[random.Next(neighbors.Count)];
            game.Attack(attackTerr, defendTerr, this, defendTerr.Player);
            i++;
        }
    }
}
