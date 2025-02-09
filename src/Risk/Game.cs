namespace Risk;

public class Game(IList<IPlayer> players) {
    public IList<IPlayer> Players { get; private set; } = players;
    public int PlayerTurn { get; private set; } = 0;
    public IDictionary<string, Territory> Territories { get; private set; } = new Dictionary<string, Territory>();
    public Random Random { get; } = new Random();
    public IList<Action> Actions { get; private set; } = [];

    public void Run() {
        Players = SetNumArmies(Players);
        int numStartingArmies = Players[0].NumArmies;
        Console.WriteLine($"Each player will start with {numStartingArmies} armies.");

        Console.WriteLine("The order of play will be randomized.");
        Players = GetOrderedPlayers(Players);

        Console.WriteLine("Players:");
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

        IPlayer? winner;
        while ((winner = GetWinner(Players)) == null) {
            Players[PlayerTurn].NumArmies += TerritoriesConquered(Players[PlayerTurn], Territories).Count;

            if (Players[PlayerTurn].NumArmies > 0) {
                Actions = [Action.DEPLOY];
            } else {
                Actions = [Action.ATTACK, Action.FORTIFY];
            }
            
            Players[PlayerTurn].TakeTurn(this);
            PlayerTurn = (PlayerTurn + 1) % Players.Count;
        }

        Console.WriteLine("The game has been decided!");
        Console.WriteLine($"The winner is {winner.Name}!");
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

    private static IList<IPlayer> SetNumArmies(IList<IPlayer> players) {
        IList<IPlayer> newPlayers = players;
        
        foreach (IPlayer player in newPlayers) {
            player.NumArmies = players.Count switch {
                2 => 40,
                3 => 35,
                4 => 30,
                5 => 25,
                6 => 20,
                _ => throw new ArgumentException("Players should have length between 2 and 6."),
            };
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
            int roll = GetDieRoll();
            rolls.Add(roll);
        }

        IList<IPlayer> sortedPlayers = [];
        for (int i = 0; i < numPlayers; i++) {
            int maxRoll = rolls.ToArray().Max();
            sortedPlayers.Add(players[rolls.IndexOf(maxRoll)]);
            players.Remove(players[rolls.IndexOf(maxRoll)]);
            rolls.Remove(maxRoll);
        }

        return sortedPlayers;
    }

    /*
    private void ClaimTerritories(IList<IPlayer> players, IDictionary<string, Territory> territories) {
        int currentPlayerIndex = 0;
        int i = 0;
        while (i < territories.Count) {
            Console.WriteLine($"{players[currentPlayerIndex].Name}, claim a territory.");
            string input = InputHandler.GetInput();
            string selection = TextInfo.ToTitleCase(input);

            if (!territories.ContainsKey(selection)) {
                Console.WriteLine("Invalid. Try again.");
            } else {
                PlaceArmy(players[currentPlayerIndex], territories[selection]);
                players[currentPlayerIndex].NumTerritoriesOwned += 1;
                currentPlayerIndex = (currentPlayerIndex + 1) % players.Count;
                i++;
            }
        }
    }
    */

    private void DistributeTerritories(IList<IPlayer> players, IDictionary<string, Territory> territories) {
        IList<string> unusedTerrNames = [.. territories.Keys];

        int currentPlayerIndex = 0;
        string selection;
        int nameIndex;
        while (unusedTerrNames.Count != 0) {
            nameIndex = Random.Next(unusedTerrNames.Count);
            selection = unusedTerrNames[nameIndex];

            SpecialDeploy(players[currentPlayerIndex], territories[selection]);
            players[currentPlayerIndex].NumTerritoriesOwned += 1;
            unusedTerrNames.Remove(selection);

            currentPlayerIndex = (currentPlayerIndex + 1) % players.Count;
        }
    }

    /*
    private void PlaceInitialArmies(IList<IPlayer> players, IDictionary<string, Territory> territories) {
        int currentPlayerIndex = 0;

        while (players[currentPlayerIndex].NumArmies > 0) {
            Console.WriteLine($"{players[currentPlayerIndex].Name}, place an army on an owned territory.");
            string input = InputHandler.GetInput();
            string selection = TextInfo.ToTitleCase(input);

            if (!territories.ContainsKey(selection)) {
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
    */

    private void DistributeArmies(IList<IPlayer> players, IDictionary<string, Territory> territories) {
        for (int i = 0; i < players.Count; i++) {
            int terrIndex = 0;
            IDictionary<string, Territory> playerTerrs = GetPlayerTerritories(players[i], territories);
            IList<Territory> playerTerrsList = [.. playerTerrs.Values];

            while (players[i].NumArmies > 0) {
                SpecialDeploy(players[i], playerTerrsList[terrIndex], 1);
                terrIndex = (terrIndex + 1) % playerTerrsList.Count;
                if (terrIndex >= playerTerrsList.Count) {
                    terrIndex = 0;
                }
            }
        }
    }

    private static IPlayer? GetWinner(IList<IPlayer> players) {
        foreach (IPlayer player in players) {
            if (player.NumTerritoriesOwned == 42) {
                return player;
            }
        }

        return null;
    }

    public void Attack(Territory attackTerr, Territory defendTerr,
            IPlayer attackPlayer, IPlayer defendPlayer) {
        if (!Actions.Contains(Action.ATTACK)
            || !Territories.ContainsKey(defendTerr.Name)
            || attackPlayer == Territories[defendTerr.Name].Player
            || !Territories.ContainsKey(attackTerr.Name)
            || Territories[attackTerr.Name].NumArmies <= 1
            || !Territories[defendTerr.Name].IsNeighbor(Territories[attackTerr.Name])) {
            return;
        }

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
                } else if (attackRolls[0] <= defendRolls[0] && attackRolls[1] <= defendRolls[1]) {
                    attackTerr.NumArmies -= 2;
                    Console.WriteLine("Attacking territory lost 2 army!");
                } else if (attackRolls[0] > defendRolls[0] && attackRolls[1] <= defendRolls[1]) {
                    attackTerr.NumArmies -= 1;
                    defendTerr.NumArmies -= 1;
                    Console.WriteLine("Both territories lost 1 army!");
                } else if (attackRolls[0] <= defendRolls[0] && attackRolls[1] > defendRolls[1]) {
                    attackTerr.NumArmies -= 1;
                    defendTerr.NumArmies -= 1;
                    Console.WriteLine("Both territories lost 1 army!");
                }
            } else if (defendRolls.Count == 1) {
                if (attackRolls[0] > defendRolls[0]) {
                    defendTerr.NumArmies -= 1;
                    Console.WriteLine("Defending territory lost 1 army!");
                } else if (attackRolls[0] <= defendRolls[0]) {
                    attackTerr.NumArmies -= 1;
                    Console.WriteLine("Attacking territory lost 1 army!");
                }
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

    public IList<Territory> TerritoriesConquered(IPlayer player, IDictionary<string, Territory> terrsDict) {
        IList<Territory> territories = [];
        foreach (Territory terr in terrsDict.Values) {
            if (player == terr.Player) {
                territories.Add(terr);
            }
        }

        return territories;
    }

    private void SpecialDeploy(IPlayer player, Territory terr, int numArmies = 1) {
        if (!Actions.Contains(Action.DEPLOY)
            || !Territories.ContainsKey(terr.Name)) { // Players shouldn't be able to call this directly if they don't own the territory
            return;
        }

        if (terr.Player == null) {
            terr.Player = player;
        }

        if (player.NumArmies >= numArmies) {
            terr.NumArmies += numArmies;
            player.NumArmies -= numArmies;
        }
    }

    public void Deploy(IPlayer player, Territory terr, int numArmies = 1) {
        if (!Actions.Contains(Action.DEPLOY)
            || !Territories.ContainsKey(terr.Name)
            || player != terr.Player) { // Players shouldn't be able to call this directly if they don't own the territory
            return;
        }

        if (terr.Player == null) {
            terr.Player = player;
        }

        if (player.NumArmies >= numArmies) {
            terr.NumArmies += numArmies;
            player.NumArmies -= numArmies;
        }

        if (player.NumArmies == 0) {
            Actions = [Action.ATTACK, Action.FORTIFY];
        }
    }

    public void Fortify(int numArmies, Territory from, Territory to) {
        if (!Actions.Contains(Action.FORTIFY)
            || !Territories.ContainsKey(from.Name) || !Territories.ContainsKey(to.Name)
            || !(this == Territories[from.Name].Player) || !(this == Territories[to.Name].Player)
            || numArmies >= from.NumArmies) {
            return;
        }

        to.NumArmies += numArmies;
        from.NumArmies -= numArmies;

        Actions = [];
    }
}
