using LittleConqueror.AppService.Domain.Models;

namespace LittleConqueror.AppService.Domain.DrivingModels.Commands.ActionsCommands;

public class RemoveActionOfCityCommand
{
    public ActionType ActionType { get; set; }
    public long CityId { get; set; }
}