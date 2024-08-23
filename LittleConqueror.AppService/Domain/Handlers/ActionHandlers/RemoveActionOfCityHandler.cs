using LittleConqueror.AppService.Domain.DrivingModels.Commands.ActionsCommands;
using LittleConqueror.AppService.Domain.Services;
using LittleConqueror.AppService.Domain.Strategies;
using LittleConqueror.AppService.Domain.Strategies.ActionStrategies.Remove;
using LittleConqueror.AppService.DrivenPorts;
using LittleConqueror.AppService.Exceptions;

namespace LittleConqueror.AppService.Domain.Handlers.ActionHandlers;

public interface IRemoveActionOfCityHandler
{
    Task Handle(RemoveActionOfCityCommand command);
}

public class RemoveActionOfCityHandler(
    IStrategyContext strategyContext,
    ICityDatabasePort cityDatabase,
    IUserContext userContext) : IRemoveActionOfCityHandler
{
    public async Task Handle(RemoveActionOfCityCommand command)
    {
        var city = await cityDatabase.GetCityWithActionAndTerritoryOwnerId(command.CityId);
        if (city == null)
            throw new AppException("City not found", 404);
            
        if (city.Territory.OwnerId != userContext.UserId)
            throw new AppException("You are not the owner of this territory", 403);
        
        command.City = city;
        
        await strategyContext.ExecuteStrategy<RemoveActionOfCityCommand, object?, IRemoveActionStrategy>(
            command.ActualActionType, command, default);
    }
}