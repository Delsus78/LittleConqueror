using AutoMapper;
using LittleConqueror.AppService.DomainEntities;
using LittleConqueror.AppService.DrivingPorts;
using LittleConqueror.Models.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace LittleConqueror.RestAdapters;

[ApiController]
[Route("api/Users")]
public class UserRestAdapter(IUserService userService, IMapper mapper) : ControllerBase
{
    [HttpGet("{Id}")]
    public async Task<UserDto> GetUser(int id)
    {
        var user = await userService.GetUserById(id);
        return mapper.Map<UserDto>(user);
    }
    
    [HttpPost]
    public async Task<UserDto> CreateUser([FromBody] UserDto userDto)
    {
        var user = mapper.Map<User>(userDto);
        var addedUser = await userService.CreateUser(user);
        return mapper.Map<UserDto>(addedUser);
    }
    
}