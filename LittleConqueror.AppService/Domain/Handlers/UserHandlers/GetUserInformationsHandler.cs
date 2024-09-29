using LittleConqueror.AppService.Domain.DrivingModels.Queries;
using LittleConqueror.AppService.Domain.Handlers.ResourcesHandlers;
using LittleConqueror.AppService.Domain.Handlers.TerritoryHandlers;
using LittleConqueror.AppService.Domain.Models;

namespace LittleConqueror.AppService.Domain.Handlers.UserHandlers;

public interface IGetUserInformationsHandler
{
    Task<UserInformations?> Handle(GetUserInformationsQuery query);
}
public class GetUserInformationsHandler(
    IGetUserByIdHandler getUserByIdHandler,
    IGetTerritoryByUserIdHandler getTerritoryByUserIdHandler,
    IGetResourcesForUserHandler getResourcesForUserHandler,
    IGetPopulationHappinessForUserIdHandler getPopulationHappinessForUserIdHandler) : IGetUserInformationsHandler
{
    public async Task<UserInformations?> Handle(GetUserInformationsQuery query)
    {
        var user = await getUserByIdHandler.Handle(new GetUserByIdQuery { UserId = query.UserId });
     
        var userInformations = new UserInformations
        {
            Id = user.Id,
            Name = user.Name
        };

        if (query.IncludeTerritory)
        {
            var territory = await getTerritoryByUserIdHandler.Handle(new GetTerritoryByUserIdQuery
                { UserId = query.UserId });
            userInformations.TotalPopulation = territory.Cities.Sum(c => c.Population);
            userInformations.TotalCities = territory.Cities.Count;
        }
        
        if (query.IncludeResources)
        {
            var resources = await getResourcesForUserHandler.Handle(new GetResourcesForUserQuery 
                { UserId = query.UserId });
            userInformations.Resources = resources;
        }
        
        userInformations.TotalHappiness = await getPopulationHappinessForUserIdHandler.Handle(
            new GetPopulationHappinessForUserIdQuery { UserId = query.UserId });

        return userInformations;
    }
}