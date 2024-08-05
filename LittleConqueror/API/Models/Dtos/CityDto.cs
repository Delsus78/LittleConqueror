namespace LittleConqueror.API.Models.Dtos;

public record CityRequestDto(float Latitude, float Longitude);

public record GeojsonDto(string Type, List<List<List<double>>> Coordinates);

public record CityDto(int Id, string Name, int? population, double? Latitude, double? Longitude, string? Country, GeojsonDto? Geojson);