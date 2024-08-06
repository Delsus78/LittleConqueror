using LittleConqueror.AppService.Domain.Models;

namespace LittleConqueror.AppService.DrivenPorts;

public interface IAuthUserDatabasePort
{
    Task<AuthUser?> GetAuthUserByUsername(string username);
    Task<AuthUser> AddAsync(AuthUser authUser);
    Task<AuthUser?> GetAuthUserById(int userId);
}