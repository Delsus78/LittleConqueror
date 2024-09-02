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
    IGetResourceDetailsHandler getResourceDetailsHandler,
    IUserContext userContext) : IGetResourcesForUserHandler
{
    private static readonly Dictionary<ResourceType, Action<Resources,  Dictionary<ResourceDetailsType,Dictionary<string,int>>>> ResourceMap = new()
    {
        { ResourceType.Food, (resources, data) => resources.FoodData = data },
        { ResourceType.Wood, (resources, data) => resources.WoodData = data },
        { ResourceType.Stone, (resources, data) => resources.StoneData = data },
        { ResourceType.Iron, (resources, data) => resources.IronData = data },
        { ResourceType.Gold, (resources, data) => resources.GoldData = data },
        { ResourceType.Diamond, (resources, data) => resources.DiamondData = data },
        { ResourceType.Petrol, (resources, data) => resources.PetrolData = data },
        { ResourceType.ResearchPoints, (resources, data) => resources.ResearchPointsData = data }
    };
    
    public async Task<Resources> Handle(GetResourcesForUserQuery query)
    {
        if (query.UserId != userContext.UserId)
            throw new AppException("You are not authorized to access this resource", 403);
        
        var resources = await resourcesDatabase.GetResourcesOfUser(query.UserId) ??
                          throw new AppException("Resources not found", 404);
        
        
        foreach (var resourceType in ResourceMap.Keys)
        {
            var resourceData = await getResourceDetailsHandler.Handle(new GetResourceDetailsQuery
            {
                UserId = query.UserId,
                ResourceType = resourceType
            });

            // Map resource data to the appropriate property
            ResourceMap[resourceType](resources, resourceData);
        }
        
        return resources;
    }
}