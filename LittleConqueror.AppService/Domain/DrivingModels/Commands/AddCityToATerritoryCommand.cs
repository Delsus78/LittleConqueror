namespace LittleConqueror.AppService.Domain.DrivingModels.Commands;

public class AddCityToATerritoryCommand
{
    public long CityId { get; set; }
    public char CityType { get; set; }
    public long TerritoryId { get; set; }
}