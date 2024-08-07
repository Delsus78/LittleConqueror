using LittleConqueror.AppService.Domain.DrivingModels.Queries;
using LittleConqueror.AppService.Domain.Models;
using LittleConqueror.AppService.Domain.Singletons;

namespace LittleConqueror.AppService.Domain.Handlers.AuthHandlers;

public interface IGetRegistrationLinkRelatedDataHandler
{
    RegistrationLinkData Handle(GetRegistrationLinkRelatedDataQuery query);
}
public class ConsumeRegistrationLinkRelatedDataHandler(
    IRegistrationLinkService registrationLinkService) : IGetRegistrationLinkRelatedDataHandler
{
    public RegistrationLinkData Handle(GetRegistrationLinkRelatedDataQuery query) 
        => registrationLinkService.ConsumeLinkRelatedData(query.Link);
}