using System.Globalization;
using Newtonsoft.Json;
using JsonException = Newtonsoft.Json.JsonException;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;

namespace LittleConqueror.Infrastructure.JsonConverters;

public class StringToIntConverter : JsonConverter<int>
{
    public override void WriteJson(JsonWriter writer, int value, JsonSerializer serializer)
    {
        writer.WriteValue(value);
    }

    public override int ReadJson(JsonReader reader, Type objectType, int existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        switch (reader.TokenType)
        {
            case JsonToken.String when int.TryParse(reader.Value.ToString(), out var result):
                return result;
            case JsonToken.String:
                throw new JsonException($"Unable to convert \"{reader.Value}\" to {objectType}.");
            case JsonToken.Integer:
                return reader.Value is int i ? i : Convert.ToInt32(reader.Value, CultureInfo.InvariantCulture);
            default:
                throw new JsonException($"Unexpected token parsing {objectType}. Expected String or Number, got {reader.TokenType}.");
        }
    }
}