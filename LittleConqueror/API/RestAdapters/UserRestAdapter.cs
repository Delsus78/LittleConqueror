using AutoMapper;
using LittleConqueror.API.Models.Dtos;
using LittleConqueror.AppService.Domain.DrivingModels.Commands;
using LittleConqueror.AppService.Domain.DrivingModels.Queries;
using LittleConqueror.AppService.Domain.Handlers;
using LittleConqueror.AppService.Domain.Handlers.UserHandlers;
using LittleConqueror.AppService.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace LittleConqueror.API.RestAdapters;

[ApiController]
[Route("api/Users")]
public class UserRestAdapter(
    ICreateUserHandler createUserHandler,
    IGetUserByIdHandler getUserByIdHandler,
    IGetTerritoryByUserIdHandler getTerritoryByUserIdHandler,
    IGetUserInformationsHandler getUserInformationsHandler,
    IMapper mapper) : ControllerBase
{
    [HttpGet("{userId}")]
    public async Task<UserDto> GetUser(int userId)
        => mapper.Map<UserDto>(
            await getUserByIdHandler.Handle(new GetUserByIdQuery {UserId = userId}) 
            ?? throw new AppException("User not found", 404));
    
    [HttpPost]
    public async Task<UserDto> CreateUser([FromBody] CreateUserCommand command)
        => mapper.Map<UserDto>(
            await createUserHandler.Handle(command));
    
    [HttpGet("{userId}/Territory")]
    public async Task<TerritoryDto> GetTerritoryOfUser(int userId)
        => mapper.Map<TerritoryDto>(
            await getTerritoryByUserIdHandler.Handle(new GetTerritoryByUserIdQuery {UserId = userId}) 
            ?? throw new AppException("Territory not found", 404));
    
    [HttpGet("{userId}/Informations")]
    public async Task<UserInformationsDto> GetUserInformations(int userId)
        => mapper.Map<UserInformationsDto>(
            await getUserInformationsHandler.Handle(new GetUserInformationsQuery {UserId = userId}) 
            ?? throw new AppException("User not found", 404));
}