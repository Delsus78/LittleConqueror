using LittleConqueror.AppService.DomainEntities;
using LittleConqueror.AppService.DrivenPorts;
using LittleConqueror.AppService.DrivingPorts;

namespace LittleConqueror.AppService.Domain.Services;

public class UserService(IUserDatabasePort userDatabase) : IUserService
{
    public Task<User?> GetUserById(int id)
        => userDatabase.GetUserById(id);
    
    public Task<User?> CreateUser(User user)
        => userDatabase.CreateUser(user);
}