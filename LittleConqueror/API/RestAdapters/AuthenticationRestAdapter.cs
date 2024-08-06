using AutoMapper;
using LittleConqueror.API.Models.Dtos;
using LittleConqueror.AppService.Domain.DrivingModels.Commands;
using LittleConqueror.AppService.Domain.Handlers.AuthHandlers;
using LittleConqueror.Infrastructure.JwtAdapters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LittleConqueror.API.RestAdapters;

[ApiController]
[Route("api/Authenticate")]
public class AuthenticationRestAdapter(
    IAuthenticateUserHandler authenticateHandler,
    ITokenManagerService tokenBlacklist,
    IMapper mapper): ControllerBase
{
    [HttpPost]
    public async Task<AuthenticateResponseDto> Authenticate([FromBody] AuthenticateUserCommand model)
        => mapper.Map<AuthenticateResponseDto>(await authenticateHandler.Handle(model));

    [Authorize(Roles = "Admin")]
    [HttpPut("desactivateToken/{userId}")]
    public ActionResult DesactivateTokenJTI(int userId)
    {
        tokenBlacklist.DesactivateTokenJTI(userId);
        return Ok();
    }
}