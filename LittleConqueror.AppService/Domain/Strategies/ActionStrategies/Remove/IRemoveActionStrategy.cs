using LittleConqueror.AppService.Domain.DrivingModels.Commands.ActionsCommands;

namespace LittleConqueror.AppService.Domain.Strategies.ActionStrategies.Remove;

public interface IRemoveActionStrategy : IStrategy<RemoveActionOfCityCommand, object?>;