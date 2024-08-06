namespace LittleConqueror.Infrastructure.Entities.DatabaseEntities;

public class CityEntity
{
    public int Id { get; init; }
    public char OsmType { get; init; }
    public string Name { get; init; }
    public int Population { get; init; }
}