using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace LittleConqueror.Persistence.JsonConverters;

public class StringOrNumberToDoubleConverter : JsonConverter<double>
{
    public override double Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        switch (reader.TokenType)
        {
            case JsonTokenType.String when double.TryParse(reader.GetString(), NumberStyles.Any, CultureInfo.InvariantCulture, out var result):
                return result;
            case JsonTokenType.String:
                throw new JsonException($"Unable to convert \"{reader.GetString()}\" to {typeToConvert}.");
            case JsonTokenType.Number:
                return reader.GetDouble();
            default:
                throw new JsonException($"Unexpected token parsing {typeToConvert}. Expected String or Number, got {reader.TokenType}.");
        }
    }

    public override void Write(Utf8JsonWriter writer, double value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString(CultureInfo.InvariantCulture));
    }
}

