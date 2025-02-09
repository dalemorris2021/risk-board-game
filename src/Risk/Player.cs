using System.Drawing;
using System.Globalization;

namespace Risk;

public class Player : IPlayer {
    public string Name { get; set; }
    public IEnumerable<Card> Cards { get; set; } = []; // Player shouldn't be able to see other players' cards
    public int NumArmies { get; set; } = 0;
    public int NumTerritoriesOwned { get; set; } = 0;
    public Color Color { get; set; }
    private TextInfo TextInfo = new CultureInfo("en-US").TextInfo;
    private const string END_OF_INPUT_MESSAGE = "Reached end of input";

    public Player(string name, Color color) {
        Name = name;
        Cards = [];
        NumArmies = 0;
        NumTerritoriesOwned = 0;
        Color = color;
    }

    public Player(Color color) {
        Console.WriteLine("Enter a player name: ");
        Name = Console.ReadLine() ?? throw new EndOfStreamException();
        Cards = [];
        NumArmies = 0;
        NumTerritoriesOwned = 0;
        Color = color;
    }

    public void TakeTurn(Game game) {
        int i = 0;
        int currentPlayer = 0;
        while (i < game.Players.Count) {
            if (game.Players[i].NumArmies > 0) {
                Console.WriteLine("Deploy armies!");
                DeployArmies(game);
            }

            Console.WriteLine($"{game.Players[currentPlayer].Name}, will you attack? (Y/N)");
            string? input = Console.ReadLine();
            if (input == null) {
                Console.WriteLine(END_OF_INPUT_MESSAGE);
                throw new EndOfStreamException(END_OF_INPUT_MESSAGE);
            }
            string answer = game.TextInfo.ToTitleCase(input);

            if (answer == "Y") {
                Attack(game);
            } else if (answer == "N") {
                Console.WriteLine($"{game.Players[currentPlayer].Name}, will you fortify? (Y/N)");
                input = Console.ReadLine();
                if (input == null) {
                    Console.WriteLine(END_OF_INPUT_MESSAGE);
                    throw new EndOfStreamException(END_OF_INPUT_MESSAGE);
                }
                answer = game.TextInfo.ToTitleCase(input);
                if (answer == "Y") {
                    Console.WriteLine("Fortify!");
                    Fortify(game);
                    currentPlayer = (currentPlayer + 1) % game.Players.Count;
                    i += 1;
                } else if (answer == "N") {
                    currentPlayer = (currentPlayer + 1) % game.Players.Count;
                    i += 1;
                } else { // Getting here will restart the whole loop, but it should only go back to last input
                    Console.WriteLine("Invalid!");
                }
            } else {
                Console.WriteLine("Invalid!");
            }
        }
    }

    private void DeployArmies(Game game) {
        while (NumArmies != 0) {
            Console.WriteLine($"{Name}, select a territory to place an army");
            string input = InputHandler.GetInput();
            string terrName = TextInfo.ToTitleCase(input);

            if (!game.Territories.ContainsKey(terrName)) {
                Console.WriteLine("Invalid! Try again.");
            } else if (this != game.Territories[terrName].Player) {
                Console.WriteLine("Not your territory!");
            } else {
                Console.WriteLine($"You have {NumArmies} to deploy.");
                input = InputHandler.GetInput();
                int numArmies;
                if (Int32.TryParse(input, out numArmies)) {
                    game.PlaceArmy(this, game.Territories[terrName], numArmies);
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
            game.StartAttack(game.Territories[attackTerrName], game.Territories[defendTerrName], this,
                    game.Territories[defendTerrName].Player); // Should verify that defendTerr is occupied
        }
    }

    private void Fortify(Game game) {
        while (true) {
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

        game.DeferredPlaceArmyFortify(numArmies, from, to);
    }
}
