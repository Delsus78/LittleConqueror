using LittleConqueror.AppService.Domain.DrivingModels.Commands.ActionsCommands;
using LittleConqueror.AppService.Domain.Models.Entities;
using LittleConqueror.AppService.Domain.Services;
using LittleConqueror.AppService.Domain.Strategies;
using LittleConqueror.AppService.Domain.Strategies.ActionStrategies;
using LittleConqueror.AppService.DrivenPorts;
using LittleConqueror.AppService.Exceptions;

namespace LittleConqueror.AppService.Domain.Handlers.ActionHandlers;

public interface ISetActionToCityHandler
{
    Task<City> Handle(SetActionToCityCommand command);
}
public class SetActionToCityHandler(
    IStrategyContext strategyContext,
    ICityDatabasePort cityDatabase,
    IUserContext userContext): ISetActionToCityHandler
{
    public async Task<City> Handle(SetActionToCityCommand command)
    {
        var city = await cityDatabase.GetCityWithActionAndTerritoryOwnerId(command.CityId);
        if (city == null)
            throw new AppException("City not found", 404);
            
        if (city.Territory.OwnerId != userContext.UserId)
            throw new AppException("You are not the owner of this territory", 403);
        
        await strategyContext.ExecuteStrategy<(SetActionToCityCommand command, City city), object?, ISetActionStrategy>(
            command.ActionType, (command, city), default);

        return city;
    }
}