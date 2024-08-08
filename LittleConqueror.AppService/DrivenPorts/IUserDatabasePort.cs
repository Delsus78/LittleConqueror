using LittleConqueror.AppService.Domain.Models.Entities;

namespace LittleConqueror.AppService.DrivenPorts;

public interface IUserDatabasePort
{
    Task<User?> GetUserById(int id);
    Task<User?> CreateUser(User user);
}