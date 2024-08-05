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

        var urlparams = $"reverse?format=jsonv2&lat={latitude.ToURIString()}&lon={longitude.ToURIString()}&zoom=10&polygon_geojson=1&extratags=1";

        var response = await httpClient.GetAsync(urlparams);

        if (!response.IsSuccessStatusCode)
            throw new Exception("Error while fetching data from Nominatim OSM : " + response.ReasonPhrase);
        
        var content = await response.Content.ReadAsStringAsync();
        
        var city = JsonSerializer.Deserialize<CityOSM>(content, jsonOptions);
        return city;
    }
}