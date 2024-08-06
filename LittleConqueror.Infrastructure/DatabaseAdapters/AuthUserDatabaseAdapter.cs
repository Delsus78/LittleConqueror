using AutoMapper;
using LittleConqueror.AppService.Domain.Models;
using LittleConqueror.AppService.DrivenPorts;
using LittleConqueror.Infrastructure.Entities.DatabaseEntities;
using LittleConqueror.Infrastructure.Repositories;

namespace LittleConqueror.Infrastructure.DatabaseAdapters;

public class AuthUserDatabaseAdapter(
    AuthUserRepository authUserRepository, 
    IMapper mapper) : IAuthUserDatabasePort
{
    public async Task<AuthUser?> GetAuthUserByUsernameAndHash(string username, string hash)
    {
        var entity = await authUserRepository.GetAsync(x => x.Username == username && x.Hash == hash);
        return entity == null ? null : mapper.Map<AuthUser>(entity);
    }

    public async Task<AuthUser?> GetAuthUserByUsername(string username)
    {
        var entity = await authUserRepository.GetAsync(x => x.Username == username);
        return entity == null ? null : mapper.Map<AuthUser>(entity);
    }

    public async Task<AuthUser> AddAsync(AuthUser authUser)
    {
        var entityEntry = await authUserRepository.CreateAsync(new AuthUserEntity
        {
            Id = authUser.Id,
            Username = authUser.Username,
            Hash = authUser.Hash,
            Role = authUser.Role,
            UserId = authUser.User.Id
        });
        
        await authUserRepository.SaveAsync();
        
        return mapper.Map<AuthUser>(entityEntry.Entity);
    }

    public async Task<AuthUser?> GetAuthUserById(int userId)
    {
        var entity = await authUserRepository.GetAsync(x => x.UserId == userId);
        return entity == null ? null : mapper.Map<AuthUser>(entity);
    }
}