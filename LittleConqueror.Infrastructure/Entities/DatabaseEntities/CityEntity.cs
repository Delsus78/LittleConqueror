namespace LittleConqueror.Persistence.Entities;

public class CityEntity
{
    public int Id { get; init; }
    public string Name { get; init; }
    public int Population { get; init; }
    
    public int TerritoryId { get; init; }
    public TerritoryEntity? Territory { get; init; }
}