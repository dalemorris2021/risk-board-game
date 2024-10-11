namespace Risk;

using QuikGraph;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

public class Game {
    public void Run() {
        UndirectedGraph<Territory, SEdge<Territory>> terrs = loadTerritories("data/territories.yml");

        ISerializer serializer = new SerializerBuilder()
                .WithNamingConvention(CamelCaseNamingConvention.Instance)
                .Build();
        string yaml = "---\n"
                + serializer.Serialize(terrs.Vertices)
                + "---\n"
                + serializer.Serialize(terrs.Edges);

        Console.WriteLine(yaml);
    }

    private static UndirectedGraph<Territory, SEdge<Territory>> loadTerritories(string path) {
        string contents = File.ReadAllText(path);
        string[] sections = contents.Split("---").Skip(1).ToArray();
        
        IDeserializer deserializer = new DeserializerBuilder()
                .WithNamingConvention(CamelCaseNamingConvention.Instance)
                .Build();
        
        var territories = (List<Territory>) deserializer.Deserialize(sections[0]);
        var neighbors = (SEdge<Territory>[]) deserializer.Deserialize(sections[1]);
        var territoriesGraph = neighbors.ToUndirectedGraph<Territory, SEdge<Territory>>();

        return territoriesGraph;
    }

    private static UndirectedGraph<Territory, SEdge<Territory>> populateTerritories() {
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

        var neighbors = new SEdge<Territory>[] {
            new SEdge<Territory>(alaska, northwestTerritory),
            new SEdge<Territory>(alaska, alberta),
            new SEdge<Territory>(alaska, kamchatka),
            new SEdge<Territory>(northwestTerritory, alberta),
            new SEdge<Territory>(northwestTerritory, ontario),
            new SEdge<Territory>(northwestTerritory, greenland),
            new SEdge<Territory>(alberta, ontario),
            new SEdge<Territory>(alberta, westernUS),
            new SEdge<Territory>(ontario, westernUS),
            new SEdge<Territory>(ontario, easternUS),
            new SEdge<Territory>(ontario, quebec),
            new SEdge<Territory>(ontario, greenland),
            new SEdge<Territory>(westernUS, easternUS),
            new SEdge<Territory>(westernUS, centralAmerica),
            new SEdge<Territory>(easternUS, centralAmerica),
            new SEdge<Territory>(easternUS, quebec),
            new SEdge<Territory>(centralAmerica, venezuela),
            new SEdge<Territory>(quebec, greenland),
            new SEdge<Territory>(greenland, iceland),
            new SEdge<Territory>(iceland, greatBritain),
            new SEdge<Territory>(iceland, scandinavia),
            new SEdge<Territory>(greatBritain, northernEurope),
            new SEdge<Territory>(greatBritain, westernEurope),
            new SEdge<Territory>(greatBritain, scandinavia),
            new SEdge<Territory>(northernEurope, westernEurope),
            new SEdge<Territory>(northernEurope, southernEurope),
            new SEdge<Territory>(northernEurope, scandinavia),
            new SEdge<Territory>(northernEurope, ukraine),
            new SEdge<Territory>(westernEurope, southernEurope),
            new SEdge<Territory>(westernEurope, northAfrica),
            new SEdge<Territory>(southernEurope, ukraine),
            new SEdge<Territory>(southernEurope, northAfrica),
            new SEdge<Territory>(southernEurope, egypt),
            new SEdge<Territory>(scandinavia, ukraine),
            new SEdge<Territory>(ukraine, afghanistan),
            new SEdge<Territory>(ukraine, ural),
            new SEdge<Territory>(ukraine, middleEast),
            new SEdge<Territory>(afghanistan, ural),
            new SEdge<Territory>(afghanistan, china),
            new SEdge<Territory>(afghanistan, middleEast),
            new SEdge<Territory>(afghanistan, india),
            new SEdge<Territory>(ural, siberia),
            new SEdge<Territory>(ural, china),
            new SEdge<Territory>(siberia, yakutsk),
            new SEdge<Territory>(siberia, irkutsk),
            new SEdge<Territory>(siberia, mongolia),
            new SEdge<Territory>(siberia, china),
            new SEdge<Territory>(yakutsk, kamchatka),
            new SEdge<Territory>(yakutsk, irkutsk),
            new SEdge<Territory>(kamchatka, irkutsk),
            new SEdge<Territory>(kamchatka, mongolia),
            new SEdge<Territory>(kamchatka, japan),
            new SEdge<Territory>(irkutsk, mongolia),
            new SEdge<Territory>(mongolia, china),
            new SEdge<Territory>(mongolia, japan),
            new SEdge<Territory>(china, india),
            new SEdge<Territory>(china, siam),
            new SEdge<Territory>(middleEast, india),
            new SEdge<Territory>(middleEast, egypt),
            new SEdge<Territory>(middleEast, eastAfrica),
            new SEdge<Territory>(india, siam),
            new SEdge<Territory>(siam, indonesia),
            new SEdge<Territory>(venezuela, peru),
            new SEdge<Territory>(venezuela, brazil),
            new SEdge<Territory>(peru, brazil),
            new SEdge<Territory>(peru, argentina),
            new SEdge<Territory>(brazil, argentina),
            new SEdge<Territory>(brazil, northAfrica),
            new SEdge<Territory>(northAfrica, egypt),
            new SEdge<Territory>(northAfrica, eastAfrica),
            new SEdge<Territory>(northAfrica, congo),
            new SEdge<Territory>(egypt, eastAfrica),
            new SEdge<Territory>(eastAfrica, congo),
            new SEdge<Territory>(eastAfrica, southAfrica),
            new SEdge<Territory>(eastAfrica, madagascar),
            new SEdge<Territory>(congo, southAfrica),
            new SEdge<Territory>(southAfrica, madagascar),
            new SEdge<Territory>(indonesia, newGuinea),
            new SEdge<Territory>(indonesia, westernAustralia),
            new SEdge<Territory>(newGuinea, westernAustralia),
            new SEdge<Territory>(newGuinea, easternAustralia),
            new SEdge<Territory>(westernAustralia, easternAustralia),
        };

        var territories = neighbors.ToUndirectedGraph<Territory, SEdge<Territory>>(false);
        return territories;
    }
}
