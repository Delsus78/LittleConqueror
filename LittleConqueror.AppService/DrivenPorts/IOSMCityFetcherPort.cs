using LittleConqueror.AppService.Domain.Models;

namespace LittleConqueror.AppService.DrivenPorts;

public interface IOSMCityFetcherPort
{
    Task<CityOSM> GetCityByLongitudeAndLatitude(double longitude, double latitude);
}