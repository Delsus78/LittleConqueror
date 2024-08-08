using AutoMapper;
using LittleConqueror.API.Models.Dtos;
using LittleConqueror.AppService.Domain.DrivingModels.Queries;
using LittleConqueror.AppService.Domain.Handlers.TerritoryHandlers;
using LittleConqueror.AppService.Domain.Handlers.UserHandlers;
using LittleConqueror.AppService.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace LittleConqueror.API.RestAdapters;

[ApiController]
[Route("api/Users")]
public class UserRestAdapter(
    IGetUserByIdHandler getUserByIdHandler,
    IGetTerritoryByUserIdHandler getTerritoryByUserIdHandler,
    IGetUserInformationsHandler getUserInformationsHandler,
    IMapper mapper) : ControllerBase
{
    [HttpGet("{userId}")]
    public async Task<UserDto> GetUser(int userId)
        => mapper.Map<UserDto>(
            await getUserByIdHandler.Handle(new GetUserByIdQuery {UserId = userId}));
    
    // [HttpPost]
    // public async Task<UserDto> CreateUser([FromBody] CreateUserCommand command)
    //     => mapper.Map<UserDto>(
    //         await createUserHandler.Handle(command));
    
    [HttpGet("{userId}/Territory")]
    public async Task<TerritoryDto> GetTerritoryOfUser(int userId)
        => mapper.Map<TerritoryDto>(
            await getTerritoryByUserIdHandler.Handle(new GetTerritoryByUserIdQuery {UserId = userId}));
    
    [HttpGet("{userId}/Informations")]
    public async Task<UserInformationsDto> GetUserInformations(int userId, [FromQuery] GetUserInformationsQueryDto query)
        => mapper.Map<UserInformationsDto>(
               await getUserInformationsHandler.Handle(
                   new GetUserInformationsQuery {
                       UserId = userId, 
                       IncludeResources = query.IncludeResources,
                       IncludeTerritory = query.IncludeTerritory
                   })) ?? 
           throw new AppException("User not found", 404);
}