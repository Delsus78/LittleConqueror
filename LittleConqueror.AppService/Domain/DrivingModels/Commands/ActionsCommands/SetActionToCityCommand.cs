using LittleConqueror.AppService.Domain.Models;

namespace LittleConqueror.AppService.Domain.DrivingModels.Commands.ActionsCommands;


public class SetActionToCityCommand
{
    public long CityId { get; set; }
    public ActionType ActionType { get; set; }
}