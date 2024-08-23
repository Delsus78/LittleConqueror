using LittleConqueror.AppService.Domain.DrivingModels.Queries;
using LittleConqueror.AppService.Domain.Models;
using LittleConqueror.AppService.Domain.Services;

namespace LittleConqueror.AppService.Domain.Handlers.AuthHandlers;

public interface IConsumeRegistrationLinkRelatedDataHandler
{
    RegistrationLinkData Handle(GetRegistrationLinkRelatedDataQuery query);
}
public class ConsumeRegistrationLinkRelatedDataHandler(
    ITemporaryCodeService temporaryCodeService) : IConsumeRegistrationLinkRelatedDataHandler
{
    public RegistrationLinkData Handle(GetRegistrationLinkRelatedDataQuery query) 
        => temporaryCodeService.ConsumeLinkRelatedData<RegistrationLinkData>(query.Link) ?? new RegistrationLinkData { Valid = false };
}