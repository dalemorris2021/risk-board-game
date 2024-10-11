namespace Risk;

using QuikGraph;
using QuikGraph.Collections;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

public class Game {
    public void Run() {
        Territory Alaska = new Territory("Alaska");
        Territory NorthwestTerritory = new Territory("Northwest Territory");
        Territory Ontario = new Territory("Ontario");

        var neighbors = new SEdge<Territory>[] {
            new SEdge<Territory>(Alaska, NorthwestTerritory),
            new SEdge<Territory>(NorthwestTerritory, Ontario),
        };
        var terrs = neighbors.ToUndirectedGraph<Territory, SEdge<Territory>>(false);

        ISerializer serializer = new SerializerBuilder()
                .WithNamingConvention(CamelCaseNamingConvention.Instance)
                .Build();
        string yaml = "---\n"
                + serializer.Serialize(terrs.Vertices)
                + "---\n"
                + serializer.Serialize(terrs.Edges);

        Console.WriteLine(yaml);
    }
}
