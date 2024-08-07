using LittleConqueror.AppService.Domain.DrivingModels.Commands;
using LittleConqueror.AppService.Domain.DrivingModels.Queries;
using LittleConqueror.AppService.DrivenPorts;
using LittleConqueror.AppService.Exceptions;

namespace LittleConqueror.AppService.Domain.Handlers.CityHandlers;

public interface IAddCityToATerritoryHandler
{
    Task Handle(AddCityToATerritoryCommand command);
}
public class AddCityToATerritoryHandler(
    ITerritoryDatabasePort territoryDatabase,
    ICityDatabasePort cityDatabase,
    IGetCityByOsmIdHandler getCityByOsmIdHandler): IAddCityToATerritoryHandler
{
    public async Task Handle(AddCityToATerritoryCommand command)
    {
        var city = await getCityByOsmIdHandler.Handle(new GetCityByOsmIdQuery
        {
            OsmId = command.CityId,
            OsmType = command.CityType
        });
        var territory = await territoryDatabase.GetTerritoryById(command.TerritoryId);
        if (territory == null)
            throw new AppException("Territory not found", 404);
        
        await cityDatabase.SetTerritoryId(city.Id, territory.Id);
    }
}