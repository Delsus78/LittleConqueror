using LittleConqueror.AppService.Domain.DrivingModels.Commands.ActionsCommands;
using LittleConqueror.AppService.Domain.DrivingModels.Commands.ActionsCommands.Agricole;
using LittleConqueror.AppService.Domain.Handlers.ActionHandlers.Agricole;

namespace LittleConqueror.AppService.Domain.Handlers.ActionHandlers;

public interface ISetActionToCityHandler
{
    Task Handle(SetActionToCityCommand command);
}
public class SetActionToCityHandler(
    ISetActionAgricoleToCityHandler setActionAgricoleToCityHandler): ISetActionToCityHandler
{
    public async Task Handle(SetActionToCityCommand command)
    {
        switch (command)
        {
            case SetActionAgricoleToCityCommand setActionAgricoleToCityCommand:
                await setActionAgricoleToCityHandler.Handle(setActionAgricoleToCityCommand);
                break;
            default:
                throw new NotImplementedException("Implement Action in SetActionToCityHandler");
        }
    }
}