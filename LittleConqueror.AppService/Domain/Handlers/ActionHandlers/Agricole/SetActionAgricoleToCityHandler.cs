using LittleConqueror.AppService.Domain.DrivingModels.Commands.ActionsCommands;
using LittleConqueror.AppService.Domain.DrivingModels.Commands.ActionsCommands.Agricole;
using LittleConqueror.AppService.DrivenPorts;
using LittleConqueror.AppService.Exceptions;

namespace LittleConqueror.AppService.Domain.Handlers.ActionHandlers.Agricole;

public interface ISetActionAgricoleToCityHandler
{
    Task Handle(SetActionAgricoleToCityCommand command);
}
public class SetActionAgricoleToCityHandler(
    ICityDatabasePort cityDatabase,
    IRemoveActionOfCityHandler removeActionOfCityHandler,
    ITransactionManagerPort transactionManager): ISetActionAgricoleToCityHandler
{
    public async Task Handle(SetActionAgricoleToCityCommand command)
    {
        await transactionManager.BeginTransaction();
        try
        {
            var city = await cityDatabase.GetCityWithAction(command.CityId);
            if (city == null)
                throw new AppException("City not found", 404);

            // Check if city already has an action, if so, call if the action is removable via the handler
            if (city.Action != null)
                await removeActionOfCityHandler.Handle(new RemoveActionOfCityCommand
                {
                    CityId = command.CityId
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
    }
}