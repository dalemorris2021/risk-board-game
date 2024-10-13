using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Risk;

public record Territory(string Name);

public sealed class TerritoryJsonConverter : JsonConverter<Territory> {
    public override Territory Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options) =>
        new Territory(reader.GetString());

    public override void Write(
            Utf8JsonWriter writer,
            Territory territory,
            JsonSerializerOptions options) =>
        writer.WriteStringValue(territory.Name);

    public override Territory ReadAsPropertyName(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options) =>
        Read(ref reader, typeToConvert, options);

    public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            [DisallowNull] Territory territory,
            JsonSerializerOptions options) =>
        writer.WritePropertyName(territory.Name);
}
