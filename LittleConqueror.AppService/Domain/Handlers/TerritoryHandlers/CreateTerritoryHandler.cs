using LittleConqueror.AppService.Domain.DrivingModels.Commands;
using LittleConqueror.AppService.Domain.Models.Entities;
using LittleConqueror.AppService.DrivenPorts;
using LittleConqueror.AppService.Exceptions;

namespace LittleConqueror.AppService.Domain.Handlers.TerritoryHandlers;

public interface ICreateTerritoryHandler
{
    Task<Territory> Handle(CreateTerritoryCommand command);
}
public class CreateTerritoryHandler(
    ITerritoryDatabasePort territoryDatabase) : ICreateTerritoryHandler
{
    public async Task<Territory> Handle(CreateTerritoryCommand command)
    {
        var resultedTerritory = await territoryDatabase.CreateTerritory(new Territory
        {
            OwnerId = command.OwnerId
        });
        if (resultedTerritory == null)
            throw new AppException("Territory creation failed", 500);
        
        return resultedTerritory;
    }
}