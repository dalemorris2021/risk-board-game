namespace Risk;

using System.Text.Json;

public class Game {
    public void Run() {
        var terrs = LoadTerritories("data/territories.json");
        if (terrs == null) {
            Console.Error.WriteLine("Could not load territories");
        }

        var conts = LoadContinents("data/continents.json");
        if (conts == null) {
            Console.Error.WriteLine("Could not load continents");
            return;
        }

        var cards = LoadCards("data/cards.json");
        if (cards == null) {
            Console.Error.WriteLine("Could not load cards");
        }

        var options = new JsonSerializerOptions {
            WriteIndented = true,
            Converters = { new TerritoryJsonConverter() },
        };
        string terrsData = JsonSerializer.Serialize(terrs, options);
        string contsData = JsonSerializer.Serialize(conts, options);
        string cardsData = JsonSerializer.Serialize(cards, options);

        Console.WriteLine(terrsData);
        Console.WriteLine(contsData);
        Console.WriteLine(cardsData);
    }

    private static IDictionary<Territory, IList<Territory>>? LoadTerritories(string path) {
        string contents = File.ReadAllText(path);

        var options = new JsonSerializerOptions {
            Converters = { new TerritoryJsonConverter() }
        };
        var terrsDict = JsonSerializer.Deserialize<IDictionary<Territory, IList<Territory>>>(contents, options);

        return terrsDict;
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

    private static IDictionary<Territory, IList<Territory>> CreateTerritories() {
        Territory alaska = new("Alaska");
        Territory northwestTerritory = new("Northwest Territory");
        Territory alberta = new("Alberta");
        Territory ontario = new("Ontario");
        Territory westernUS = new("Western US");
        Territory easternUS = new("Eastern US");
        Territory centralAmerica = new("Central America");
        Territory quebec = new("Quebec");
        Territory greenland = new("Greenland");
        Territory iceland = new("Iceland");
        Territory greatBritain = new("Great Britain");
        Territory northernEurope = new("Northern Europe");
        Territory westernEurope = new("Western Europe");
        Territory southernEurope = new("Southern Europe");
        Territory scandinavia = new("Scandinavia");
        Territory ukraine = new("Ukraine");
        Territory afghanistan = new("Afghanistan");
        Territory ural = new("Ural");
        Territory siberia = new("Siberia");
        Territory yakutsk = new("Yakutsk");
        Territory kamchatka = new("Kamchatka");
        Territory irkutsk = new("Irkutsk");
        Territory mongolia = new("Mongolia");
        Territory china = new("China");
        Territory middleEast = new("Middle East");
        Territory india = new("India");
        Territory siam = new("Siam");
        Territory japan = new("Japan");
        Territory venezuela = new("Venezuela");
        Territory peru = new("Peru");
        Territory brazil = new("Brazil");
        Territory argentina = new("Argentina");
        Territory northAfrica = new("North Africa");
        Territory egypt = new("Egypt");
        Territory eastAfrica = new("East Africa");
        Territory congo = new("Congo");
        Territory southAfrica = new("South Africa");
        Territory madagascar = new("Madagascar");
        Territory indonesia = new("Indonesia");
        Territory newGuinea = new("New Guinea");
        Territory westernAustralia = new("Western Australia");
        Territory easternAustralia = new("Eastern Australia");

        var terrsDict = new Dictionary<Territory, List<Territory>> {
            { alaska, [northwestTerritory, alberta, kamchatka] },
            { northwestTerritory, [alaska, alberta, ontario] },
            { alberta, [alaska, northwestTerritory, ontario, westernUS] },
            { ontario, [northwestTerritory, alberta, westernUS, easternUS, quebec, greenland] },
            { westernUS, [alberta, ontario, easternUS, centralAmerica] },
            { easternUS, [ontario, westernUS, centralAmerica, quebec] },
            { centralAmerica, [westernUS, easternUS, venezuela] },
            { quebec, [ontario, easternUS, greenland] },
            { greenland, [northwestTerritory, ontario, quebec, iceland] },
            { iceland, [greenland, greatBritain, scandinavia] },
            { greatBritain, [iceland, northernEurope, westernEurope, scandinavia] },
            { northernEurope, [iceland, westernEurope, southernEurope, scandinavia, ukraine] },
            { westernEurope, [greatBritain, northernEurope, southernEurope, northAfrica] },
            { southernEurope, [northernEurope, westernEurope, ukraine, northAfrica, egypt] },
            { scandinavia, [iceland, greatBritain, northernEurope, ukraine] },
            { ukraine, [northernEurope, southernEurope, scandinavia, afghanistan, ural, middleEast] },
            { afghanistan, [ukraine, ural, china, middleEast, india] },
            { ural, [ukraine, afghanistan, siberia, china] },
            { siberia, [ural, yakutsk, irkutsk, mongolia, china] },
            { yakutsk, [siberia, kamchatka, irkutsk] },
            { kamchatka, [alaska, yakutsk, irkutsk, mongolia, japan] },
            { irkutsk, [siberia, yakutsk, kamchatka, mongolia] },
            { mongolia, [siberia, kamchatka, irkutsk, china, japan] },
            { china, [afghanistan, ural, siberia, mongolia, india, siam] },
            { middleEast, [southernEurope, ukraine, afghanistan, india, egypt, eastAfrica] },
            { india, [afghanistan, china, middleEast, siam] },
            { siam, [china, india, indonesia] },
            { japan, [kamchatka, mongolia] },
            { venezuela, [centralAmerica, peru, brazil] },
            { peru, [venezuela, brazil, argentina] },
            { brazil, [venezuela, peru, argentina, northAfrica] },
            { argentina, [peru, brazil] },
            { northAfrica, [westernEurope, southernEurope, brazil, egypt, easternAustralia, congo] },
            { egypt, [southernEurope, middleEast, northAfrica, eastAfrica] },
            { eastAfrica, [middleEast, northAfrica, egypt, congo, southAfrica, madagascar] },
            { congo, [northAfrica, eastAfrica, southAfrica] },
            { southAfrica, [eastAfrica, congo, madagascar] },
            { madagascar, [eastAfrica, southAfrica] },
            { indonesia, [siam, newGuinea, westernAustralia] },
            { newGuinea, [indonesia, westernAustralia, easternAustralia] },
            { westernAustralia, [indonesia, newGuinea, easternAustralia] },
            { easternAustralia, [newGuinea, westernAustralia] },
        };

        return (IDictionary<Territory, IList<Territory>>) terrsDict;
    }

    public ICollection<Continent> CreateContinents() {
        Territory alaska = new("Alaska");
        Territory northwestTerritory = new("Northwest Territory");
        Territory alberta = new("Alberta");
        Territory ontario = new("Ontario");
        Territory westernUS = new("Western US");
        Territory easternUS = new("Eastern US");
        Territory centralAmerica = new("Central America");
        Territory quebec = new("Quebec");
        Territory greenland = new("Greenland");
        Territory iceland = new("Iceland");
        Territory greatBritain = new("Great Britain");
        Territory northernEurope = new("Northern Europe");
        Territory westernEurope = new("Western Europe");
        Territory southernEurope = new("Southern Europe");
        Territory scandinavia = new("Scandinavia");
        Territory ukraine = new("Ukraine");
        Territory afghanistan = new("Afghanistan");
        Territory ural = new("Ural");
        Territory siberia = new("Siberia");
        Territory yakutsk = new("Yakutsk");
        Territory kamchatka = new("Kamchatka");
        Territory irkutsk = new("Irkutsk");
        Territory mongolia = new("Mongolia");
        Territory china = new("China");
        Territory middleEast = new("Middle East");
        Territory india = new("India");
        Territory siam = new("Siam");
        Territory japan = new("Japan");
        Territory venezuela = new("Venezuela");
        Territory peru = new("Peru");
        Territory brazil = new("Brazil");
        Territory argentina = new("Argentina");
        Territory northAfrica = new("North Africa");
        Territory egypt = new("Egypt");
        Territory eastAfrica = new("East Africa");
        Territory congo = new("Congo");
        Territory southAfrica = new("South Africa");
        Territory madagascar = new("Madagascar");
        Territory indonesia = new("Indonesia");
        Territory newGuinea = new("New Guinea");
        Territory westernAustralia = new("Western Australia");
        Territory easternAustralia = new("Eastern Australia");

        Continent northAmerica = new("North America", 5, 
                [alaska, northwestTerritory, alberta, ontario, westernUS, easternUS, centralAmerica, quebec, greenland]
        );
        Continent europe = new("Europe", 5,
                [iceland, greatBritain, northernEurope, westernEurope, southernEurope, scandinavia, ukraine]
        );
        Continent asia = new("Asia", 7,
                [afghanistan, ural, siberia, yakutsk, kamchatka, irkutsk, mongolia, china, middleEast, india, siam, japan]
        );
        Continent southAmerica = new("South America", 2,
                [venezuela, peru, brazil, argentina]
        );
        Continent africa = new("Africa", 3,
                [northAfrica, egypt, eastAfrica, congo, southAfrica, madagascar]
        );
        Continent australia = new("Australia", 2,
                [indonesia, newGuinea, westernAustralia, easternAustralia]
        );

        return [northAmerica, europe, asia, southAmerica, africa, australia];
    }

    public ICollection<Card> CreateCards() {
        return new Card[] {
            new("Alaska", CardType.Infantry),
            new("Northwest Territory", CardType.Cavalry),
            new("Alberta", CardType.Artillery),
            new("Ontario", CardType.Infantry),
            new("Western US", CardType.Cavalry),
            new("Eastern US", CardType.Artillery),
            new("Central America", CardType.Infantry),
            new("Quebec", CardType.Cavalry),
            new("Greenland", CardType.Artillery),
            new("Iceland", CardType.Infantry),
            new("Great Britain", CardType.Cavalry),
            new("Northern Europe", CardType.Artillery),
            new("Western Europe", CardType.Infantry),
            new("Southern Europe", CardType.Cavalry),
            new("Scandinavia", CardType.Artillery),
            new("Ukraine", CardType.Infantry),
            new("Afghanistan", CardType.Cavalry),
            new("Ural", CardType.Artillery),
            new("Siberia", CardType.Infantry),
            new("Yakutsk", CardType.Cavalry),
            new("Kamchatka", CardType.Artillery),
            new("Irkutsk", CardType.Infantry),
            new("Mongolia", CardType.Cavalry),
            new("China", CardType.Artillery),
            new("Middle East", CardType.Infantry),
            new("India", CardType.Cavalry),
            new("Siam", CardType.Artillery),
            new("Japan", CardType.Infantry),
            new("Venezuela", CardType.Cavalry),
            new("Peru", CardType.Artillery),
            new("Brazil", CardType.Infantry),
            new("Argentina", CardType.Cavalry),
            new("North Africa", CardType.Artillery),
            new("Egypt", CardType.Infantry),
            new("East Africa", CardType.Cavalry),
            new("Congo", CardType.Artillery),
            new("South Africa", CardType.Infantry),
            new("Madagascar", CardType.Cavalry),
            new("Indonesia", CardType.Artillery),
            new("New Guinea", CardType.Infantry),
            new("Western Australia", CardType.Cavalry),
            new("Eastern Australia", CardType.Artillery),
        };
    }
}
