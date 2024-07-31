using System.Text.Json.Serialization;

namespace LittleConqueror.AppService.Domain.Models;

public record Address(string? City, string? State, string? Country);
public record Geojson(string Type, List<List<List<double>>> Coordinates);
public record Extratags(int? Population);

public record OSMCityResponse(
    [property: JsonPropertyName("place_id")] int PlaceId, double Lat, double Lon, double? Importance, string Name,
    [property: JsonPropertyName("display_name")] string? DisplayName, Address? Address, List<double> Boundingbox, 
    Extratags? Extratags, Geojson? Geojson);