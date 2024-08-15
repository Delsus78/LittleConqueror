using AutoMapper;
using LittleConqueror.API.Models.Dtos;
using LittleConqueror.AppService.Domain.DrivingModels.Commands;
using LittleConqueror.AppService.Domain.DrivingModels.Queries;
using LittleConqueror.AppService.Domain.Handlers.CityHandlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LittleConqueror.API.RestAdapters;

[ApiController]
[Route("api/Cities")]
public class CityRestAdapter(
    IGetCityByLongitudeAndLatitudeHandler getCityByLongitudeAndLatitudeHandler, 
    IGetCityByOsmIdHandler getCityByOsmIdHandler,
    IAddCityToATerritoryHandler addCityToTerritoryHandler,
    IMapper mapper) : ControllerBase
{
    [HttpGet("ByLonLat")]
    public async Task<CityDto> GetCityByLonLat([FromQuery] GetCityByLongitudeLatitudeQuery query)
        => mapper.Map<CityDto>(await getCityByLongitudeAndLatitudeHandler.Handle(query));
    
    [HttpGet("ByOsmId")]
    public async Task<CityDto> GetCityByOsmId([FromQuery] long osmId, [FromQuery] char osmType)
        => mapper.Map<CityDto>(await getCityByOsmIdHandler.Handle(new GetCityByOsmIdQuery { OsmId = osmId, OsmType = osmType }));
    
    [Authorize(Roles="Admin")]
    [HttpPost("AddToTerritory")]
    public async Task AddCityToTerritory([FromBody] AddCityToATerritoryCommand command)
        => await addCityToTerritoryHandler.Handle(command);
}