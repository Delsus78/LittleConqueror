using LittleConqueror.AppService.Domain.DrivingModels.Queries;
using LittleConqueror.AppService.Domain.Singletons;

namespace LittleConqueror.AppService.Domain.Handlers;

public interface IGetRegistrationLinkRelatedDataHandler
{
    (bool valid, string role, int firstPlaceId) Handle(GetRegistrationLinkRelatedDataQuery query);
}
public class GetRegistrationLinkRelatedDataHandler(
    IRegistrationLinkService registrationLinkService) : IGetRegistrationLinkRelatedDataHandler
{
    public (bool valid, string role, int firstPlaceId) Handle(GetRegistrationLinkRelatedDataQuery query) 
        => registrationLinkService.GetLinkRelatedData(query.Link);
}