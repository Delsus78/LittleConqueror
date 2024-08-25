using AutoMapper;
using LittleConqueror.API.Models.Dtos.ActionsDtos;
using LittleConqueror.AppService.Domain.DrivingModels.Queries;
using LittleConqueror.AppService.Domain.Handlers.ActionHandlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LittleConqueror.API.RestAdapters;

[Authorize]
[ApiController]
[Route("api/Actions")]
public class ActionRestAdapter(
    IGetPaginatedActionsByUserIdHandler getPaginatedActionsByUserIdHandler,
    IMapper mapper) : ControllerBase
{
    [HttpGet]
    public async Task<ActionsListDto> GetPaginatedActionsByUserId([FromQuery] GetPaginatedActionsByUserIdQuery query)
        => mapper.Map<ActionsListDto>((await getPaginatedActionsByUserIdHandler.Handle(query)));
}