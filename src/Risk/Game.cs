namespace Risk;

using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

public class Game {
    public void Run() {
        var territories = PopulateTerritories();

        ISerializer serializer = new SerializerBuilder()
                .WithNamingConvention(CamelCaseNamingConvention.Instance)
                .Build();
        string yaml = serializer.Serialize(territories);

        using (StreamWriter streamWriter = new StreamWriter("data/test.yml")) {
            streamWriter.WriteLine(yaml);
        }
        // Console.WriteLine(yaml);
    }

    private static Dictionary<Territory, List<Territory>> LoadTerritories(string path) {
        string contents = File.ReadAllText(path);
        
        IDeserializer deserializer = new DeserializerBuilder()
                .WithNamingConvention(CamelCaseNamingConvention.Instance)
                .Build();
        
        var territoriesDict = (Dictionary<Territory, List<Territory>>) deserializer.Deserialize(contents);

        return territoriesDict;
    }

    private static Dictionary<Territory, List<Territory>> PopulateTerritories() {
        var alaska = new Territory("Alaska");
        var northwestTerritory = new Territory("Northwest Territory");
        var alberta = new Territory("Alberta");
        var ontario = new Territory("Ontario");
        var westernUS = new Territory("Western US");
        var easternUS = new Territory("Eastern US");
        var centralAmerica = new Territory("Central America");
        var quebec = new Territory("Quebec");
        var greenland = new Territory("Greenland");
        var iceland = new Territory("Iceland");
        var greatBritain = new Territory("Great Britain");
        var northernEurope = new Territory("Northern Europe");
        var westernEurope = new Territory("Western Europe");
        var southernEurope = new Territory("Southern Europe");
        var scandinavia = new Territory("Scandinavia");
        var ukraine = new Territory("Ukraine");
        var afghanistan = new Territory("Afghanistan");
        var ural = new Territory("Ural");
        var siberia = new Territory("Siberia");
        var yakutsk = new Territory("Yakutsk");
        var kamchatka = new Territory("Kamchatka");
        var irkutsk = new Territory("Irkutsk");
        var mongolia = new Territory("Mongolia");
        var china = new Territory("China");
        var middleEast = new Territory("Middle East");
        var india = new Territory("India");
        var siam = new Territory("Siam");
        var japan = new Territory("Japan");
        var venezuela = new Territory("Venezuela");
        var peru = new Territory("Peru");
        var brazil = new Territory("Brazil");
        var argentina = new Territory("Argentina");
        var northAfrica = new Territory("North Africa");
        var egypt = new Territory("Egypt");
        var eastAfrica = new Territory("East Africa");
        var congo = new Territory("Congo");
        var southAfrica = new Territory("South Africa");
        var madagascar = new Territory("Madagascar");
        var indonesia = new Territory("Indonesia");
        var newGuinea = new Territory("New Guinea");
        var westernAustralia = new Territory("Western Australia");
        var easternAustralia = new Territory("Eastern Australia");

        var territoriesDict = new Dictionary<Territory, List<Territory>> {
            // { alaska, [northwestTerritory, alberta, kamchatka] },
            // { northwestTerritory, [alaska, alberta, ontario] },
            { indonesia, [newGuinea, westernAustralia] },
            { newGuinea, [indonesia, westernAustralia, easternAustralia] },
            { westernAustralia, [indonesia, newGuinea, easternAustralia] },
            { easternAustralia, [newGuinea, westernAustralia] },
        };

        return territoriesDict;
    }
}
