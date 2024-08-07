using LittleConqueror.AppService.Domain.DrivingModels.Commands;
using LittleConqueror.AppService.Domain.Models.Entities;
using LittleConqueror.AppService.DrivenPorts;
using LittleConqueror.AppService.Exceptions;

namespace LittleConqueror.AppService.Domain.Handlers.AuthHandlers;

public interface IAuthenticateUserHandler
{
    Task<(AuthUser? authUser, string token)> Handle(AuthenticateUserCommand command);
}
public class AuthenticateUserHandler(
    IAuthUserDatabasePort authUserDatabase,
    IPasswordHasherPort passwordHasher,
    IJwtTokenProviderPort jwtTokenProvider) : IAuthenticateUserHandler
{
    public async Task<(AuthUser? authUser, string token)> Handle(AuthenticateUserCommand command)
    {
        var user = await authUserDatabase.GetAuthUserByUsername(command.Username);
        if (user == null)
            throw new AppException("Username or password is incorrect", 400);
        
        var isValidPassword = passwordHasher.VerifyHashedPassword(command.Password, user.Hash);
        
        if (!isValidPassword)
            throw new AppException("Username or password is incorrect", 400);
        
        // authentication successful so generate jwt token
        var token = await jwtTokenProvider.GenerateJwtToken(user);

        return (user, token);
    }
}