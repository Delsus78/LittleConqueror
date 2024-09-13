using AutoMapper;
using LittleConqueror.API.Models.Dtos;
using LittleConqueror.AppService.Domain.DrivingModels.Queries;
using LittleConqueror.AppService.Domain.Handlers.TechResearchHandlers;
using LittleConqueror.AppService.Domain.Models.TechResearches;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LittleConqueror.API.RestAdapters;

[Authorize]
[ApiController]
[Route("api/TechResearches")]
public class TechResearchRestAdapter(
    IGetTechTreeOfUserIdHandler techTreeOfUserIdHandler,
    IGetSciencePointsOfUserIdHandler getSciencePointsOfUserIdHandler,
    IMapper mapper) : ControllerBase
{
    [HttpGet("{userId}")]
    public async Task<IEnumerable<TechResearchDataDto>> GetTechTreeOfUser(long userId)
        => mapper.Map<IEnumerable<TechResearchDataDto>>(await techTreeOfUserIdHandler.Handle(new GetTechTreeOfUserIdQuery {UserId = userId}));
    
    [HttpGet("{userId}/SciencePoints")]
    public async Task<Dictionary<TechResearchCategories, int>> GetSciencePointsOfUser(long userId)
        => await getSciencePointsOfUserIdHandler.Handle(new GetSciencePointsOfUserIdQuery {UserId = userId});
}