using LittleConqueror.AppService.Domain.DrivingModels.Commands.ActionsCommands;
using LittleConqueror.AppService.DrivenPorts;
using LittleConqueror.AppService.Exceptions;
using ActionEntities = LittleConqueror.AppService.Domain.Models.Entities.ActionEntities;

namespace LittleConqueror.AppService.Domain.Handlers.ActionHandlers.Agricole;

public interface IRemoveActionAgricoleOfCityHandler
{
    Task Handle(RemoveActionAgricoleOfCityCommand command);
}
public class RemoveActionAgricoleOfCityHandler(
    ICityDatabasePort cityDatabase,
    IActionAgricoleDatabasePort actionAgricoleDatabase): IRemoveActionAgricoleOfCityHandler
{
    public async Task Handle(RemoveActionAgricoleOfCityCommand command)
    {
        var city = await cityDatabase.GetCityWithAction(command.CityId);
        if (city == null)
            throw new AppException("City not found", 404);
        
        if (city.Action == null)
            throw new AppException("Action not found", 404);
        
        if (city.Action is not ActionEntities.Agricole actionAgricole)
            throw new AppException("Action is not an agricole action", 400);
        
        await actionAgricoleDatabase.DeleteActionAgricole(actionAgricole);
    }
}