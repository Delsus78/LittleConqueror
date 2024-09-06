using AutoMapper;
using LittleConqueror.API.Models.Dtos;
using LittleConqueror.AppService.Domain.DrivingModels.Queries;
using LittleConqueror.AppService.Domain.Handlers.TechResearchHandlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LittleConqueror.API.RestAdapters;

[Authorize]
[ApiController]
[Route("api/TechResearches")]
public class TechResearchRestAdapter(IGetTechTreeOfUserIdHandler techTreeOfUserIdHandler, IMapper mapper) : ControllerBase
{
    [HttpGet("{userId}")]
    public async Task<IEnumerable<TechResearchDataDto>> GetTechTreeOfUser(long userId)
        => mapper.Map<IEnumerable<TechResearchDataDto>>(await techTreeOfUserIdHandler.Handle(new GetTechTreeOfUserIdQuery {UserId = userId}));
}