using LittleConqueror.AppService.Domain.DrivingModels.Commands.ActionsCommands;
using LittleConqueror.AppService.DrivenPorts;
using LittleConqueror.AppService.Exceptions;
using ActionEntities = LittleConqueror.AppService.Domain.Models.Entities.ActionEntities;

namespace LittleConqueror.AppService.Domain.Strategies.ActionStrategies.Remove;

public class RemoveActionMiniereStrategy(
    IActionDatabasePort actionDatabase) : IRemoveActionStrategy
{
    public async Task<object?> Execute(RemoveActionOfCityCommand input, CancellationToken cancellationToken)
    {
        var city = input.City ?? throw new AppException("ERROR IN REMOVEACTIONMINIERE", 500);
        
        if (city.Action == null)
            throw new AppException("Action not found", 404);
        
        if (city.Action is not ActionEntities.Miniere actionMiniere)
            throw new AppException("Action is not an mine action", 400);
        
        await actionDatabase.DeleteAction(actionMiniere);
        
        return null;
    }
}