namespace LittleConqueror.AppService.Domain.DrivingModels.Queries;

public class GetCityByOsmIdQuery
{
    public int OsmId { get; set; }
    public char OsmType { get; set; }
}