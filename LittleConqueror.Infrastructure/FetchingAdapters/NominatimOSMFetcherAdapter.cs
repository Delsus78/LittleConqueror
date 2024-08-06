using System.Text.Json;
using LittleConqueror.AppService.Domain.Models;
using LittleConqueror.AppService.DrivenPorts;
using LittleConqueror.Persistence.JsonConverters;

namespace LittleConqueror.Infrastructure.FetchingAdapters;

public class NominatimOSMFetcherAdapter(IHttpClientFactory httpClientFactory) : IOSMCityFetcherPort
{
    private readonly JsonSerializerOptions jsonOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        Converters = {new StringOrNumberToDoubleConverter(), new StringToIntConverter()}
    };

    public async Task<CityOSM> GetCityByLongitudeAndLatitude(double longitude, double latitude)
    {
        var httpClient = httpClientFactory.CreateClient("NominatimOSM");

        var urlparams = $"reverse?lat={latitude.ToURIString()}&lon={longitude.ToURIString()}&zoom=10&format=jsonv2&polygon_geojson=1&extratags=1";

        var response = await httpClient.GetAsync(urlparams);

        if (!response.IsSuccessStatusCode)
            throw new Exception("Error while fetching data from Nominatim OSM : " + response.ReasonPhrase);
        
        var content = await response.Content.ReadAsStringAsync();
        
        var city = JsonSerializer.Deserialize<CityOSM>(content, jsonOptions);
        return city;
    }

    public async Task<CityOSM> GetCityByOsmId(int osmId, char osmType)
    {
        var httpClient = httpClientFactory.CreateClient("NominatimOSM");

        var urlparams = $"details?format=json&osmid={osmId}&osmtype={osmType}&zoom=10&polygon_geojson=1&extratags=1";

        var response = await httpClient.GetAsync(urlparams);

        if (!response.IsSuccessStatusCode)
            throw new Exception("Error while fetching data from Nominatim OSM : " + response.ReasonPhrase);
        
        var content = await response.Content.ReadAsStringAsync();
        
        var cityOsmByDetails = JsonSerializer.Deserialize<CityOSMByDetails>(content, jsonOptions);


        return new CityOSM(
            cityOsmByDetails.OsmId, 
            cityOsmByDetails.OsmIdType, 
            cityOsmByDetails.Centroid.coordinates[1], 
            cityOsmByDetails.Centroid.coordinates[0], 
            0, 
            cityOsmByDetails.Names.Name,
            cityOsmByDetails.Extratags, 
            cityOsmByDetails.Geometry);
    }
}