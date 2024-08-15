using AutoMapper;
using LittleConqueror.API.Models.Dtos;
using LittleConqueror.AppService.Domain.DrivingModels.Commands;
using LittleConqueror.AppService.Domain.Handlers.AuthHandlers;
using Microsoft.AspNetCore.Mvc;

namespace LittleConqueror.API.RestAdapters;

[ApiController]
[Route("api/Registration")]
public class RegistrationRestAdapter(
    IRegisterAuthUserHandler registerAuthUserHandler,
    IMapper mapper) : ControllerBase
{
    
    [HttpPost]
    public async Task<AuthUserDto> Register([FromBody] RegisterAuthUserCommand model)
        => mapper.Map<AuthUserDto>(await registerAuthUserHandler.Handle(model));
    
}