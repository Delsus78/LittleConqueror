using AutoMapper;
using LittleConqueror.AppService.Domain.DrivingModels.Commands;
using LittleConqueror.AppService.Domain.DrivingModels.Queries;
using LittleConqueror.AppService.Domain.Handlers;
using LittleConqueror.AppService.Exceptions;
using LittleConqueror.Models.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace LittleConqueror.RestAdapters;

[ApiController]
[Route("api/Users")]
public class UserRestAdapter(
    ICreateUserHandler createUserHandler,
    IGetUserByIdHandler getUserByIdHandler,
    IGetTerritoryByUserIdHandler getTerritoryByUserIdHandler,
    IMapper mapper) : ControllerBase
{
    [HttpGet("{Id}")]
    public async Task<UserDto> GetUser(int id)
        => mapper.Map<UserDto>(
            await getUserByIdHandler.Handle(new GetUserByIdQuery {UserId = id}) 
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
    
}