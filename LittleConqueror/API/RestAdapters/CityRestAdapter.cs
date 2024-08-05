using AutoMapper;
using LittleConqueror.API.Models.Dtos;
using LittleConqueror.AppService.Domain.DrivingModels.Queries;
using LittleConqueror.AppService.Domain.Handlers;
using Microsoft.AspNetCore.Mvc;

namespace LittleConqueror.API.RestAdapters;

[ApiController]
[Route("api/Cities")]
public class CityRestAdapter(
    IGetCityByLongitudeAndLatitudeHandler getCityByLongitudeAndLatitudeHandler, 
    IMapper mapper) : ControllerBase
{
    [HttpGet]
    public async Task<CityDto> GetCity([FromQuery] GetCityByLongitudeLatitudeQuery query)
        => mapper.Map<CityDto>(await getCityByLongitudeAndLatitudeHandler.Handle(query));
    
}