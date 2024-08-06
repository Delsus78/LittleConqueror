using LittleConqueror.AppService.Domain.Models;

namespace LittleConqueror.AppService.DrivenPorts;

public interface IOSMCityFetcherPort
{
    Task<CityOSM> GetCityByLongitudeAndLatitude(double longitude, double latitude);
    Task<CityOSM> GetCityByOsmId(int osmId, char osmType);
}