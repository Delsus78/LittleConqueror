using LittleConqueror.AppService.Domain.Models;

namespace LittleConqueror.AppService.DrivenPorts;

public interface IUserDatabasePort
{
    Task<User?> GetUserById(int id);
    Task<User?> CreateUser(User user);
    Task<bool> IsUserExist(int id);
}