using LittleConqueror.AppService.Domain.DrivingModels.Commands;
using LittleConqueror.AppService.Domain.Models;
using LittleConqueror.AppService.DrivenPorts;
using LittleConqueror.AppService.Exceptions;

namespace LittleConqueror.AppService.Domain.Handlers;

public interface ICreateUserHandler
{
    public Task<User> Handle(CreateUserCommand command);
}
public class CreateUserHandler(
    IUserDatabasePort userDatabase, 
    ITerritoryDatabasePort territoryDatabase) : ICreateUserHandler
{

    public async Task<User> Handle(CreateUserCommand command)
    {
        var user = new User { Name = command.Name };
        user = await userDatabase.CreateUser(user);
        
        if (user == null)
            throw new AppException("User creation failed", 500);

        var territory = new Territory { Owner = user };
        var resultedTerritory = await territoryDatabase.CreateTerritory(territory);
        
        if (resultedTerritory == null)
            throw new AppException("Territory creation failed", 500);

        user.Territory = resultedTerritory;
        
        return user;
    }
}