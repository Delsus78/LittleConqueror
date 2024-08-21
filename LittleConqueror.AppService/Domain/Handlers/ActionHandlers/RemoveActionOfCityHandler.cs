using LittleConqueror.AppService.Domain.DrivingModels.Commands.ActionsCommands;
using LittleConqueror.AppService.Domain.Strategies;
using LittleConqueror.AppService.Domain.Strategies.ActionStrategies.Remove;

namespace LittleConqueror.AppService.Domain.Handlers.ActionHandlers;

public interface IRemoveActionOfCityHandler
{
    Task Handle(RemoveActionOfCityCommand command);
}

public class RemoveActionOfCityHandler(
    IStrategyContext strategyContext
    ) : IRemoveActionOfCityHandler
{
    public async Task Handle(RemoveActionOfCityCommand command)
    {
        await strategyContext.ExecuteStrategy<RemoveActionOfCityCommand, object?, IRemoveActionStrategy>(
            command.ActionType, command, default);
    }
}