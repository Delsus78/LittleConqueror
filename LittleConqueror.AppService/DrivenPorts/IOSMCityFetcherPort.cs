using LittleConqueror.AppService.Domain.DrivenModels;

namespace LittleConqueror.AppService.DrivenPorts;

public interface IOSMCityFetcherPort
{
    Task<CityOSM> GetCityByLongitudeAndLatitude(double longitude, double latitude);
}