using LittleConqueror.AppService.DomainEntities;
using LittleConqueror.AppService.DrivenPorts;
using LittleConqueror.Infrastructure.Repositories;
using LittleConqueror.Persistence.Entities;

namespace LittleConqueror.Infrastructure.DatabaseAdapters;

public class UserDatabaseAdapter(UserRepository userRepository) : IUserDatabasePort
{
    public async Task<User?> GetUserById(int id)
    {
        var userEntity = await userRepository.GetAsync(entity => entity.Id == id);
        
        if (userEntity == null)
            return null;

        return new User
        {
            Id = userEntity.Id,
            Name = userEntity.Name
        };
    }

    public async Task<User?> CreateUser(User user)
    {
        var userEntity = new UserEntity
        {
            Name = user.Name
        };

        await userRepository.CreateAsync(userEntity);
        await userRepository.SaveAsync();
        
        return new User
        {
            Id = userEntity.Id,
            Name = userEntity.Name
        };
    }
}