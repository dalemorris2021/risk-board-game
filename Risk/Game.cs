using System.Drawing;
using System.Globalization;

namespace Risk;

public class Game {
    const string ENTER_NUM_PLAYERS_MESSAGE = "Please enter a number between 3 and 6.";
    const string END_OF_INPUT_MESSAGE = "Reached end of input";
    const string UNKNOWN_ERROR_MESSAGE = "An unknown error has occurred";
    TextInfo TextInfo;
    Random Random;

    public Game() {
        TextInfo = new CultureInfo("en-US", false).TextInfo;
        Random = new Random();
    }

    public void Run() {
        Console.WriteLine("Welcome to Risk!");
        Console.WriteLine("----------------");
        Console.WriteLine("Need 3 to 6 players to start.");
        Console.WriteLine();

        int numPlayers = GetNumPlayers();

        IList<Player> players = (IList<Player>) CreatePlayers(numPlayers);

        Console.WriteLine("Players: ");
        foreach (Player player in players) {
            Console.WriteLine(player.Name);
        }
        Console.WriteLine();

        players = SetNumArmies(players);
        int numStartingArmies = players[0].NumArmies;

        Console.WriteLine($"Each player will start with {numStartingArmies} armies.");

        players = GetOrderedPlayers(players);

        Console.WriteLine("Players: ");
        foreach (Player player in players) {
            Console.WriteLine(player.Name);
        }
        Console.WriteLine();

        IDictionary<string, Territory> territoriesDict = CreateTerritories();
        
        ClaimTerritories(players, territoriesDict);

        PlaceInitialArmies(players, territoriesDict);

        while (true) {
            bool playerWins = IsWinner(players);
            if (playerWins) {
                break;
            }

            TakeTurn(players, territoriesDict);
        }
    }

