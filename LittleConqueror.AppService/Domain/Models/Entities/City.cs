using LittleConqueror.AppService.Domain.Models.Entities.Base;

namespace LittleConqueror.AppService.Domain.Models.Entities;

public class City : Entity
{
    public required char OsmType { get; set; } = 'R';
    public string Name { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    
    public Geojson Geojson { get; set; }
    public int Population { get; set; }
    
    // n:1 relationship
    public int? TerritoryId { get; set; }
    public Territory Territory { get; set; }
}

public class Geojson 
{
    public string Type { get; set; } 
    public List<List<List<double>>> Coordinates { get; set; }

    protected bool Equals(Geojson other)
    {
        return Type == other.Type && Coordinates.Equals(other.Coordinates);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((Geojson)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Type, Coordinates);
    }
}
