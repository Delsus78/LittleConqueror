using LittleConqueror.AppService.Domain.DrivingModels.Commands;
using LittleConqueror.AppService.Domain.Models;
using LittleConqueror.AppService.Domain.Singletons;

namespace LittleConqueror.AppService.Domain.Handlers.AuthHandlers;

public interface ICreateForgetPasswordLinkHandler
{
    string Handle(CreateForgetPasswordLinkCommand command);
}
public class CreateForgetPasswordLinkHandler(ITemporaryCodeService temporaryCodeService) : ICreateForgetPasswordLinkHandler
{
    public string Handle(CreateForgetPasswordLinkCommand command) 
        => temporaryCodeService.CreateForgetPasswordLink(new ForgetPasswordLinkData
        {
            Username = command.Username
        });
}