    private IDictionary<string, Territory> CreateTerritories() {
        Territory alaska = new("Alaska", "North America");
        Territory northwestTerritory = new("Northwest Territory", "North America");
        Territory alberta = new("Alberta", "North America");
        Territory ontario = new("Ontario", "North America");
        Territory westernUS = new("Western US", "North America");
        Territory easternUS = new("Eastern US", "North America");
        Territory centralAmerica = new("Central America", "North America");
        Territory quebec = new("Quebec", "North America");
        Territory greenland = new("Greenland", "North America");
        Territory iceland = new("Iceland", "Europe");
        Territory greatBritain = new("Great Britain", "Europe");
        Territory northernEurope = new("Northern Europe", "Europe");
        Territory westernEurope = new("Western Europe", "Europe");
        Territory southernEurope = new("Southern Europe", "Europe");
        Territory scandinavia = new("Scandinavia", "Europe");
        Territory ukraine = new("Ukraine", "Europe");
        Territory afghanistan = new("Afghanistan", "Asia");
        Territory ural = new("Ural", "Asia");
        Territory siberia = new("Siberia", "Asia");
        Territory yakutsk = new("Yakutsk", "Asia");
        Territory kamchatka = new("Kamchatka", "Asia");
        Territory irkutsk = new("Irkutsk", "Asia");
        Territory mongolia = new("Mongolia", "Asia");
        Territory china = new("China", "Asia");
        Territory middleEast = new("Middle East", "Asia");
        Territory india = new("India", "Asia");
        Territory siam = new("Siam", "Asia");
        Territory japan = new("Japan", "Asia");
        Territory venezuela = new("Venezuela", "South America");
        Territory peru = new("Peru", "South America");
        Territory brazil = new("Brazil", "South America");
        Territory argentina = new("Argentina", "South America");
        Territory northAfrica = new("North Africa", "Africa");
        Territory egypt = new("Egypt", "Africa");
        Territory eastAfrica = new("East Africa", "Africa");
        Territory congo = new("Congo", "Africa");
        Territory southAfrica = new("South Africa", "Africa");
        Territory madagascar = new("Madagascar", "Africa");
        Territory indonesia = new("Indonesia", "Australia");
        Territory newGuinea = new("New Guinea", "Australia");
        Territory westernAustralia = new("Western Australia", "Australia");
        Territory easternAustralia = new("Eastern Australia", "Australia");

        alaska.Neighbors = [northwestTerritory, alberta, kamchatka];
        northwestTerritory.Neighbors = [alaska, alberta, ontario];
        alberta.Neighbors = [alaska, northwestTerritory, ontario, westernUS];
        ontario.Neighbors = [northwestTerritory, alberta, westernUS, easternUS, quebec, greenland];
        westernUS.Neighbors = [alberta, ontario, easternUS, centralAmerica];
        easternUS.Neighbors = [ontario, westernUS, centralAmerica, quebec];
        centralAmerica.Neighbors = [westernUS, easternUS, venezuela];
        quebec.Neighbors = [ontario, easternUS, greenland];
        greenland.Neighbors = [northwestTerritory, ontario, quebec, iceland];
        iceland.Neighbors = [greenland, greatBritain, scandinavia];
        greatBritain.Neighbors = [iceland, northernEurope, westernEurope, scandinavia];
        northernEurope.Neighbors = [iceland, westernEurope, southernEurope, scandinavia, ukraine];
        westernEurope.Neighbors = [greatBritain, northernEurope, southernEurope, northAfrica];
        southernEurope.Neighbors = [northernEurope, westernEurope, ukraine, northAfrica, egypt];
        scandinavia.Neighbors = [iceland, greatBritain, northernEurope, ukraine];
        ukraine.Neighbors = [northernEurope, southernEurope, scandinavia, afghanistan, ural, middleEast];
        afghanistan.Neighbors = [ukraine, ural, china, middleEast, india];
        ural.Neighbors = [ukraine, afghanistan, siberia, china];
        siberia.Neighbors = [ural, yakutsk, irkutsk, mongolia, china];
        yakutsk.Neighbors = [siberia, kamchatka, irkutsk];
        kamchatka.Neighbors = [alaska, yakutsk, irkutsk, mongolia, japan];
        irkutsk.Neighbors = [siberia, yakutsk, kamchatka, mongolia];
        mongolia.Neighbors = [siberia, kamchatka, irkutsk, china, japan];
        china.Neighbors = [afghanistan, ural, siberia, mongolia, india, siam];
        middleEast.Neighbors = [southernEurope, ukraine, afghanistan, india, egypt, eastAfrica];
        india.Neighbors = [afghanistan, china, middleEast, siam];
        siam.Neighbors = [china, india, indonesia];
        japan.Neighbors = [kamchatka, mongolia];
        venezuela.Neighbors = [centralAmerica, peru, brazil];
        peru.Neighbors = [venezuela, brazil, argentina];
        brazil.Neighbors = [venezuela, peru, argentina, northAfrica];
        argentina.Neighbors = [peru, brazil];
        northAfrica.Neighbors = [westernEurope, southernEurope, brazil, egypt, easternAustralia, congo];
        egypt.Neighbors = [southernEurope, middleEast, northAfrica, eastAfrica];
        eastAfrica.Neighbors = [middleEast, northAfrica, egypt, congo, southAfrica, madagascar];
        congo.Neighbors = [northAfrica, eastAfrica, southAfrica];
        southAfrica.Neighbors = [eastAfrica, congo, madagascar];
        madagascar.Neighbors = [eastAfrica, southAfrica];
        indonesia.Neighbors = [siam, newGuinea, westernAustralia];
        newGuinea.Neighbors = [indonesia, westernAustralia, easternAustralia];
        westernAustralia.Neighbors = [indonesia, newGuinea, easternAustralia];
        easternAustralia.Neighbors = [newGuinea, westernAustralia];

        IList<Territory> terrs = [
            alaska,
            northwestTerritory,
            alberta,
            ontario,
            westernUS,
            easternUS,
            centralAmerica,
            quebec,
            greenland,
            iceland,
            greatBritain,
            northernEurope,
            westernEurope,
            southernEurope,
            scandinavia,
            ukraine,
            afghanistan,
            ural,
            siberia,
            yakutsk,
            kamchatka,
            irkutsk,
            mongolia,
            china,
            middleEast,
            india,
            siam,
            japan,
            venezuela,
            peru,
            brazil,
            argentina,
            northAfrica,
            egypt,
            eastAfrica,
            congo,
            southAfrica,
            madagascar,
            indonesia,
            newGuinea,
            westernAustralia,
            easternAustralia
        ];

        int[][] coordinates = [[50, 75], [125, 75], [125, 150], [200, 150], [125, 225], [200, 225], [125, 300],
                [275, 150], [325, 75], [375, 175], [375, 250], [450, 250], [375, 325], [450, 325], [450, 175],
                [525, 250], [600, 250], [600, 175], [675, 175], [775, 75], [850, 75], [775, 150], [775, 225],
                [750, 300], [600, 325], [675, 350], [750, 375], [850, 225], [200, 375], [200, 450], [275, 450],
                [200, 525], [425, 425], [500, 425], [500, 500], [425, 500], [500, 575], [575, 575], [825, 450],
                [900, 450], [825, 525], [900, 525]];
        
        for (int i = 0; i < terrs.Count; i++) {
            terrs[i].Coordinates = coordinates[i];
        }

        var terrsDict = new Dictionary<string, Territory>();
        foreach (Territory terr in terrs) {
            terrsDict.Add(terr.Name, terr);
        }

        return terrsDict;
    }
    
