namespace LittleConqueror.API.Models.Dtos;

public class TerritoryDto
{
    public long Id { get; set; }
    
    public long OwnerId { get; set; }
    public IEnumerable<CityDto> Cities { get; set; }
}