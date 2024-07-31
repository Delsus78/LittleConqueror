using System.Text.Json;
using LittleConqueror.AppService.DrivenPorts;
using LittleConqueror.Persistence.JsonConverters;
using OSMCityResponse = LittleConqueror.AppService.Domain.Models.OSMCityResponse;

namespace LittleConqueror.Infrastructure.FetchingAdapters;

public class NominatimOSMFetcherAdapter(IHttpClientFactory httpClientFactory) : IOSMCityFetcher
{
    private readonly JsonSerializerOptions jsonOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        Converters = {new StringOrNumberToDoubleConverter(), new StringToIntConverter()}
    };

    public async Task<OSMCityResponse> GetCityByLongitudeAndLatitude(double longitude, double latitude)
    {
        var httpClient = httpClientFactory.CreateClient("NominatimOSM");

        var urlparams = $"reverse?format=jsonv2&lat={latitude.ToURIString()}&lon={longitude.ToURIString()}&zoom=10&polygon_geojson=1&extratags=1";

        var response = await httpClient.GetAsync(urlparams);

        if (!response.IsSuccessStatusCode)
            throw new Exception("Error while fetching data from Nominatim OSM : " + response.ReasonPhrase);
        
        var content = await response.Content.ReadAsStringAsync();
        
        var city = JsonSerializer.Deserialize<OSMCityResponse>(content, jsonOptions);
        return city;
    }
}