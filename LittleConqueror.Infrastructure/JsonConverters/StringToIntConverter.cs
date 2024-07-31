using System.Text.Json;
using System.Text.Json.Serialization;

namespace LittleConqueror.Persistence.JsonConverters;

public class StringToIntConverter : JsonConverter<int>
{
    public override int Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        switch (reader.TokenType)
        {
            case JsonTokenType.String when int.TryParse(reader.GetString(), out var result):
                return result;
            case JsonTokenType.String:
                throw new JsonException($"Unable to convert \"{reader.GetString()}\" to {typeToConvert}.");
            case JsonTokenType.Number:
                return reader.GetInt32();
            default:
                throw new JsonException($"Unexpected token parsing {typeToConvert}. Expected String or Number, got {reader.TokenType}.");
        }
    }

    public override void Write(Utf8JsonWriter writer, int value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}