namespace LittleConqueror.AppService.DomainEntities;

public class City
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string Country { get; set; }
    public Geojson Geojson { get; set; }
    public int Population { get; set; }
    public Territory? Territory { get; set; }
}

public record Geojson
{
    public string Type { get; set; } 
    public List<List<List<double>>> Coordinates { get; set; }
}
