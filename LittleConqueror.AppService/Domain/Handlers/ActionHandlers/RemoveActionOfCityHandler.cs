using LittleConqueror.AppService.Domain.DrivingModels.Commands.ActionsCommands;
using LittleConqueror.AppService.Domain.Handlers.ActionHandlers.Agricole;

namespace LittleConqueror.AppService.Domain.Handlers.ActionHandlers;

public interface IRemoveActionOfCityHandler
{
    Task Handle(RemoveActionOfCityCommand command);
}

public class RemoveActionOfCityHandler(
    IRemoveActionAgricoleOfCityHandler removeActionAgricoleOfCityHandler
    ) : IRemoveActionOfCityHandler
{
    public async Task Handle(RemoveActionOfCityCommand command)
    {
        switch (command)
        {
            case RemoveActionAgricoleOfCityCommand removeActionAgricoleOfCityCommand:
                await removeActionAgricoleOfCityHandler.Handle(removeActionAgricoleOfCityCommand);
                break;
            default:
                throw new NotImplementedException();
        }
    }
}