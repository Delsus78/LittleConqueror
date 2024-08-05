using LittleConqueror.AppService.Domain.Models;

namespace LittleConqueror.AppService.DrivenPorts;

public interface ICityDatabasePort
{
    Task<City?> GetCityById(int id);
    Task<City?> AddCity(City city);
}