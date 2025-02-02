using System.Drawing;
using System.Globalization;

namespace Risk;

public class Game {
    private const string ENTER_NUM_PLAYERS_MESSAGE = "Please enter a number between 3 and 6.";
    private const string END_OF_INPUT_MESSAGE = "Reached end of input";
    private const string UNKNOWN_ERROR_MESSAGE = "An unknown error has occurred";
    public IList<IPlayer> Players { get; private set; }
    public int PlayerTurn { get; private set; }
    public IDictionary<string, Territory> Territories { get; private set; }
    public TextInfo TextInfo { get; }
    public Random Random { get; }

    public Game() {
        TextInfo = new CultureInfo("en-US", false).TextInfo;
        Random = new Random();
        Players = [];
        Territories = new Dictionary<string, Territory>();
    }

    public void Run() {
        Console.WriteLine("Welcome to Risk!");
        Console.WriteLine("----------------");
        Console.WriteLine("Need 2 to 6 players to start.");
        Console.WriteLine();

        int numPlayers = GetNumPlayers();
        Console.WriteLine();
        Players = CreatePlayers(numPlayers);
        Console.WriteLine();

        Console.WriteLine("Players: ");
        foreach (IPlayer player in Players) {
            Console.WriteLine(player.Name);
        }
        Console.WriteLine();

        Players = SetNumArmies(Players);
        int numStartingArmies = Players[0].NumArmies;
        Console.WriteLine($"Each player will start with {numStartingArmies} armies.");

        Players = GetOrderedPlayers(Players);

        Console.WriteLine("Players: ");
        foreach (IPlayer player in Players) {
            Console.WriteLine(player.Name);
        }
        Console.WriteLine();

        Territories = CreateTerritories();
        
        DistributeTerritories(Players, Territories);
        foreach (IPlayer player in Players) {
            Console.WriteLine(player.Name);
            foreach (Territory terr in TerritoriesConquered(player, Territories)) {
                Console.WriteLine($"* {terr.Name}");
            }
            Console.WriteLine();
        }

        DistributeArmies(Players, Territories);
        Console.WriteLine("The armies have been evenly distributed.");

        while (true) {
            bool playerWins = HasWinner(Players);
            if (playerWins) {
                break;
            }

            Players[PlayerTurn].TakeTurn(this);
            PlayerTurn = (PlayerTurn + 1) % Players.Count;
        }
    }

    public IList<IPlayer> GetPlayers() {
        return Players;
    }

    public IDictionary<string, Territory> GetTerritories() {
        return Territories;
    }

