namespace LittleConqueror.Infrastructure.Entities.DatabaseEntities;

public class TerritoryEntity
{
    public int Id { get; init; }
    public List<CityEntity> Cities { get; init; } = new();
    
    public int OwnerId { get; init; }
    public UserEntity Owner { get; init; }
}