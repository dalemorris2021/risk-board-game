using System.Drawing;

namespace Risk;

public class Player : IPlayer {
    public string Name { get; set; }
    public IEnumerable<Card> Cards { get; set; } // Player shouldn't be able to see other players' cards
    public int NumArmies { get; set; }
    public int NumTerritoriesOwned { get; set; }
    public Color Color { get; set; }
    private const string END_OF_INPUT_MESSAGE = "Reached end of input";

    public Player(string name, Color color) {
        Name = name;
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
                game.DeployArmies(game.Players[i], game.Territories);
            }

            Console.WriteLine($"{game.Players[currentPlayer].Name}, will you attack? (Y/N)");
            string? input = Console.ReadLine();
            if (input == null) {
                Console.WriteLine(END_OF_INPUT_MESSAGE);
                throw new EndOfStreamException(END_OF_INPUT_MESSAGE);
            }
            string answer = game.TextInfo.ToTitleCase(input);

            if (answer == "Y") {
                game.Attack(game.Players[currentPlayer], game.Territories);
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
                    game.Fortify(game.Players[currentPlayer], game.Territories);
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

    public IEnumerable<Territory> TerritoriesConquered(IDictionary<string, Territory> terrsDict) {
        IList<Territory> territories = [];
        foreach (Territory terr in terrsDict.Values) {
            if (terr.Player == this) {
                territories.Add(terr);
            }
        }

        return territories;
    }

    public void PlaceArmy(Territory terr, int numArmies = 1) {
        if (terr.Player == null) {
            terr.Player = this;
        }

        if (NumArmies >= numArmies) {
            terr.NumArmies += numArmies;
            NumArmies -= numArmies;
        }
    }

    public void PlaceArmyFortify(Territory from, Territory to) {
        const string END_OF_INPUT_MESSAGE = "Reached end of input";
        const string ENTER_NUM_ARMIES_MESSAGE = "How many armies would you like to place?";
        
        if (from.NumArmies == 1) {
            Console.WriteLine($"You only have one army at {from.Name}");
            return;
        }
        
        int numArmies;
        while (true) {
            Console.WriteLine($"There are {from.NumArmies - 1} armies available to move.");
            Console.WriteLine(ENTER_NUM_ARMIES_MESSAGE);
            string? input = Console.ReadLine();
            if (input == null) {
                Console.WriteLine(END_OF_INPUT_MESSAGE);
                throw new EndOfStreamException(END_OF_INPUT_MESSAGE);
            } else if (Int32.TryParse(input, out numArmies)) {
                break;
            } else {
                Console.WriteLine(ENTER_NUM_ARMIES_MESSAGE);
            }
        }

        to.NumArmies += numArmies;
        from.NumArmies -= numArmies;
    }
}
