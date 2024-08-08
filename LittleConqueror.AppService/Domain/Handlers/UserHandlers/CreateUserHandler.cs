using LittleConqueror.AppService.Domain.DrivingModels.Commands;
using LittleConqueror.AppService.Domain.Handlers.CityHandlers;
using LittleConqueror.AppService.Domain.Handlers.ResourcesHandlers;
using LittleConqueror.AppService.Domain.Handlers.TerritoryHandlers;
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
    ICreateTerritoryHandler createTerritoryHandler,
    IAddCityToATerritoryHandler cityToATerritoryHandler,
    ICreateResourcesForUserHandler createResourcesForUserHandler,
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

            
            // TERRITORY INITIALIZATION
            var resultedTerritory = await createTerritoryHandler.Handle(new CreateTerritoryCommand
            {
                OwnerId = user.Id
            });

            await cityToATerritoryHandler.Handle(new AddCityToATerritoryCommand
            {
                CityId = command.FirstOsmId,
                CityType = command.FirstOsmType,
                TerritoryId = resultedTerritory.Id
            });
            
            // RESOURCES INITIALIZATION
            await createResourcesForUserHandler.Handle(new CreateResourcesForUserCommand
            {
                UserId = user.Id
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