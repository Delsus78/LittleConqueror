using Newtonsoft.Json.Linq;

namespace LittleConqueror.API.Models.Dtos;

public class CityDto
{
    public long Id { get; set; } 
    public char OsmType { get; set; } 
    public string Name { get; set; }
    public int? population { get; set; } 
    public double? Latitude { get; set; } 
    public double? Longitude { get; set; }
    public JToken? Geojson { get; set; }
}