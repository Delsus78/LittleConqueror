using LittleConqueror.AppService.Domain.DrivingModels.Queries;
using LittleConqueror.AppService.Domain.Models.Entities;
using LittleConqueror.AppService.DrivenPorts;

namespace LittleConqueror.AppService.Domain.Handlers.UserHandlers;

public interface IGetTerritoryByUserIdHandler
{
    public Task<Territory?> Handle(GetTerritoryByUserIdQuery query);
}
public class GetTerritoryByUserIdHandler(ITerritoryDatabasePort territoryDatabase) : IGetTerritoryByUserIdHandler
{
    public async Task<Territory?> Handle(GetTerritoryByUserIdQuery query)
        => await territoryDatabase.GetTerritoryOfUser(query.UserId);
}