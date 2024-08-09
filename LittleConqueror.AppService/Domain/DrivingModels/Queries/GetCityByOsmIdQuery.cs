namespace LittleConqueror.AppService.Domain.DrivingModels.Queries;

public class GetCityByOsmIdQuery
{
    public long OsmId { get; set; }
    public char OsmType { get; set; }
}