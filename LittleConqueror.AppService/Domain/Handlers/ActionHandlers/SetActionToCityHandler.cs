using LittleConqueror.AppService.Domain.DrivingModels.Commands.ActionsCommands;
using LittleConqueror.AppService.Domain.Strategies;
using LittleConqueror.AppService.Domain.Strategies.ActionStrategies;

namespace LittleConqueror.AppService.Domain.Handlers.ActionHandlers;

public interface ISetActionToCityHandler
{
    Task Handle(SetActionToCityCommand command);
}
public class SetActionToCityHandler(
    IStrategyContext strategyContext): ISetActionToCityHandler
{
    public async Task Handle(SetActionToCityCommand command)
    {
        await strategyContext.ExecuteStrategy<SetActionToCityCommand, object?, ISetActionStrategy>(
            command.ActionType, command, default);
    }
}