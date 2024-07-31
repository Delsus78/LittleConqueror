using LittleConqueror.AppService.DomainEntities;

namespace LittleConqueror.AppService.DrivingPorts;

public interface ICityService
{
    City GetCityById(int id);
    City GetCityByLongitudeAndLatitude(double longitude, double latitude);
}