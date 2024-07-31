using LittleConqueror.AppService.DomainEntities;
using LittleConqueror.AppService.DrivenPorts;
using LittleConqueror.AppService.DrivingPorts;

namespace LittleConqueror.AppService.Domain.Services;

public class CityService(IOSMCityFetcher osmCityFetcher) : ICityService
{
    public Task<City> GetCityById(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<City> GetCityByLongitudeAndLatitude(double longitude, double latitude)
    {
        var city = await osmCityFetcher.GetCityByLongitudeAndLatitude(longitude, latitude);
        
        // TODO Get Database City informations 

        return new City
        {
            Id = city.PlaceId,
            Country = city.Address?.Country ?? string.Empty,
            Name = city.Name,
            Latitude = city.Lat,
            Longitude = city.Lon,
            Population = city.Extratags?.Population ?? 0,
            Geojson = new Geojson
            {
                Type = city.Geojson?.Type ?? string.Empty,
                Coordinates = city.Geojson?.Coordinates ?? new List<List<List<double>>>()
            }
            //Territory = TODO with db info
        };
    }
}