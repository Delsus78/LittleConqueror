using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace LittleConqueror.AppService.Domain.Models;

public record Extratags(
    int? Population, 
    [property: JsonProperty("linked_place")] string? LinkedPlace = null);

public record CityOSM(
    [property: JsonProperty("osm_id")] long OsmId,
    [property: JsonProperty("osm_type")] char OsmIdType,
    double Lat,
    double Lon,
    string Name,
    Extratags? Extratags,
    string? AddressType,
    JToken? Geojson);

public record CityOSMByDetails(
    [property: JsonProperty("osm_id")] long OsmId,
    [property: JsonProperty("osm_type")]
    char OsmIdType,
    Names Names,
    Extratags? Extratags,
    string? AddressType,
    Centroid? Centroid,
    JToken? Geometry);
    
public record Names(string Name);
public record Centroid(string type, List<double> coordinates);