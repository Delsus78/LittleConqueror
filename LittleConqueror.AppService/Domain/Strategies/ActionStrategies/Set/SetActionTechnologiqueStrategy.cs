using LittleConqueror.AppService.Domain.DrivingModels.Commands.ActionsCommands;
using LittleConqueror.AppService.Domain.Handlers.ActionHandlers;
using LittleConqueror.AppService.Domain.Models;
using LittleConqueror.AppService.Domain.Models.Entities;
using LittleConqueror.AppService.DrivenPorts;
using LittleConqueror.AppService.Exceptions;

namespace LittleConqueror.AppService.Domain.Strategies.ActionStrategies.Set;

public class SetActionTechnologiqueStrategy(
    IActionDatabasePort actionDatabase,
    IRemoveActionOfCityHandler removeActionOfCityHandler,
    ITransactionManagerPort transactionManager) : ISetActionStrategy
{
    public async Task<object?> Execute((SetActionToCityCommand command, City city) input, CancellationToken cancellationToken)
    {
        await transactionManager.BeginTransaction();
        try
        {
            var city = input.city ?? throw new AppException("ERROR IN SETACTIONTECHNOLOGIQUE", 500);

            // Check if city already has an action, if so, call if the action is removable via the handler
            if (city.Action != null)
                await removeActionOfCityHandler.Handle(new RemoveActionOfCityCommand
                {
                    CityId = city.Id,
                    ActualActionType = city.Action.GetActionType()
                });

            var actionTechnologique = new Models.Entities.ActionEntities.Technologique
            {
                Id = city.Id,
                StartTime = DateTime.Now,
                TechResearchCategory = input.command.TechResearchCategory
            };

            await actionDatabase.AddAction(actionTechnologique);
            await transactionManager.CommitTransaction();
            
            city.Action = actionTechnologique;
            actionTechnologique.City = city;
        }
        catch (Exception e)
        {
            await transactionManager.RollbackTransaction();
            throw new AppException(e.Message, e is AppException appException ? appException.ErrorCode : 500);
        }
        
        return null;
    }
}