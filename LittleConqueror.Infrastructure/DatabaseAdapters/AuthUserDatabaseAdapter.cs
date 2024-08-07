using LittleConqueror.AppService.Domain.Models.Entities;
using LittleConqueror.AppService.DrivenPorts;
using LittleConqueror.Infrastructure.Repositories;

namespace LittleConqueror.Infrastructure.DatabaseAdapters;

public class AuthUserDatabaseAdapter(
    AuthUserRepository authUserRepository) : IAuthUserDatabasePort
{
    public async Task<AuthUser?> GetAuthUserByUsername(string username)
        => await authUserRepository.GetAsync(x => x.Username == username);
    
    public async Task<AuthUser> AddAsync(AuthUser authUser)
    {
        var entityEntry = await authUserRepository.CreateAsync(authUser);
        
        await authUserRepository.SaveAsync();
        
        return entityEntry.Entity;
    }

    public async Task<AuthUser?> GetAuthUserById(int userId)
        => await authUserRepository.GetAsync(x => x.UserId == userId);
}