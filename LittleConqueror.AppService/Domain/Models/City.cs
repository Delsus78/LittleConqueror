namespace LittleConqueror.AppService.Domain.Models;

public class City
{
    public int Id { get; set; }
    public char OsmType { get; set; }
    public string Name { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public Geojson Geojson { get; set; }
    public int Population { get; set; }
}

public record Geojson
{
    public string Type { get; set; } 
    public List<List<List<double>>> Coordinates { get; set; }
}
