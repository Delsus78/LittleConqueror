using LittleConqueror.AppService.Domain.DrivingModels.Queries;
using LittleConqueror.AppService.Domain.Services;
using LittleConqueror.AppService.DrivenPorts;
using ActionEntities = LittleConqueror.AppService.Domain.Models.Entities.ActionEntities;
namespace LittleConqueror.AppService.Domain.Handlers.ActionHandlers;

public interface IGetPaginatedActionsByUserIdHandler
{
    Task<(int total, List<ActionEntities.Action> actions)> Handle(GetPaginatedActionsByUserIdQuery query);
}

public class GetPaginatedActionsByUserIdHandler(
    IActionDatabasePort actionDatabase,
    IUserContext userContext) : IGetPaginatedActionsByUserIdHandler
{
    public async Task<(int total, List<ActionEntities.Action> actions)> Handle(GetPaginatedActionsByUserIdQuery query)
    {
        var skip = query.Page * query.PageSize;
        var take = query.PageSize;
        
        return await actionDatabase.GetPaginatedActionsByUserId(userContext.UserId, skip, take);
    }
}