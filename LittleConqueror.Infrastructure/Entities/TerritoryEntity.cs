namespace LittleConqueror.Persistence.Entities;

public class TerritoryEntity
{
    public int Id { get; set; }
    
    public List<CityEntity> Cities { get; set; }
    
    public int UserId { get; set; }
    public UserEntity User { get; set; }
}