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
        var terrs = JsonSerializer.Deserialize<IDictionary<Territory, IList<Territory>>>(contents, options);

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
