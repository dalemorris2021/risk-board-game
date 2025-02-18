using System.Drawing;

namespace Risk;

public class RandomBot(string name, Color color) : IPlayer {
    public string Name { get; set; } = name;
    public Color Color { get; set; } = color;
    private readonly Random random = new Random();

    public void TakeTurn(Game game) {
        DeployAll(game);
        AttackAll(game);
    }

    private void DeployAll(Game game) {
        IList<Territory> terrs = game.TerritoriesConquered(this, game.Territories);
        Territory terr;
        while (game.PlayerArmies[this] > 0 && terrs.Count > 0) {
            int randInt = random.Next(terrs.Count);
            terr = terrs[randInt];
            if (terr.NumArmies == Territory.MAX_ARMIES) {
                terrs.Remove(terr);
            } else {
                game.Deploy(this, terr);
            }
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
