using LittleConqueror.AppService.Domain.Models;

namespace LittleConqueror.AppService.DrivenPorts;

public interface ITerritoryDatabasePort
{
    public Task<Territory> CreateTerritory(Territory territory);
    public Task<Territory> GetTerritoryOfUser(int userId);
    public Task<Territory> GetTerritoryById(int territoryId);
    public Task<Territory> UpdateTerritory(Territory territory);
}