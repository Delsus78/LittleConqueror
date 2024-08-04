using LittleConqueror.AppService.DomainEntities;

namespace LittleConqueror.AppService.DrivingPorts;

public interface IUserService
{
    Task<User?> GetUserById(int id);
    Task<User?> CreateUser(User user);
}