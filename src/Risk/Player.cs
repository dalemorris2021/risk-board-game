using System.Drawing;
using System.Globalization;

namespace Risk;

public class Player : IPlayer {
    public string Name { get; set; }
    public Color Color { get; set; }
    private readonly TextInfo TextInfo = new CultureInfo("en-US").TextInfo;

    public Player(string name, Color color) {
        Name = name;
        Color = color;
    }

    public Player(Color color) {
        Console.WriteLine("Enter a player name: ");
        Name = Console.ReadLine() ?? throw new EndOfStreamException();
        Color = color;
    }

    public void TakeTurn(Game game) {
        string input;
        string answer;

        Console.WriteLine($"{Name}'s turn!");
        if (game.PlayerArmies[this] > 0) { // Where does NumArmies get set?
            Console.WriteLine("Deploy armies!");
            DeployArmies(game);
        }

        bool isAttacking = true;
        do {
            Console.WriteLine("Will you attack? (Y/N)"); // Should notify player when they can no longer attack
            input = TextInfo.ToTitleCase(InputHandler.GetInput());
            answer = TextInfo.ToTitleCase(input);

            switch (answer) {
            case "Y":
                Attack(game);
                break;
            case "N":
                isAttacking = false;
                break;
            default:
                Console.WriteLine("Invalid!");
                break;
            }
        } while (isAttacking);

        bool isFortifying = true;
        do {
            Console.WriteLine("Will you fortify? (Y/N)");
            input = InputHandler.GetInput();
            answer = TextInfo.ToTitleCase(input);

            switch (answer) {
            case "Y":
                Console.WriteLine("Fortify!");
                Fortify(game);
                break;
            case "N":
                isFortifying = false;
                break;
            default:
                Console.WriteLine("Invalid!");
                break;
            }
        } while (isFortifying);
    }

    private void DeployArmies(Game game) {
        while (game.PlayerArmies[this] != 0) {
            Console.WriteLine($"{Name}, select one of your territories to place an army.");
            foreach (Territory terr in game.TerritoriesConquered(this, game.Territories)) {
                Console.WriteLine($"* {terr.Name}");
            }

            string input = InputHandler.GetInput();
            string terrName = TextInfo.ToTitleCase(input);

            if (!game.Territories.ContainsKey(terrName)) {
                Console.WriteLine("Invalid! Try again.");
            } else if (this != game.Territories[terrName].Player) {
                Console.WriteLine("Not your territory!");
            } else {
                Console.WriteLine($"How many armies would you like to deploy? ({game.PlayerArmies[this]} available)");
                input = InputHandler.GetInput();
                int numArmies;
                if (Int32.TryParse(input, out numArmies)) {
                    game.Deploy(this, game.Territories[terrName], numArmies);
                    Console.WriteLine($"{numArmies} armies have been moved to {game.Territories[terrName].Name}!");
                } else {
                    Console.WriteLine("Input was not a number! Try again.");
                }
            }
        }
    }

    public void Attack(Game game) {
        Console.WriteLine($"{Name}, what territory will you attack?");
        string input = InputHandler.GetInput();
        string defendTerrName = TextInfo.ToTitleCase(input);

        if (!game.Territories.ContainsKey(defendTerrName)) {
            Console.WriteLine("Invalid territory!");
        } else if (this == game.Territories[defendTerrName].Player) {
            Console.WriteLine("You own this territory!");
            return;
        }

        Console.WriteLine($"{Name}, what territory will you attack from?");
        input = InputHandler.GetInput();
        string attackTerrName = TextInfo.ToTitleCase(input);

        if (!game.Territories.ContainsKey(attackTerrName)) {
            Console.WriteLine("Invalid territory!");
            return;
        }

        if (game.Territories[attackTerrName].NumArmies <= 1) {
            Console.WriteLine("Not enough armies to attack!");
            return;
        }

        bool isNeighbor = game.Territories[defendTerrName].IsNeighbor(game.Territories[attackTerrName]);
        game.Territories[attackTerrName].PrintNeighbors();

        if (!isNeighbor) {
            Console.WriteLine("These territories are not neighbors!");
            return;
        } else {
            game.Attack(game.Territories[attackTerrName], game.Territories[defendTerrName], this,
                    game.Territories[defendTerrName].Player); // Should verify that defendTerr is occupied
        }
    }

    private void Fortify(Game game) {
        bool isFortifying = true;
        while (isFortifying) {
            Console.WriteLine($"{Name}, select a territory to move armies from.");
            string input = InputHandler.GetInput();
            string fromTerrName = TextInfo.ToTitleCase(input);

            Console.WriteLine("Select a territory to place armies.");
            input = InputHandler.GetInput();
            string toTerrName = TextInfo.ToTitleCase(input);

            if (!game.Territories.ContainsKey(fromTerrName) || !game.Territories.ContainsKey(toTerrName)) {
                Console.WriteLine("Invalid! Try again.");
            } else if (this == game.Territories[fromTerrName].Player && this == game.Territories[toTerrName].Player) {
                PlaceArmyFortify(game, game.Territories[fromTerrName], game.Territories[toTerrName]);
                isFortifying = false;
            } else {
                Console.WriteLine("You must select territories you own!");
            }
        }
    }

    private void PlaceArmyFortify(Game game, Territory from, Territory to) {
        const string ENTER_NUM_ARMIES_MESSAGE = "How many armies would you like to place?";
        
        if (from.NumArmies == 1) {
            Console.WriteLine($"You only have one army at {from.Name}");
            return;
        }
        
        int numArmies;
        while (true) {
            Console.WriteLine($"There are {from.NumArmies - 1} armies available to move.");
            Console.WriteLine(ENTER_NUM_ARMIES_MESSAGE);
            string input = InputHandler.GetInput();
            if (!Int32.TryParse(input, out numArmies)) {
                Console.WriteLine(ENTER_NUM_ARMIES_MESSAGE);
            } else if (numArmies < from.NumArmies) {
                break;
            }
        }

        game.Fortify(numArmies, from, to);
    }
}
