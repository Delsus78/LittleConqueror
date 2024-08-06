using LittleConqueror.AppService.Domain.DrivingModels.Queries;
using LittleConqueror.AppService.Domain.Singletons;

namespace LittleConqueror.AppService.Domain.Handlers.AuthHandlers;

public interface IGetRegistrationLinkRelatedDataHandler
{
    (bool valid, string role, int firstOsmId) Handle(GetRegistrationLinkRelatedDataQuery query);
}
public class GetRegistrationLinkRelatedDataHandler(
    IRegistrationLinkService registrationLinkService) : IGetRegistrationLinkRelatedDataHandler
{
    public (bool valid, string role, int firstOsmId) Handle(GetRegistrationLinkRelatedDataQuery query) 
        => registrationLinkService.GetLinkRelatedData(query.Link);
}