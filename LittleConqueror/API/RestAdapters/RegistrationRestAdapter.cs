using AutoMapper;
using LittleConqueror.API.Models.Dtos;
using LittleConqueror.AppService.Domain.DrivingModels.Commands;
using LittleConqueror.AppService.Domain.Handlers;
using LittleConqueror.AppService.Domain.Handlers.AuthHandlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LittleConqueror.API.RestAdapters;

[ApiController]
[Route("api/Registration")]
public class RegistrationRestAdapter(
    ICreateRegistrationLinkHandler createRegistrationLinkHandler,
    IRegisterAuthUserHandler registerAuthUserHandler,
    IMapper mapper) : ControllerBase
{
    [Authorize(Roles = "Admin")]
    [HttpPut("createLink")]
    public string CreateLink(CreateRegistrationLinkCommand model)
        => createRegistrationLinkHandler.Handle(model);
    
    [HttpPost]
    public async Task<AuthUserDto> Register([FromBody] RegisterAuthUserCommand model)
        => mapper.Map<AuthUserDto>(await registerAuthUserHandler.Handle(model));
    
}