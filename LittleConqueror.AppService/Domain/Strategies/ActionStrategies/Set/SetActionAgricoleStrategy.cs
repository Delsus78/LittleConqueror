using LittleConqueror.AppService.Domain.DrivingModels.Commands.ActionsCommands;
using LittleConqueror.AppService.Domain.Handlers.ActionHandlers;
using LittleConqueror.AppService.DrivenPorts;
using LittleConqueror.AppService.Exceptions;

namespace LittleConqueror.AppService.Domain.Strategies.ActionStrategies.Set;

public class SetActionAgricoleStrategy(
    ICityDatabasePort cityDatabase,
    IRemoveActionOfCityHandler removeActionOfCityHandler,
    ITransactionManagerPort transactionManager) : ISetActionStrategy
{
    public async Task<object?> Execute(SetActionToCityCommand input, CancellationToken cancellationToken)
    {
        await transactionManager.BeginTransaction();
        try
        {
            var city = await cityDatabase.GetCityWithAction(input.CityId);
            if (city == null)
                throw new AppException("City not found", 404);

            // Check if city already has an action, if so, call if the action is removable via the handler
            if (city.Action != null)
                await removeActionOfCityHandler.Handle(new RemoveActionOfCityCommand
                {
                    CityId = input.CityId
                });

            var actionAgricole = new Models.Entities.ActionEntities.Agricole
            {
                City = city,
                Id = city.Id,
                StartTime = DateTime.Now
            };

            await cityDatabase.SetAction(city, actionAgricole);
            await transactionManager.CommitTransaction();
        }
        catch (Exception e)
        {
            await transactionManager.RollbackTransaction();
            throw new AppException(e.Message, e is AppException appException ? appException.ErrorCode : 500);
        }
        
        return null;
    }
}