using LittleConqueror.AppService.Domain.DrivingModels.Queries;
using LittleConqueror.AppService.Domain.Models.Entities;
using LittleConqueror.AppService.DrivenPorts;
using LittleConqueror.AppService.Exceptions;

namespace LittleConqueror.AppService.Domain.Handlers.ResourcesHandlers;

public interface IGetPopulationHappinessForUserIdHandler
{
    Task<double> Handle(GetPopulationHappinessForUserIdQuery query);
}

public class GetPopulationHappinessForUserIdHandler(
    IGetResourcesForUserHandler getResourcesForUserHandler,
    IPopulationHappinessConfigsDatabasePort populationHappinessConfigsDatabase) : IGetPopulationHappinessForUserIdHandler
{
    public async Task<double> Handle(GetPopulationHappinessForUserIdQuery query)
    {
        var resources = await getResourcesForUserHandler.Handle(new GetResourcesForUserQuery
        {
            UserId = query.UserId
        });
        
        var result = 0.0;

        var happinessConfig = await populationHappinessConfigsDatabase.GetAllPopHappinessConfigs();
        
        // each resource happiness
        foreach (var resourceId in Enum.GetValues<ResourceType>())
        {
            result += CalculateResourceHappiness(
                resources.GetData(resourceId),
                happinessConfig.First(config => config.Type == resourceId).Coef);
        }
        
        return result;
    }
    
    private double CalculateHappinessPourcentage(double used, double max, double happiness)
    {
        return used / max * happiness;
    }
    
    private double CalculateResourceHappiness(
        IReadOnlyDictionary<ResourceDetailsType, Dictionary<string, int>> resourceData, 
        double resourceHappiness)
    {
        var resourceUsed = resourceData.GetValueOrDefault(ResourceDetailsType.Used)
            ?? throw new AppException("Resource used not found", 404);
        resourceUsed.TryGetValue("Total", out var resourceUsedTotal);
        
        var resourceProduction = resourceData.GetValueOrDefault(ResourceDetailsType.Production)
            ?? throw new AppException("Resource production not found", 404);
        resourceProduction.TryGetValue("Available", out var resourceAvailable);
        
        return CalculateHappinessPourcentage(resourceAvailable, resourceUsedTotal, resourceHappiness);
    }
}