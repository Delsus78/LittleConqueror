using AutoMapper;
using LittleConqueror.AppService.DrivingPorts;
using LittleConqueror.Models.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace LittleConqueror.RestAdapters;

[ApiController]
[Route("api/Cities")]
public class CityRestAdapter(ICityService cityService, IMapper mapper) : ControllerBase
{
    [HttpGet]
    public async Task<CityDto> GetCity([FromQuery] CityRequestDto cityRequestDto)
    {
        var city = await cityService.GetCityByLongitudeAndLatitude(cityRequestDto.Longitude, cityRequestDto.Latitude);
        return mapper.Map<CityDto>(city);
    }
}