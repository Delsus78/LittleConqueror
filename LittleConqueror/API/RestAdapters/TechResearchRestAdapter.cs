using AutoMapper;
using LittleConqueror.API.Models.Dtos;
using LittleConqueror.AppService.Domain.DrivingModels.Commands;
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
    ISetTechToResearchOfUserIdHandler setTechToResearchOfUserIdHandler,
    ICancelTechResearchOfUserIdHandler cancelTechResearchOfUserIdHandler,
    IMapper mapper) : ControllerBase
{
    [HttpGet("{userId}")]
    public async Task<IEnumerable<TechResearchDataDto>> GetTechTreeOfUser(long userId)
        => mapper.Map<IEnumerable<TechResearchDataDto>>(await techTreeOfUserIdHandler.Handle(new GetTechTreeOfUserIdQuery {UserId = userId}));
    
    [HttpGet("{userId}/SciencePoints")]
    public async Task<Dictionary<TechResearchCategory, int>> GetSciencePointsOfUser(long userId)
        => await getSciencePointsOfUserIdHandler.Handle(new GetSciencePointsOfUserIdQuery {UserId = userId});
    
    [HttpPost("{userId}/start/{techResearchType}")]
    public async Task SetTechToResearchOfUser(long userId, TechResearchType techResearchType, bool force)
        => await setTechToResearchOfUserIdHandler.Handle(new SetTechToResearchOfUserIdCommand {UserId = userId, TechResearchType = techResearchType, Force = force});
    
    [HttpPost("{userId}/cancel/{techResearchType}")]
    public async Task CancelTechResearch(long userId, TechResearchType techResearchType)
        => await cancelTechResearchOfUserIdHandler.Handle(new CancelTechToResearchOfUserIdCommand {UserId = userId, TechResearchType = techResearchType});
}