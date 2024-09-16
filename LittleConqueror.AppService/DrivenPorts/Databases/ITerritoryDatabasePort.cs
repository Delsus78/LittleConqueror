using LittleConqueror.AppService.Domain.Models.Entities;

namespace LittleConqueror.AppService.DrivenPorts;

public interface ITerritoryDatabasePort
{
    public Task<Territory> CreateTerritory(Territory territory);
    public Task<Territory?> GetTerritoryOfUser(long userId);
    public Task<List<City>> GetTerritoryCitiesFullDataOfUser(long userId);
    public Task<Territory?> GetTerritoryById(long territoryId);
}