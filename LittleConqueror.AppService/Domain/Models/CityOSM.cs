using System.Text.Json.Serialization;
using LittleConqueror.AppService.Domain.Models.Entities;

namespace LittleConqueror.AppService.Domain.Models;

public record Extratags(int? Population);

public record CityOSM(
    [property: JsonPropertyName("osm_id")] int OsmId,
    [property: JsonPropertyName("osm_type")] char OsmIdType,
    double Lat,
    double Lon,
    double? Importance,
    string Name,
    Extratags? Extratags,
    Geojson? Geojson);

public record CityOSMByDetails(
    [property: JsonPropertyName("osm_id")] int OsmId,
    [property: JsonPropertyName("osm_type")]
    char OsmIdType,
    Names Names,
    Extratags? Extratags,
    Centroid? Centroid,
    Geojson? Geometry);
    
public record Names(string Name);
public record Centroid(string type, List<double> coordinates);