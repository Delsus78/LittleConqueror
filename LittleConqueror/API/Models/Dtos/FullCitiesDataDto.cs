using LittleConqueror.API.Models.Dtos.ActionsDtos;
using Newtonsoft.Json.Linq;

namespace LittleConqueror.API.Models.Dtos;

public class FullCitiesDataDto
{
    public string Type { get; set; } = "FeatureCollection";
    public List<FeatureDto> Features { get; set; }
}

public class FeatureDto
{
    public string Type { get; set; } = "Feature";
    public string Id { get; set; }
    public CityPropertiesDto Properties { get; set; }
    public JToken? Geometry { get; set; }
}

public class CityPropertiesDto
{
    public char OsmType { get; set; } 
    public string Name { get; set; }
    public int? population { get; set; } 
    public double? Latitude { get; set; } 
    public double? Longitude { get; set; }
    public ActionDto? Action { get; set; }
}