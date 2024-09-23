using LittleConqueror.AppService.Domain.DrivingModels.Commands.ActionsCommands;
using LittleConqueror.AppService.DrivenPorts;
using LittleConqueror.AppService.Exceptions;
using ActionEntities = LittleConqueror.AppService.Domain.Models.Entities.ActionEntities;

namespace LittleConqueror.AppService.Domain.Strategies.ActionStrategies.Remove;

public class RemoveActionAgricoleStrategy(
    IActionDatabasePort actionDatabase) : IRemoveActionStrategy
{
    public async Task<object?> Execute(RemoveActionStrategyParams input, CancellationToken cancellationToken)
    {
        var city = input.City ?? throw new AppException("ERROR IN REMOVEACTIONAGRICOLE", 500);
        
        if (city.Action == null)
            throw new AppException("Action not found", 404);
        
        if (city.Action is not ActionEntities.Agricole actionAgricole)
            throw new AppException("Action is not an agricole action", 400);
        
        await actionDatabase.DeleteAction(actionAgricole);
        
        return null;
    }
}