    private IEnumerable<Player> CreatePlayers(int numPlayers) {
        IList<Color> colors = [Color.Red, Color.Blue, Color.Green, Color.Purple, Color.Yellow, Color.Orange];
        IList<Player> players = [];
        for (int i = 0; i < numPlayers; i++) {
            Console.WriteLine($"Enter player {i + 1}'s name.");
            string? name = Console.ReadLine(); // Should check name for null value before using
            players.Add(new Player(name, colors[i]));
        }

        if (numPlayers == 2) {
            players.Add(new Player("Neutral", colors[numPlayers]));
        }

        return players;
    }

    private IList<Player> SetNumArmies(IList<Player> players) {
        IList<Player> newPlayers = players;
        
        foreach (Player player in newPlayers) {
            switch (players.Count) {
            case 3: player.NumArmies = 35; break;
            case 4: player.NumArmies = 30; break;
            case 5: player.NumArmies = 25; break;
            case 6: player.NumArmies = 20; break;
            default: throw new ArgumentException("Players should have length between 3 and 6.");
            }
        }

        return newPlayers;
    }

    private int GetDieRoll() {
        return Random.Next(1, 7);
    }

    private IList<Player> GetOrderedPlayers(IList<Player> players) {
        IList<int> rolls = [];

        for (int i = 0; i < players.Count; i++) {
            Console.WriteLine($"Player {i + 1}, press enter to roll die.");
            Console.ReadLine(); // Should wait for input
            int roll = GetDieRoll();
            Console.WriteLine(roll);
            Console.WriteLine();
            rolls.Add(roll);
        }

        IList<Player> sortedPlayers = [];
        for (int i = 0; i < players.Count; i++) {
            sortedPlayers[i] = players[rolls.IndexOf(Max(rolls.ToArray()))];
            players.Remove(players[rolls.IndexOf(Max(rolls.ToArray()))]);
            rolls.Remove(Max(rolls.ToArray()));
        }

        return sortedPlayers;
    }

    private int Max(int[] nums) {
        int max = nums[0];

        for (int i = 1; i < nums.Length; i++) {
            if (nums[i] > max) {
                max = nums[i];
            }
        }

        return max;
    }

