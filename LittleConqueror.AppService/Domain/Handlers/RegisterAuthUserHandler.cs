using LittleConqueror.AppService.Domain.DrivingModels.Commands;
using LittleConqueror.AppService.Domain.DrivingModels.Queries;
using LittleConqueror.AppService.Domain.Models;
using LittleConqueror.AppService.DrivenPorts;
using LittleConqueror.AppService.Exceptions;

namespace LittleConqueror.AppService.Domain.Handlers;

public interface IRegisterAuthUserHandler
{
    Task<AuthUser> Handle(RegisterAuthUserCommand command);
}
public class RegisterAuthUserHandler(
    IPasswordHasherPort passwordHasherPort,
    IGetRegistrationLinkRelatedDataHandler getRegistrationLinkRelatedDataHandler,
    IAuthUserDatabasePort authUserDatabase,
    ICreateUserHandler createUserHandler) : IRegisterAuthUserHandler
{
    public async Task<AuthUser> Handle(RegisterAuthUserCommand command)
    {
        // get registration link data
        var registrationLinkData = 
            getRegistrationLinkRelatedDataHandler.Handle(new GetRegistrationLinkRelatedDataQuery{Link = command.ValidRegistrationLink});
        
        if (!registrationLinkData.valid)
            throw new AppException("Invalid registration link", 400);
        
        var authUser = new AuthUser{
            Username = command.Username,
            Hash = passwordHasherPort.EnhancedHashPassword(command.Password), 
            Role = registrationLinkData.role
        };
        
        // create user entity
        var user = await createUserHandler.Handle(new CreateUserCommand
        {
            Name = command.Username
        });
        
        authUser.User = user;
        
        var entity = await authUserDatabase.AddAsync(authUser);
        
        return entity;
    }
}