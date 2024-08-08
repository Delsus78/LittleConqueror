using LittleConqueror.AppService.Domain.DrivingModels.Commands;
using LittleConqueror.AppService.Domain.Handlers.CityHandlers;
using LittleConqueror.AppService.Domain.Models.Entities;
using LittleConqueror.AppService.DrivenPorts;
using LittleConqueror.AppService.Exceptions;

namespace LittleConqueror.AppService.Domain.Handlers.UserHandlers;

public interface ICreateUserHandler
{
    public Task<User> Handle(CreateUserCommand command);
}
public class CreateUserHandler(
    IUserDatabasePort userDatabase, 
    ITerritoryDatabasePort territoryDatabase,
    IAddCityToATerritoryHandler cityToATerritoryHandler,
    ITransactionManagerPort transactionManager) : ICreateUserHandler
{

    public async Task<User> Handle(CreateUserCommand command)
    {
        await transactionManager.BeginTransaction();
        try
        {
            var user = new User { Name = command.Name };
            user = await userDatabase.CreateUser(user);

            if (user == null)
                throw new AppException("User creation failed", 500);
            
            var resultedTerritory = await territoryDatabase.CreateTerritory(new Territory
            {
                OwnerId = user.Id
            });
            if (resultedTerritory == null)
                throw new AppException("Territory creation failed", 500);

            await cityToATerritoryHandler.Handle(new AddCityToATerritoryCommand
            {
                CityId = command.FirstOsmId,
                CityType = command.FirstOsmType,
                TerritoryId = resultedTerritory.Id
            });
            
            await transactionManager.CommitTransaction();

            return user;
        }
        catch (Exception e)
        {
            await transactionManager.RollbackTransaction();
            throw new AppException("User creation failed : " + e.Message + "\n" + e.InnerException, 500);
        }
    }
}