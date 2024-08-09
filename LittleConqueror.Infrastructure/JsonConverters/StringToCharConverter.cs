using Newtonsoft.Json;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;

namespace LittleConqueror.Infrastructure.JsonConverters;

public class StringToCharConverter : JsonConverter<char>
{
    public override void WriteJson(JsonWriter writer, char value, JsonSerializer serializer)
    {
        writer.WriteValue(value);
    }

    public override char ReadJson(JsonReader reader, Type objectType, char existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        var value = reader.Value.ToString();
        return value[0];
    }
}