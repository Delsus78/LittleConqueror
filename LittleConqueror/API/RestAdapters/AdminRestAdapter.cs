using LittleConqueror.AppService.Domain.DrivingModels.Commands;
using LittleConqueror.AppService.Domain.Handlers.AuthHandlers;
using LittleConqueror.Infrastructure.JwtAdapters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LittleConqueror.API.RestAdapters;

[ApiController]
[Authorize(Roles = "Admin")]
[Route("api/Authenticate")]
public class AdminRestAdapter(
    ICreateRegistrationLinkHandler createRegistrationLinkHandler,
    ICreateForgetPasswordLinkHandler createForgetPasswordLinkHandler,
    ITokenManagerService tokenBlacklist) : ControllerBase
{
    
    [HttpPut("desactivateToken/{userId}")]
    public ActionResult DesactivateTokenJTI(long userId)
    {
        tokenBlacklist.DesactivateTokenJTI(userId);
        return Ok();
    }
    
    [HttpPut("createForgetPasswordLink")]
    public string CreateForgetPasswordLink([FromBody] CreateForgetPasswordLinkCommand model)
        => createForgetPasswordLinkHandler.Handle(model);
    

    [HttpPut("createLink")]
    public string CreateLink(CreateRegistrationLinkCommand model)
        => createRegistrationLinkHandler.Handle(model);
}