    private int GetNumPlayers() {
        Console.WriteLine("How many players are there?");
        int numPlayers;
        while (true) {
            string? input = Console.ReadLine();
            if (input == null) {
                Console.WriteLine(END_OF_INPUT_MESSAGE);
                throw new EndOfStreamException(END_OF_INPUT_MESSAGE);
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

        return numPlayers;
    }

    private void ClaimTerritories(IList<Player> players, IDictionary<string, Territory> territories) {
        int currentPlayerIndex = 0;

        int i = 0;
        while (i < territories.Count) {
            Console.WriteLine($"Player {i + 1}, claim a territory.");
            string? input = Console.ReadLine(); // Should check input for null
            string selection = TextInfo.ToTitleCase(input);

            if (!territories.Keys.Contains(selection)) {
                Console.WriteLine("Invalid. Try again.");
            } else {
                players[currentPlayerIndex].PlaceArmy(territories[selection]);
                players[currentPlayerIndex].NumTerritoriesOwned += 1;
                currentPlayerIndex = (currentPlayerIndex + 1) % players.Count;
                i++;
            }
        }
    }

    private void PlaceInitialArmies(IList<Player> players, IDictionary<string, Territory> territories) {
        int currentPlayerIndex = 0;

        while (players[currentPlayerIndex].NumArmies > 0) {
            Console.WriteLine($"Player {currentPlayerIndex}, place an army on an owned territory.");
            String? input = Console.ReadLine(); // Should check input for null
            string selection = TextInfo.ToTitleCase(input);

            if (!territories.Keys.Contains(selection)) {
                Console.WriteLine("Invalid. Try again.");
            } else if (players[currentPlayerIndex] != territories[selection].Player) {
                Console.WriteLine("You do not own this territory.");
            } else {
                players[currentPlayerIndex].PlaceArmy(territories[selection]);
                players[currentPlayerIndex].NumTerritoriesOwned += 1;
                currentPlayerIndex = (currentPlayerIndex + 1) % players.Count;
            }
        }
    }

    private bool IsWinner(IList<Player> players) {
        foreach (Player player in players) {
            if (player.NumTerritoriesOwned == 42) {
                return true;
            }
        }

        return false;
    }

    private void TakeTurn(IList<Player> players, IDictionary<string, Territory> terrs) {
        int i = 0;
        int currentPlayer = 0;
        while (i < players.Count) {
            if (players[i].NumArmies > 0) {
                Console.WriteLine("Deploy armies!");
                DeployArmies(players[i], terrs);
            }

            Console.WriteLine($"Player {players[currentPlayer].Name}, will you attack? (Y/N)");
            string? input = Console.ReadLine(); // Should check for null before using
            string answer = TextInfo.ToTitleCase(input);

            if (answer == "Y") {
                Attack(players[currentPlayer], terrs);
            } else if (answer == "N") {
                Console.WriteLine($"Player {players[currentPlayer].Name}, will you fortify? (Y/N)");
                input = Console.ReadLine(); // Should check for null before using
                answer = TextInfo.ToTitleCase(input);
                if (answer == "Y") {
                    Console.WriteLine("Fortify!");
                    Fortify(players[currentPlayer], terrs);
                    currentPlayer = (currentPlayer + 1) % players.Count;
                    i += 1;
                } else if (answer == "N") {
                    currentPlayer = (currentPlayer + 1) % players.Count;
                    i += 1;
                } else { // Getting here will restart the whole loop, but it should only go back to last input
                    Console.WriteLine("Invalid!");
                }
            } else {
                Console.WriteLine("Invalid!");
            }
        }
    }

    private void DeployArmies(Player player, IDictionary<string, Territory> terrs) {
        while (player.NumArmies != 0) {
            Console.WriteLine($"Player {player.Name}, select a territory to place an army");
            string? input = Console.ReadLine(); // Should check for null before using
            string terrName = TextInfo.ToTitleCase(input);

            if (!terrs.ContainsKey(terrName)) {
                Console.WriteLine("Invalid! Try again.");
            } else if (player != terrs[terrName].Player) {
                Console.WriteLine("Not you territory!");
            } else {
                Console.WriteLine($"You have {player.NumArmies} to deploy.");
                int numArmies;
                if (Int32.TryParse(input, out numArmies)) {
                    player.PlaceArmy(terrs[terrName], numArmies);
                    Console.WriteLine($"{numArmies} armies have been moved to {terrs[terrName].Name}!");
                } else {
                    Console.WriteLine("Input was not a number! Try again.");
                }
            }
        }
    }

    private void Fortify(Player player, IDictionary<string, Territory> terrs) {
        Console.WriteLine($"Player {player.Name}, select a territory to move armies from.");
        string? input = Console.ReadLine(); // Should check for null before using
        string fromTerrName = TextInfo.ToTitleCase(input);

        Console.WriteLine("Select a territory to place armies.");
        input = Console.ReadLine(); // Should check for null before using
        string toTerrName = TextInfo.ToTitleCase(input);

        if (!terrs.ContainsKey(fromTerrName) || !terrs.ContainsKey(toTerrName)) {
            Console.WriteLine("Invalid! Try again.");
            Fortify(player, terrs);
        } else if (player == terrs[fromTerrName].Player && player == terrs[toTerrName].Player) {
            player.PlaceArmyFortify(terrs[fromTerrName], terrs[toTerrName]);
        } else {
            Console.WriteLine("You must select territories you own!");
            Fortify(player, terrs);
        }
    }

    private void Attack(Player player, IDictionary<string, Territory> terrs) {
        Console.WriteLine($"Player {player.Name}, what territory will you attack?");
        string? input = Console.ReadLine(); // Should check for null before using
        string defendTerrName = TextInfo.ToTitleCase(input);

        if (!terrs.ContainsKey(defendTerrName)) {
            Console.WriteLine("Invalid territory!");
        } else if (player == terrs[defendTerrName].Player) {
            Console.WriteLine("You own this territory!");
            return;
        }

        Console.WriteLine($"Player {player.Name}, what territory will you attack from?");
        input = Console.ReadLine(); // Should check for null before using
        string attackTerrName = TextInfo.ToTitleCase(input);

        if (!terrs.ContainsKey(attackTerrName)) {
            Console.WriteLine("Invalid territory!");
            return;
        }

        if (terrs[attackTerrName].NumArmies <= 1) {
            Console.WriteLine("Not enough armies to attack!");
            return;
        }

        bool isNeighbor = terrs[defendTerrName].IsNeighbor(terrs[attackTerrName]);
        terrs[attackTerrName].PrintNeighbors();

        if (!isNeighbor) {
            Console.WriteLine("These territories are not neighbors!");
            return;
        } else {
            StartAttack(terrs[attackTerrName], terrs[defendTerrName], player,
                    terrs[defendTerrName].Player); // Should verify that defendTerr is occupied
        }
    }

    private void StartAttack(Territory attackTerr, Territory defendTerr,
            Player attackPlayer, Player defendPlayer) {
        List<int> attackRolls = [];
        List<int> defendRolls = [];
        while (attackTerr.NumArmies >= 2) {
            if (attackTerr.NumArmies >= 4) {
                for (int i = 0; i < 3; i++) {
                    attackRolls.Add(GetDieRoll());
                }
            } else {
                for (int i = 0; i < 2; i++) {
                    attackRolls.Add(GetDieRoll());
                }
            } // There should be separate cases for NumArmies == 2, 3, and 4+
            attackRolls.Sort((a, b) => b.CompareTo(a)); // Sorts in descending order

            if (defendTerr.NumArmies >= 2) { // Should make sure defendRolls.Count <= attackRolls.Count
                for (int i = 0; i < 2; i++) {
                    defendRolls.Add(GetDieRoll());
                }
            } else {
                for (int i = 0; i < 1; i++) {
                    defendRolls.Add(GetDieRoll());
                }
            }
            defendRolls.Sort((a, b) => b.CompareTo(a)); // Sorts in descending order

            if (defendRolls.Count == 2) {
                if (attackRolls[0] > defendRolls[0] && attackRolls[1] > defendRolls[1]) {
                    defendTerr.NumArmies -= 2;
                    Console.WriteLine("Defending territory lost 2 army!");
                } else if (attackRolls[0] < defendRolls[0] && attackRolls[1] < defendRolls[1]) {
                    attackTerr.NumArmies -= 2;
                    Console.WriteLine("Attacking territory lost 2 army!");
                } else if (attackRolls[0] > defendRolls[0] && attackRolls[1] < defendRolls[1]) {
                    attackTerr.NumArmies -= 1;
                    defendTerr.NumArmies -= 1;
                    Console.WriteLine("Both territories lost 1 army!");
                } else if (attackRolls[0] < defendRolls[0] && attackRolls[1] > defendRolls[1]) {
                    attackTerr.NumArmies -= 1;
                    defendTerr.NumArmies -= 1;
                    Console.WriteLine("Both territories lost 1 army!");
                } // Defender should win ties
            } else if (defendRolls.Count == 1) {
                if (attackRolls[0] > defendRolls[0]) {
                    defendTerr.NumArmies -= 1;
                    Console.WriteLine("Defending territory lost 1 army!");
                } else if (attackRolls[0] < defendRolls[0]) {
                    attackTerr.NumArmies -= 1;
                } // Defender should win ties
            }

            if (defendTerr.NumArmies == 0) {
                PlaceArmyWinner(attackPlayer, defendTerr, attackTerr.NumArmies - 1);
                Console.WriteLine("Attacking player has won the territory! Your armies now occupy it.");
                break;
            } else if (attackTerr.NumArmies == 1) {
                Console.WriteLine("Defending player has retained the territory!");
                break;
            }
        }
    }

    private void PlaceArmyWinner(Player player, Territory terr, int numArmies) {
        Console.WriteLine($"{numArmies} armies have been moved to {terr.Name}!");
        if (terr.Player != player) {
            terr.Player = player;
        }
        terr.NumArmies += numArmies;
    }
}
