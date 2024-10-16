namespace Risk;

using System.Text.Json;

public class Game {
    public void Run() {
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
