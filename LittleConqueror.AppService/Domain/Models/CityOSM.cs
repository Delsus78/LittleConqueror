using System.Text.Json.Serialization;

namespace LittleConqueror.AppService.Domain.Models;

public record GeojsonOSM(string Type, List<List<List<double>>> Coordinates);
public record Extratags(int? Population);

public record CityOSM(
    [property: JsonPropertyName("osm_id")] int OsmId,
    [property: JsonPropertyName("osm_type")] string OsmIdType,
    double Lat,
    double Lon,
    double? Importance,
    string Name,
    Extratags? Extratags,
    GeojsonOSM? Geojson);

public record CityOSMByDetails(
    [property: JsonPropertyName("osm_id")] int OsmId,
    [property: JsonPropertyName("osm_type")]
    string OsmIdType,
    Names Names,
    Extratags? Extratags,
    Centroid? Centroid,
    GeojsonOSM? Geometry);
    
public record Names(string Name);
public record Centroid(string type, List<double> coordinates);