namespace LittleConqueror.API.Models.Dtos;

public record GeojsonDto(string Type, List<List<List<double>>> Coordinates);

public class CityDto
{
    public int Id { get; set; } 
    public char OsmType { get; set; } 
    public string Name { get; set; }
    public int? population { get; set; } 
    public double? Latitude { get; set; } 
    public double? Longitude { get; set; }
    public string? Country { get; set; } 
    public GeojsonDto? Geojson { get; set; }
}