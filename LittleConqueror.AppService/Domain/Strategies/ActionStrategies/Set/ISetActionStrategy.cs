using LittleConqueror.AppService.Domain.DrivingModels.Commands.ActionsCommands;

namespace LittleConqueror.AppService.Domain.Strategies.ActionStrategies;

public interface ISetActionStrategy : IStrategy<SetActionToCityCommand, object?>;