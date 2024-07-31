using LittleConqueror.AppService.DomainEntities;

namespace LittleConqueror.AppService.Ports;

public interface ICityRepository
{
    City GetCityById(int id);
    List<City> GetCitiesByTerritoryId(int territoryId);
    void AddCity(City city);
    void UpdateCity(City city);
}