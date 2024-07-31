using System.Globalization;

namespace LittleConqueror.Persistence.JsonConverters;

public static class DoubleExtension
{
    public static string ToURIString(this double val)
    {
        return val.ToString(CultureInfo.InvariantCulture).Replace(',', '.');
    }
}