using LittleConqueror.AppService.Domain.Models.Entities;

namespace LittleConqueror.AppService.DrivenPorts;

public interface ICityDatabasePort
{
    Task<City?> GetCityById(long id);
    Task<City?> GetCityWithActionAndTerritoryOwnerId(long id);
    Task<City> AddCity(City city);
    Task SetTerritoryId(long cityId, long territoryId);
}