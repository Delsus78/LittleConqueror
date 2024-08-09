using LittleConqueror.AppService.Domain.Models.Entities;

namespace LittleConqueror.AppService.DrivenPorts;

public interface IAuthUserDatabasePort
{
    Task<AuthUser?> GetAuthUserByUsername(string username);
    Task<AuthUser> AddAsync(AuthUser authUser);
    Task<AuthUser?> GetAuthUserById(long userId);
}