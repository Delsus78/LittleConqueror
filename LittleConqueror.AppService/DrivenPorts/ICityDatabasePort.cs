using LittleConqueror.AppService.Domain.Models.Entities;

namespace LittleConqueror.AppService.DrivenPorts;

public interface ICityDatabasePort
{
    Task<City?> GetCityById(int id);
    Task<City?> AddCity(City city);
    Task SetTerritoryId(int cityId, int territoryId);
}