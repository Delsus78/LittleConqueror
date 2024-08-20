using LittleConqueror.AppService.Domain.Models.Entities;
using ActionEntities = LittleConqueror.AppService.Domain.Models.Entities.ActionEntities;

namespace LittleConqueror.AppService.DrivenPorts;

public interface ICityDatabasePort
{
    Task<City?> GetCityById(long id);
    Task<City?> GetCityWithAction(long id);
    Task<City?> AddCity(City city);
    Task SetTerritoryId(long cityId, long territoryId);
    Task SetAction(City city, ActionEntities.Action action);
}