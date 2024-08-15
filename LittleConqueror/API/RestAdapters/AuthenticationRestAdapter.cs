using AutoMapper;
using LittleConqueror.API.Models.Dtos;
using LittleConqueror.AppService.Domain.DrivingModels.Commands;
using LittleConqueror.AppService.Domain.Handlers.AuthHandlers;
using Microsoft.AspNetCore.Mvc;

namespace LittleConqueror.API.RestAdapters;

[ApiController]
[Route("api/Authenticate")]
public class AuthenticationRestAdapter(
    IAuthenticateUserHandler authenticateHandler,
    IChangePasswordHandler changePasswordHandler,
    IMapper mapper): ControllerBase
{
    [HttpPost]
    public async Task<AuthenticateResponseDto> Authenticate([FromBody] AuthenticateUserCommand model)
        => mapper.Map<AuthenticateResponseDto>(await authenticateHandler.Handle(model));

    [HttpPost("changePassword")]
    public async Task ChangePassword([FromBody] ChangePasswordCommand model)
        => await changePasswordHandler.Handle(model);

}