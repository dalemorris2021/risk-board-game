namespace Risk;

using System.Drawing;
using System.Text.Json;

public class Game {
    public void Run() {
        Console.WriteLine("Welcome to Risk!");

        const string END_OF_INPUT_MESSAGE = "Reached end of input";
        const string UNKNOWN_ERROR_MESSAGE = "An unknown error has occurred";
        const string ENTER_NUM_PLAYERS_MESSAGE = "Please enter a number between 3 and 6.";

        Console.WriteLine("How many players are there?");
        int numPlayers;
        while (true) {
            string? input = Console.ReadLine();
            if (input == null) {
                Console.WriteLine(END_OF_INPUT_MESSAGE);
                return;
            } else if (Int32.TryParse(input, out numPlayers)) {
                if (numPlayers == 2) {
                    Console.WriteLine("2-player mode has not yet been implemented.");
                    Console.WriteLine(ENTER_NUM_PLAYERS_MESSAGE);
                } else if (numPlayers < 2 || numPlayers > 6) {
                    Console.WriteLine(ENTER_NUM_PLAYERS_MESSAGE);
                } else {
                    break;
                }
            } else {
                Console.WriteLine(ENTER_NUM_PLAYERS_MESSAGE);
            }
        }

        IList<Player> players = [];
        IList<Color> playerColors = [Color.Red, Color.Blue, Color.Green, Color.Purple, Color.Yellow, Color.Orange];
        for (int i = 0; i < numPlayers; i++) {
            Console.WriteLine("Enter the name of player %d.", i + 1);
            string? name = Console.ReadLine();
            if (name == null) {
                Console.WriteLine(END_OF_INPUT_MESSAGE);
                return;
            }
            players.Add(new Player(name, playerColors[i]));
        }

        foreach (Player player in players) {
            switch (numPlayers) {
            case 3: player.NumArmies = 35; break;
            case 4: player.NumArmies = 30; break;
            case 5: player.NumArmies = 25; break;
            case 6: player.NumArmies = 20; break;
            default: Console.Error.WriteLine(UNKNOWN_ERROR_MESSAGE); return;
            }
        }

        Console.WriteLine("Each player will start with %d armies.", players[0].NumArmies);

        IDictionary<Territory, ICollection<Territory>>? terrs = LoadTerritories("data/territories.json");
        if (terrs == null) {
            Console.Error.WriteLine("Could not load territories");
            return;
        }

        ICollection<Continent>? conts = LoadContinents("data/continents.json");
        if (conts == null) {
            Console.Error.WriteLine("Could not load continents");
            return;
        }

        ICollection<Card>? cards = LoadCards("data/cards.json");
        if (cards == null) {
            Console.Error.WriteLine("Could not load cards");
            return;
        }
    }

    /**
     * <summary>
     * Checks if <code>t1</code> has <code>t2</code> as a neighbor in <code>terrs</code>
     * </summary>
     * <param name="t1">the primary territory</param>
     * <param name="t2">the neighboring territory</param>
     * <param name="terrs">a dictionary defining territory-neighbor pairs</param>
     * <returns>
     * <code>true</code> if the value of <code>t1</code> in <code>terrs</code>
     * contains <code>t2</code>, false otherwise
     * </returns>
     */
    private static bool HasNeighbor(Territory t1, Territory t2, IDictionary<Territory, ICollection<Territory>> terrs) {
        return terrs[t1].Contains(t2);
    }

    private static IDictionary<Territory, ICollection<Territory>>? LoadTerritories(string path) {
        string contents = File.ReadAllText(path);

        var options = new JsonSerializerOptions {
            Converters = { new TerritoryJsonConverter() }
        };
        var terrs = JsonSerializer.Deserialize<IDictionary<Territory, ICollection<Territory>>>(contents, options);

        return terrs;
    }

    private static ICollection<Continent>? LoadContinents(string path) {
        string contents = File.ReadAllText(path);

        var conts = JsonSerializer.Deserialize<ICollection<Continent>>(contents);

        return conts;
    }

    private static ICollection<Card>? LoadCards(string path) {
        string contents = File.ReadAllText(path);

        var cards = JsonSerializer.Deserialize<ICollection<Card>>(contents);

        return cards;
    }
}
