using LittleConqueror.AppService.Domain.DrivingModels.Commands.ActionsCommands;
using LittleConqueror.AppService.Domain.Handlers.ActionHandlers;
using LittleConqueror.AppService.Domain.Models;
using LittleConqueror.AppService.DrivenPorts;
using LittleConqueror.AppService.Exceptions;

namespace LittleConqueror.AppService.Domain.Strategies.ActionStrategies.Set;

public class SetActionAgricoleStrategy(
    IActionDatabasePort actionDatabase,
    IRemoveActionOfCityHandler removeActionOfCityHandler,
    ITransactionManagerPort transactionManager) : ISetActionStrategy
{
    public async Task<object?> Execute(SetActionToCityCommand input, CancellationToken cancellationToken)
    {
        await transactionManager.BeginTransaction();
        try
        {
            var city = input.City ?? throw new AppException("ERROR IN SETACTIONAGRICOLE", 500);

            // Check if city already has an action, if so, call if the action is removable via the handler
            if (city.Action != null)
                await removeActionOfCityHandler.Handle(new RemoveActionOfCityCommand
                {
                    CityId = city.Id,
                    ActualActionType = city.Action.GetActionType()
                });

            var actionAgricole = new Models.Entities.ActionEntities.Agricole
            {
                Id = city.Id,
                StartTime = DateTime.Now
            };

            await actionDatabase.AddAction(actionAgricole);
            await transactionManager.CommitTransaction();
            
            city.Action = actionAgricole;
        }
        catch (Exception e)
        {
            await transactionManager.RollbackTransaction();
            throw new AppException(e.Message, e is AppException appException ? appException.ErrorCode : 500);
        }
        
        return null;
    }
}