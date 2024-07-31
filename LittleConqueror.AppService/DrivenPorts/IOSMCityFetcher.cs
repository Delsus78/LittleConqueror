using Little_Conqueror.Models.DataTransferObjects;

namespace LittleConqueror.AppService.Ports;

public interface IOSMCityFetcher
{
    Task<OSMCityResponse> GetCityByLongitudeAndLatitude(double longitude, double latitude);
}