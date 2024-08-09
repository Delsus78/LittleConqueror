namespace LittleConqueror.Infrastructure;

public class OSMSettings
{
    public SortedSet<int> AuthorizedZoom { get; } = new();
    public List<string> UnauthorizedAddressTypes { get; } = new();
}