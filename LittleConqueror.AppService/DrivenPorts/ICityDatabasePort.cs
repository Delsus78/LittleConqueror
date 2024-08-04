using LittleConqueror.AppService.Domain.DrivenModels;
using LittleConqueror.AppService.DomainEntities;

namespace LittleConqueror.AppService.DrivenPorts;

public interface ICityDatabasePort
{
    Task<City?> GetCityById(int id);
    Task<City?> AddCity(City city);
}