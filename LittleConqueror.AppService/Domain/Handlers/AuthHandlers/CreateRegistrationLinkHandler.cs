using LittleConqueror.AppService.Domain.DrivingModels.Commands;
using LittleConqueror.AppService.Domain.Models;
using LittleConqueror.AppService.Domain.Services;

namespace LittleConqueror.AppService.Domain.Handlers.AuthHandlers;

public interface ICreateRegistrationLinkHandler
{
    string Handle(CreateRegistrationLinkCommand command);
}
public class CreateRegistrationLinkHandler(ITemporaryCodeService temporaryCodeService) : ICreateRegistrationLinkHandler
{
    public string Handle(CreateRegistrationLinkCommand command) 
        => temporaryCodeService.CreateRegistrationLink(new RegistrationLinkData
        {
            Role = command.Role, 
            FirstOsmId = command.FirstOsmId,
            FirstOsmType = command.FirstOsmType
        });
}