using LittleConqueror.AppService.Domain.DrivingModels.Queries;
using LittleConqueror.AppService.Domain.Services;
using LittleConqueror.AppService.DrivenPorts;
using LittleConqueror.AppService.Exceptions;
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
        if (userContext.IsUnauthorized(query.UserId))
            throw new AppException("You are not the owner of this action list", 403);
        
        var skip = query.Page * query.PageSize;
        var take = query.PageSize;
        
        return await actionDatabase.GetPaginatedActionsByUserId(query.UserId, skip, take);
    }
}