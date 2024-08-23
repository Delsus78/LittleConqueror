using LittleConqueror.AppService.Domain.Models;
using LittleConqueror.AppService.DrivenPorts;
using LittleConqueror.AppService.Exceptions;
using LittleConqueror.Infrastructure.JsonConverters;
using LittleConqueror.Persistence.JsonConverters;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace LittleConqueror.Infrastructure.FetchingAdapters;

public class NominatimOSMFetcherAdapter(
    IHttpClientFactory httpClientFactory,
    IOptions<OSMSettings> options) : IOSMCityFetcherPort
{
    private OSMSettings settings => options.Value;
    private IEnumerable<int> AuthorizedZooms => settings.AuthorizedZoom.Reverse();

    private readonly JsonSerializerSettings jsonSettings = new()
    {
        ContractResolver = new DefaultContractResolver
        {
            NamingStrategy = new CamelCaseNamingStrategy
            {
                ProcessDictionaryKeys = false,
                OverrideSpecifiedNames = true
            }
        },
        Converters = new List<JsonConverter>
        {
            new StringOrNumberToDoubleConverter(),
            new StringToIntConverter(),
            new StringToCharConverter()
        }
    };

    public async Task<CityOSM> GetCityByLongitudeAndLatitude(double longitude, double latitude, int zoomIndex = 0)
    {
        var httpClient = httpClientFactory.CreateClient("NominatimOSM");

        var urlparams = $"reverse?lat={latitude.ToURIString()}&lon={longitude.ToURIString()}&zoom={AuthorizedZooms.ElementAt(zoomIndex)}&format=jsonv2&class=place&addressdetails=1&hierarchy=0&group_hierarchy=1&polygon_geojson=1&extratags=1";

        var response = await httpClient.GetAsync(urlparams);

        if (!response.IsSuccessStatusCode)
            throw new Exception("Error while fetching data from Nominatim OSM : " + response.ReasonPhrase);
        
        var content = await response.Content.ReadAsStringAsync();
        
        var city = JsonConvert.DeserializeObject<CityOSM>(content, jsonSettings);
        
        if (!IsValidCity(city) && zoomIndex < AuthorizedZooms.Count() - 1)
                city = await GetCityByLongitudeAndLatitude(longitude, latitude, zoomIndex + 1);
        else if (!IsValidCity(city))
            throw new AppException("City not found", 404);
            
        return city;
    }

    public async Task<CityOSM> GetCityByOsmId(long osmId, char osmType)
    {
        var httpClient = httpClientFactory.CreateClient("NominatimOSM");
        
        osmType = osmType.ToString().ToUpper()[0];

        var urlparams = $"details?format=json&osmid={osmId}&osmtype={osmType}&zoom=10&polygon_geojson=1&extratags=1";

        var response = await httpClient.GetAsync(urlparams);

        if (!response.IsSuccessStatusCode)
            throw new Exception("Error while fetching data from Nominatim OSM : " + response.ReasonPhrase);
        
        var content = await response.Content.ReadAsStringAsync();
        
        var cityOsmByDetails = JsonConvert.DeserializeObject<CityOSMByDetails>(content, jsonSettings);

        var name = cityOsmByDetails.Localname;
        if (name == null || 
            !cityOsmByDetails.Names.TryGetValue("name:fr", out name) ||
            !cityOsmByDetails.Names.TryGetValue("name:en", out name))
            name = cityOsmByDetails.Names.First().Value;
        
        return new CityOSM(
            cityOsmByDetails.OsmId, 
            cityOsmByDetails.OsmIdType, 
            cityOsmByDetails.Centroid.coordinates[1], 
            cityOsmByDetails.Centroid.coordinates[0],
            name,
            cityOsmByDetails.Extratags,
            cityOsmByDetails.AddressType,
            cityOsmByDetails.Geometry);
    }
    
    private bool IsValidCity(CityOSM? city)
    {
        if (city?.Extratags?.Population == null)
            return false;
        
        if (city.AddressType == null || settings.UnauthorizedAddressTypes.Contains(city.AddressType))
            return false;
        
        return true;
    }
}