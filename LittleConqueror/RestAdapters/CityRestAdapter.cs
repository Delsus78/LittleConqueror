using AutoMapper;
using LittleConqueror.AppService.Domain.DrivingModels.Queries;
using LittleConqueror.AppService.Domain.Handlers;
using LittleConqueror.Models.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace LittleConqueror.RestAdapters;

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