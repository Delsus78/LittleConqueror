using LittleConqueror.AppService.DomainEntities;

namespace LittleConqueror.AppService.Ports;

public interface IUserRepository
{
    User GetUserById(int id);
    void SaveUser(User user);
    void SetTerritory(int userId, int territoryId);
}