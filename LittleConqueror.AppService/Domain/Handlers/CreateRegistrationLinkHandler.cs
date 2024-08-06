using LittleConqueror.AppService.Domain.DrivingModels.Commands;
using LittleConqueror.AppService.Domain.Singletons;

namespace LittleConqueror.AppService.Domain.Handlers;

public interface ICreateRegistrationLinkHandler
{
    string Handle(CreateRegistrationLinkCommand command);
}
public class CreateRegistrationLinkHandler(IRegistrationLinkService registrationLinkService) : ICreateRegistrationLinkHandler
{
    public string Handle(CreateRegistrationLinkCommand command) 
        => registrationLinkService.CreateRegistrationLink(command.Role, command.FirstCityId);
}