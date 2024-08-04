using LittleConqueror.AppService.DomainEntities;

namespace LittleConqueror.AppService.DrivenPorts;

public interface IUserDatabasePort
{
    Task<User?> GetUserById(int id);
    Task<User?> CreateUser(User user);
}