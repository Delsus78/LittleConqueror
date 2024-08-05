using AutoMapper;
using LittleConqueror.AppService.Domain.Models;
using LittleConqueror.AppService.DrivenPorts;
using LittleConqueror.Infrastructure.Entities.DatabaseEntities;
using LittleConqueror.Infrastructure.Repositories;

namespace LittleConqueror.Infrastructure.DatabaseAdapters;

public class UserDatabaseAdapter(
    UserRepository userRepository, 
    IMapper mapper) : IUserDatabasePort
{
    public async Task<User?> GetUserById(int id)
    {
        var userEntity = await userRepository.GetAsync(entity => entity.Id == id);
        
        return userEntity == null ? null : mapper.Map<User>(userEntity);
    }

    public async Task<User?> CreateUser(User user)
    {
        var userEntity = new UserEntity
        {
            Name = user.Name
        };

        var entityEntry = await userRepository.CreateAsync(userEntity);
        await userRepository.SaveAsync();

        return mapper.Map<User>(entityEntry.Entity);
    }

    public Task<bool> IsUserExist(int id) 
        => userRepository.AnyAsync(entity => entity.Id == id);
}