namespace LittleConqueror.AppService.Domain.DrivingModels.Commands;

public class AddCityToATerritoryCommand
{
    public int CityId { get; set; }
    public char CityType { get; set; }
    public int TerritoryId { get; set; }
}