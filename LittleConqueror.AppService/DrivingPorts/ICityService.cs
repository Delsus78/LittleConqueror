using LittleConqueror.AppService.DomainEntities;

namespace LittleConqueror.AppService.DrivingPorts;

public interface ICityService
{
    Task<City> GetCityById(int id);
    Task<City> GetCityByLongitudeAndLatitude(double longitude, double latitude);
}