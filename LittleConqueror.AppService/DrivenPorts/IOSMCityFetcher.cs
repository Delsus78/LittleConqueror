using LittleConqueror.AppService.Domain.Models;

namespace LittleConqueror.AppService.DrivenPorts;

public interface IOSMCityFetcher
{
    Task<OSMCityResponse> GetCityByLongitudeAndLatitude(double longitude, double latitude);
}