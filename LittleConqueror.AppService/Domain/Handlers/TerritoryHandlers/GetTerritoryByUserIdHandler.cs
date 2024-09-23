using LittleConqueror.AppService.Domain.DrivingModels.Queries;
using LittleConqueror.AppService.Domain.Models.Entities;
using LittleConqueror.AppService.Domain.Services;
using LittleConqueror.AppService.DrivenPorts;
using LittleConqueror.AppService.Exceptions;

namespace LittleConqueror.AppService.Domain.Handlers.TerritoryHandlers;

public interface IGetTerritoryByUserIdHandler
{
    public Task<Territory> Handle(GetTerritoryByUserIdQuery query);
}
public class GetTerritoryByUserIdHandler(
    ITerritoryDatabasePort territoryDatabase,
    IUserContext userContext) : IGetTerritoryByUserIdHandler
{
    public async Task<Territory> Handle(GetTerritoryByUserIdQuery query)
    {
        if (userContext.IsUnauthorized(query.UserId))
            throw new AppException("You are not the owner of this territory", 403);
        
        return await territoryDatabase.GetTerritoryOfUser(query.UserId) ??
               throw new AppException("Territory not found", 404);
    }
}