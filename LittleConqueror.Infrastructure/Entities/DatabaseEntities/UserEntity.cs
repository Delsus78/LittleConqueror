namespace LittleConqueror.Persistence.Entities;

public class UserEntity
{
    public int Id { get; set; }
    public required string Name { get; set; }
    
    public int TerritoryId { get; set; }
    public TerritoryEntity? Territory { get; set; }
    
    public int CapitalId { get; set; } 
    public CityEntity? Capital { get; set; }
}