using LittleConqueror.AppService.Domain.Models.Entities;
using LittleConqueror.AppService.DrivenPorts;
using LittleConqueror.Infrastructure.Repositories;

namespace LittleConqueror.Infrastructure.DatabaseAdapters;

public class UserDatabaseAdapter(
    UserRepository userRepository) : IUserDatabasePort
{
    public async Task<User?> GetUserById(int id)
        => await userRepository.GetAsync(entity => entity.Id == id);

    public async Task<User?> CreateUser(User user)
    {
        var entityEntry = await userRepository.CreateAsync(user);
        await userRepository.SaveAsync();

        return entityEntry.Entity;
    }
}