namespace Risk;

using System.Text.Json;

public class Game {
    public void Run() {
        var terrs = LoadTerritories("data/test.json");
        if (terrs == null) {
            Console.Error.WriteLine("Could not load data");
            return;
        }

        /*ISerializer serializer = new SerializerBuilder()
                .WithNamingConvention(CamelCaseNamingConvention.Instance)
                .Build();
        string data = serializer.Serialize(terrs);*/

        var options = new JsonSerializerOptions {
            WriteIndented = true,
            Converters = { new TerritoryJsonConverter() },
        };
        string data = JsonSerializer.Serialize(terrs, options);

        /*using (StreamWriter streamWriter = new StreamWriter("data/test.json")) {
            streamWriter.WriteLine(data);
        }*/
        Console.WriteLine(data);
    }

    private static Dictionary<Territory, List<Territory>>? LoadTerritories(string path) {
        string contents = File.ReadAllText(path);
        
        /*IDeserializer deserializer = new DeserializerBuilder()
                .WithNamingConvention(CamelCaseNamingConvention.Instance)
                .Build();
        
        var terrsDict = (Dictionary<string, List<Territory>>?) deserializer.Deserialize(contents);*/
        var options = new JsonSerializerOptions {
            Converters = { new TerritoryJsonConverter() }
        };
        var terrsDict = JsonSerializer.Deserialize<Dictionary<Territory, List<Territory>>>(contents, options);

        return terrsDict;
    }

    private static Dictionary<Territory, List<Territory>> PopulateTerritories() {
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
            { indonesia, [newGuinea, westernAustralia] },
            { newGuinea, [indonesia, westernAustralia, easternAustralia] },
            { westernAustralia, [indonesia, newGuinea, easternAustralia] },
            { easternAustralia, [newGuinea, westernAustralia] },
        };

        return terrsDict;
    }
}
