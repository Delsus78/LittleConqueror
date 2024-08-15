using LittleConqueror.AppService.Domain.DrivingModels.Commands;
using LittleConqueror.AppService.Domain.DrivingModels.Queries;
using LittleConqueror.AppService.DrivenPorts;
using LittleConqueror.AppService.Exceptions;

namespace LittleConqueror.AppService.Domain.Handlers.AuthHandlers;

public interface IChangePasswordHandler
{
    Task Handle(ChangePasswordCommand command);
}
public class ChangePasswordHandler(
    IPasswordHasherPort passwordHasherPort,
    IConsumeForgotPasswordLinkRelatedDataHandler getForgotPasswordLinkRelatedDataHandler,
    IAuthUserDatabasePort authUserDatabase) : IChangePasswordHandler
{
    public async Task Handle(ChangePasswordCommand command)
    {
        var linkData = getForgotPasswordLinkRelatedDataHandler.Handle(new GetForgotPasswordLinkRelatedDataQuery
        {
            Link = command.ValidForgetPasswordLink
        });
        
        var authUser = await authUserDatabase.GetAuthUserByUsername(linkData.Username);
        if (authUser == null)
            throw new AppException("User not found", 404);
        
        authUser.Hash = passwordHasherPort.EnhancedHashPassword(command.Password);
        await authUserDatabase.UpdateAsync(authUser);
    }
}