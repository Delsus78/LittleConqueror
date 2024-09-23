using LittleConqueror.AppService.Domain.DrivingModels.Commands.ActionsCommands;
using LittleConqueror.AppService.Domain.Handlers.ActionHandlers;
using LittleConqueror.AppService.Domain.Models;
using LittleConqueror.AppService.Domain.Models.Entities;
using LittleConqueror.AppService.DrivenPorts;
using LittleConqueror.AppService.Exceptions;

namespace LittleConqueror.AppService.Domain.Strategies.ActionStrategies.Set;

public class SetActionMiniereStrategy(
    IActionDatabasePort actionDatabase,
    IRemoveActionOfCityHandler removeActionOfCityHandler,
    ITransactionManagerPort transactionManager) : ISetActionStrategy
{
    public async Task<object?> Execute(SetActionStrategyParams input, CancellationToken cancellationToken)
    {
        await transactionManager.BeginTransaction();
        try
        {
            var city = input.city ?? throw new AppException("ERROR IN SETACTIONMINIERE", 500);

            // Check if city already has an action, if so, call if the action is removable via the handler
            if (city.Action != null)
            {
                await removeActionOfCityHandler.Handle(new RemoveActionOfCityCommand
                {
                    CityId = city.Id,
                    ActualActionType = city.Action.GetActionType()
                });
            }

            var actionMiniere = new Models.Entities.ActionEntities.Miniere
            {
                Id = city.Id,
                StartTime = DateTime.Now,
                ResourceType = input.ResourceType ?? throw new AppException("ResourceType is required", 400)
            };

            await actionDatabase.AddAction(actionMiniere);
            await transactionManager.CommitTransaction();
            
            city.Action = actionMiniere;
            actionMiniere.City = city;
        }
        catch (Exception e)
        {
            await transactionManager.RollbackTransaction();
            throw new AppException(e.Message, e is AppException appException ? appException.ErrorCode : 500);
        }
        
        return null;
    }
}