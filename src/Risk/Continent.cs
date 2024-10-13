namespace Risk;

/*using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization;*/

public record Continent(string Name, uint ArmyBonus, ICollection<Territory> Territories);

/*public sealed class ContinentJsonConverter : JsonConverter<Continent> {
    public override Continent Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options) =>
        new Continent(reader.GetString(), reader.GetUInt32(), reader.Get);

    public override void Write(
            Utf8JsonWriter writer,
            Continent continent,
            JsonSerializerOptions options) =>
        writer.WriteStringValue(continent.Name);

    public override Continent ReadAsPropertyName(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options) =>
        Read(ref reader, typeToConvert, options);

    public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            [DisallowNull] Continent territory,
            JsonSerializerOptions options) =>
        writer.WritePropertyName(territory.Name);
}*/
