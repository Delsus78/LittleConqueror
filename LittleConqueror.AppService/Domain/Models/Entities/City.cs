using LittleConqueror.AppService.Domain.Models.Entities.Base;
using Newtonsoft.Json.Linq;

namespace LittleConqueror.AppService.Domain.Models.Entities;

public class City : Entity
{
    public required char OsmType { get; set; } = 'R';
    public string Name { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    
    public JToken Geojson { get; set; }
    public int Population { get; set; }
    
    // n:1 relationship
    public long? TerritoryId { get; set; }
    public Territory Territory { get; set; }
}
