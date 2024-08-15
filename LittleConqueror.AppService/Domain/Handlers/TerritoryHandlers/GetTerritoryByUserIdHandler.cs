using LittleConqueror.AppService.Domain.DrivingModels.Queries;
using LittleConqueror.AppService.Domain.Models.Entities;
using LittleConqueror.AppService.DrivenPorts;
using LittleConqueror.AppService.Exceptions;

namespace LittleConqueror.AppService.Domain.Handlers.TerritoryHandlers;

public interface IGetTerritoryByUserIdHandler
{
    public Task<Territory> Handle(GetTerritoryByUserIdQuery query);
}
public class GetTerritoryByUserIdHandler(ITerritoryDatabasePort territoryDatabase) : IGetTerritoryByUserIdHandler
{
    public async Task<Territory> Handle(GetTerritoryByUserIdQuery query)
        => await territoryDatabase.GetTerritoryOfUser(query.UserId) ??
                        throw new AppException("Territory not found", 404);
    
}