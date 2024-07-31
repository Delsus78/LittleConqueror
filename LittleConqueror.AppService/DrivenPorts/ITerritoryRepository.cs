using LittleConqueror.AppService.DomainEntities;

namespace LittleConqueror.AppService.Ports;

public interface ITerritoryRepository
{
    Territory GetTerritoryById(int id);
    void UpdateTerritory(Territory territory);
    void SetUser(Territory territory, int userId);
    void AddCity(Territory territory, int cityId);
}