using LittleConqueror.AppService.Domain.DrivingModels.Commands.ActionsCommands;
using LittleConqueror.AppService.Domain.Models.Entities;

namespace LittleConqueror.AppService.Domain.Strategies.ActionStrategies;

public interface ISetActionStrategy : IStrategy<(SetActionToCityCommand command, City city), object?>;