using LittleConqueror.AppService.Domain.DrivingModels.Queries;
using LittleConqueror.AppService.Domain.Models;
using LittleConqueror.AppService.Domain.Services;

namespace LittleConqueror.AppService.Domain.Handlers.AuthHandlers;

public interface IConsumeForgotPasswordLinkRelatedDataHandler
{
    ForgetPasswordLinkData Handle(GetForgotPasswordLinkRelatedDataQuery query);
}
public class ConsumeForgotPasswordLinkRelatedDataHandler(
    ITemporaryCodeService temporaryCodeService) : IConsumeForgotPasswordLinkRelatedDataHandler
{
    public ForgetPasswordLinkData Handle(GetForgotPasswordLinkRelatedDataQuery query) 
        => temporaryCodeService.ConsumeLinkRelatedData<ForgetPasswordLinkData>(query.Link) ?? new ForgetPasswordLinkData { Valid = false };
}