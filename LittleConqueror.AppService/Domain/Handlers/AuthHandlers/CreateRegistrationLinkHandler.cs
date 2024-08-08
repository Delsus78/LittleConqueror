using LittleConqueror.AppService.Domain.DrivingModels.Commands;
using LittleConqueror.AppService.Domain.Models;
using LittleConqueror.AppService.Domain.Singletons;

namespace LittleConqueror.AppService.Domain.Handlers.AuthHandlers;

public interface ICreateRegistrationLinkHandler
{
    string Handle(CreateRegistrationLinkCommand command);
}
public class CreateRegistrationLinkHandler(IRegistrationLinkService registrationLinkService) : ICreateRegistrationLinkHandler
{
    public string Handle(CreateRegistrationLinkCommand command) 
        => registrationLinkService.CreateRegistrationLink(new RegistrationLinkData
        {
            Role = command.Role, 
            FirstOsmId = command.FirstOsmId,
            FirstOsmType = command.FirstOsmType
        });
}