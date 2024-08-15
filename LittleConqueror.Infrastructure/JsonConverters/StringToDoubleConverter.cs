using System.Globalization;
using Newtonsoft.Json;
using JsonException = Newtonsoft.Json.JsonException;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;

namespace LittleConqueror.Infrastructure.JsonConverters;

public class StringOrNumberToDoubleConverter : JsonConverter<double>
{
    public override void WriteJson(JsonWriter writer, double value, JsonSerializer serializer)
    {
        writer.WriteValue(value);
    }

    public override double ReadJson(JsonReader reader, Type objectType, double existingValue, bool hasExistingValue,
        JsonSerializer serializer)
    {
        return reader.TokenType switch
        {
            JsonToken.String when double.TryParse(reader.Value.ToString(), NumberStyles.Any,
                CultureInfo.InvariantCulture, out var result) => result,
            JsonToken.String => throw new JsonException(
                $"Unable to convert \"{reader.Value.ToString()}\" to {objectType}."),
            JsonToken.Float => reader.Value is double d
                ? d
                : Convert.ToDouble(reader.Value, CultureInfo.InvariantCulture),
            _ => throw new JsonException(
                $"Unexpected token parsing {objectType}. Expected String or Number, got {reader.TokenType}.")
        };
    }
}