    private static IDictionary<string, Territory> CreateTerritories() {
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
    
    private static IList<IPlayer> CreatePlayers(int numPlayers) {
        IList<Color> colors = [Color.Red, Color.Blue, Color.Green, Color.Purple, Color.Yellow, Color.Orange];
        IList<IPlayer> players = [];
        for (int i = 0; i < numPlayers; i++) {
            Console.WriteLine($"Enter player {i + 1}'s name.");
            string? name = Console.ReadLine();
            if (name == null) {
                Console.WriteLine(END_OF_INPUT_MESSAGE);
                throw new EndOfStreamException(END_OF_INPUT_MESSAGE);
            }
            players.Add(new Player(name, colors[i]));
        }

        if (numPlayers == 2) {
            players.Add(new NeutralBot(colors[numPlayers]));
        }

        return players;
    }

    private static IList<IPlayer> SetNumArmies(IList<IPlayer> players) {
        IList<IPlayer> newPlayers = players;
        
        foreach (IPlayer player in newPlayers) {
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

    private IList<IPlayer> GetOrderedPlayers(IList<IPlayer> players) {
        int numPlayers = players.Count;
        IList<int> rolls = [];

        for (int i = 0; i < numPlayers; i++) {
            Console.WriteLine($"{players[i].Name}, press enter to roll die.");
            Console.ReadLine();
            int roll = GetDieRoll();
            Console.WriteLine(roll);
            Console.WriteLine();
            rolls.Add(roll);
        }

        IList<IPlayer> sortedPlayers = [];
        for (int i = 0; i < numPlayers; i++) {
            int maxRoll = Max(rolls.ToArray());
            sortedPlayers.Add(players[rolls.IndexOf(maxRoll)]);
            players.Remove(players[rolls.IndexOf(maxRoll)]);
            rolls.Remove(maxRoll);
        }

        return sortedPlayers;
    }

    private static int Max(int[] nums) {
        int max = nums[0];

        for (int i = 1; i < nums.Length; i++) {
            if (nums[i] > max) {
                max = nums[i];
            }
        }

        return max;
    }

    private static int GetNumPlayers() {
        Console.WriteLine("How many players are there?");
        int numPlayers;
        while (true) {
            string? input = Console.ReadLine();
            if (input == null) {
                Console.WriteLine(END_OF_INPUT_MESSAGE);
                throw new EndOfStreamException(END_OF_INPUT_MESSAGE);
            } else if (Int32.TryParse(input, out numPlayers)) {
                if (numPlayers < 2 || numPlayers > 6) {
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

    private void ClaimTerritories(IList<IPlayer> players, IDictionary<string, Territory> territories) {
        int currentPlayerIndex = 0;
        int i = 0;
        while (i < territories.Count) {
            Console.WriteLine($"{players[currentPlayerIndex].Name}, claim a territory.");
            string? input = Console.ReadLine();
            if (input == null) {
                Console.WriteLine(END_OF_INPUT_MESSAGE);
                throw new EndOfStreamException(END_OF_INPUT_MESSAGE);
            }
            string selection = TextInfo.ToTitleCase(input);

            if (!territories.Keys.Contains(selection)) {
                Console.WriteLine("Invalid. Try again.");
            } else {
                PlaceArmy(players[currentPlayerIndex], territories[selection]);
                players[currentPlayerIndex].NumTerritoriesOwned += 1;
                currentPlayerIndex = (currentPlayerIndex + 1) % players.Count;
                i++;
            }
        }
    }

    private void DistributeTerritories(IList<IPlayer> players, IDictionary<string, Territory> territories) {
        IList<string> unusedTerrNames = territories.Keys.ToList();

        int currentPlayerIndex = 0;
        string selection;
        int nameIndex;
        while (unusedTerrNames.Count != 0) {
            nameIndex = Random.Next(unusedTerrNames.Count);
            selection = unusedTerrNames[nameIndex];

            PlaceArmy(players[currentPlayerIndex], territories[selection]);
            players[currentPlayerIndex].NumTerritoriesOwned += 1;

            unusedTerrNames.Remove(selection);

            currentPlayerIndex = (currentPlayerIndex + 1) % players.Count;
        }
    }

    private void PlaceInitialArmies(IList<IPlayer> players, IDictionary<string, Territory> territories) {
        int currentPlayerIndex = 0;

        while (players[currentPlayerIndex].NumArmies > 0) {
            Console.WriteLine($"{players[currentPlayerIndex].Name}, place an army on an owned territory.");
            String? input = Console.ReadLine();
            if (input == null) {
                Console.WriteLine(END_OF_INPUT_MESSAGE);
                throw new EndOfStreamException(END_OF_INPUT_MESSAGE);
            }
            string selection = TextInfo.ToTitleCase(input);

            if (!territories.Keys.Contains(selection)) {
                Console.WriteLine("Invalid. Try again.");
            } else if (players[currentPlayerIndex] != territories[selection].Player) {
                Console.WriteLine("You do not own this territory.");
            } else {
                PlaceArmy(players[currentPlayerIndex], territories[selection]);
                players[currentPlayerIndex].NumTerritoriesOwned += 1;
                currentPlayerIndex = (currentPlayerIndex + 1) % players.Count;
            }
        }
    }

    private void DistributeArmies(IList<IPlayer> players, IDictionary<string, Territory> territories) {
        for (int i = 0; i < players.Count; i++) {
            int terrIndex = 0;
            IDictionary<string, Territory> playerTerrs = GetPlayerTerritories(players[i], territories);
            IList<Territory> playerTerrsList = playerTerrs.Values.ToList();

            while (players[i].NumArmies > 0) {
                PlaceArmy(players[i], playerTerrsList[terrIndex], 1);
                terrIndex++;
                if (terrIndex >= playerTerrsList.Count) {
                    terrIndex = 0;
                }
            }
        }
    }

    private static bool HasWinner(IList<IPlayer> players) {
        foreach (IPlayer player in players) {
            if (player.NumTerritoriesOwned == 42) {
                return true;
            }
        }

        return false;
    }

    public void DeployArmies(IPlayer player, IDictionary<string, Territory> terrs) {
        while (player.NumArmies != 0) {
            Console.WriteLine($"{player.Name}, select a territory to place an army");
            string? input = Console.ReadLine();
            if (input == null) {
                Console.WriteLine(END_OF_INPUT_MESSAGE);
                throw new EndOfStreamException(END_OF_INPUT_MESSAGE);
            }
            string terrName = TextInfo.ToTitleCase(input);

            if (!terrs.ContainsKey(terrName)) {
                Console.WriteLine("Invalid! Try again.");
            } else if (player != terrs[terrName].Player) {
                Console.WriteLine("Not you territory!");
            } else {
                Console.WriteLine($"You have {player.NumArmies} to deploy.");
                int numArmies;
                if (Int32.TryParse(input, out numArmies)) {
                    PlaceArmy(player, terrs[terrName], numArmies);
                    Console.WriteLine($"{numArmies} armies have been moved to {terrs[terrName].Name}!");
                } else {
                    Console.WriteLine("Input was not a number! Try again.");
                }
            }
        }
    }

    public void Fortify(IPlayer player, IDictionary<string, Territory> terrs) {
        Console.WriteLine($"{player.Name}, select a territory to move armies from.");
        string? input = Console.ReadLine();
        if (input == null) {
            Console.WriteLine(END_OF_INPUT_MESSAGE);
            throw new EndOfStreamException(END_OF_INPUT_MESSAGE);
        }
        string fromTerrName = TextInfo.ToTitleCase(input);

        Console.WriteLine("Select a territory to place armies.");
        input = Console.ReadLine();
        if (input == null) {
            Console.WriteLine(END_OF_INPUT_MESSAGE);
            throw new EndOfStreamException(END_OF_INPUT_MESSAGE);
        }
        string toTerrName = TextInfo.ToTitleCase(input);

        if (!terrs.ContainsKey(fromTerrName) || !terrs.ContainsKey(toTerrName)) {
            Console.WriteLine("Invalid! Try again.");
            Fortify(player, terrs);
        } else if (player == terrs[fromTerrName].Player && player == terrs[toTerrName].Player) {
            PlaceArmyFortify(terrs[fromTerrName], terrs[toTerrName]);
        } else {
            Console.WriteLine("You must select territories you own!");
            Fortify(player, terrs);
        }
    }

    public void Attack(IPlayer player, IDictionary<string, Territory> terrs) {
        Console.WriteLine($"{player.Name}, what territory will you attack?");
        string? input = Console.ReadLine();
        if (input == null) {
            Console.WriteLine(END_OF_INPUT_MESSAGE);
            throw new EndOfStreamException(END_OF_INPUT_MESSAGE);
        }
        string defendTerrName = TextInfo.ToTitleCase(input);

        if (!terrs.ContainsKey(defendTerrName)) {
            Console.WriteLine("Invalid territory!");
        } else if (player == terrs[defendTerrName].Player) {
            Console.WriteLine("You own this territory!");
            return;
        }

        Console.WriteLine($"{player.Name}, what territory will you attack from?");
        input = Console.ReadLine();
        if (input == null) {
            Console.WriteLine(END_OF_INPUT_MESSAGE);
            throw new EndOfStreamException(END_OF_INPUT_MESSAGE);
        }
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
            IPlayer attackPlayer, IPlayer defendPlayer) {
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
                Console.WriteLine($"{attackPlayer.Name} has won the territory! Your armies now occupy it.");
                break;
            } else if (attackTerr.NumArmies == 1) {
                Console.WriteLine($"{defendPlayer.Name} has retained the territory!");
                break;
            }
        }
    }

    private static void PlaceArmyWinner(IPlayer player, Territory terr, int numArmies) {
        Console.WriteLine($"{numArmies} armies have been moved to {terr.Name}!");
        if (terr.Player != player) {
            terr.Player = player;
        }
        terr.NumArmies += numArmies;
    }

    public IDictionary<string, Territory> GetPlayerTerritories(IPlayer player, IDictionary<string, Territory> territories) {
        IDictionary<string, Territory> playerTerrs = new Dictionary<string, Territory>();

        foreach (Territory terr in territories.Values) {
            if (player.Equals(terr.Player)) {
                playerTerrs.Add(terr.Name, terr);
            }
        }

        return playerTerrs;
    }

    public IEnumerable<Territory> TerritoriesConquered(IPlayer player, IDictionary<string, Territory> terrsDict) {
        IList<Territory> territories = [];
        foreach (Territory terr in terrsDict.Values) {
            if (player == terr.Player) {
                territories.Add(terr);
            }
        }

        return territories;
    }

    public static void PlaceArmy(IPlayer player, Territory terr, int numArmies = 1) {
        if (terr.Player == null) {
            terr.Player = player;
        }

        if (player.NumArmies >= numArmies) {
            terr.NumArmies += numArmies;
            player.NumArmies -= numArmies;
        }
    }

    public static void PlaceArmyFortify(Territory from, Territory to) {
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
