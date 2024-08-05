namespace LittleConqueror.Models.Dtos;

public class TerritoryDto
{
    public int Id { get; set; }
    
    public int OwnerId { get; set; }
    public IEnumerable<CityDto> Cities { get; set; }
}