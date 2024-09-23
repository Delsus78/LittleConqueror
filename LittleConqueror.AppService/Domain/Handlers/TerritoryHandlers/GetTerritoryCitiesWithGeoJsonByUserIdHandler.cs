using LittleConqueror.AppService.Domain.DrivingModels.Queries;
using LittleConqueror.AppService.Domain.Models.Entities;
using LittleConqueror.AppService.Domain.Services;
using LittleConqueror.AppService.DrivenPorts;
using LittleConqueror.AppService.Exceptions;

namespace LittleConqueror.AppService.Domain.Handlers.TerritoryHandlers;

public interface IGetTerritoryCitiesWithGeoJsonHandler
{
    Task<List<City>> Handle(GetTerritoryCitiesWithGeoJsonByUserIdQuery query);
}

public class GetTerritoryCitiesWithGeoJsonByUserIdHandler(
    ITerritoryDatabasePort territoryDatabase,
    IUserContext userContext
    ) : IGetTerritoryCitiesWithGeoJsonHandler
{
    public async Task<List<City>> Handle(GetTerritoryCitiesWithGeoJsonByUserIdQuery query)
    {
        if (userContext.IsUnauthorized(query.UserId))
            throw new AppException("You are not the owner of this territory", 403);
        
        return await territoryDatabase.GetTerritoryCitiesFullDataOfUser(query.UserId) ??
               throw new AppException("Territory not found", 404);
    }
}