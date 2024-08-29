using LittleConqueror.AppService.Domain.DrivingModels.Queries;
using LittleConqueror.AppService.Domain.Models.Entities;
using LittleConqueror.AppService.Domain.Services;
using LittleConqueror.AppService.DrivenPorts;
using LittleConqueror.AppService.Exceptions;

namespace LittleConqueror.AppService.Domain.Handlers.ResourcesHandlers;

public interface IGetResourcesForUserHandler
{
    Task<Resources> Handle(GetResourcesForUserQuery query);
}

public class GetResourcesForUserHandler(
    IResourcesDatabasePort resourcesDatabase,
    IActionDatabasePort actionDatabase,
    IUserContext userContext) : IGetResourcesForUserHandler
{
    public async Task<Resources> Handle(GetResourcesForUserQuery query)
    {
        if (query.UserId != userContext.UserId)
            throw new AppException("You are not authorized to access this resource", 403);
        
        var resources = await resourcesDatabase.GetResourcesOfUser(query.UserId) ??
                          throw new AppException("Resources not found", 404);

        resources.Food += await actionDatabase.ComputeTotalFood(query.UserId);
        resources.FoodData.Add("AvailableFood", await actionDatabase.ComputeAvailableFood(query.UserId));

        return resources;
    }
}