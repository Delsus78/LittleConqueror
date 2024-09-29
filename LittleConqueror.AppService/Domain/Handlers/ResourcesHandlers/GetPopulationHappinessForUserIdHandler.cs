using LittleConqueror.AppService.Domain.DrivingModels.Queries;
using LittleConqueror.AppService.Domain.Models.Configs;
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
        
        // food happiness
        result += CalculateFoodHappiness(resources, happinessConfig.First(config => config.Type == PopHappinessType.Food).Coef);
        
        return result;
    }
    
    private double CalculateHappinessPourcentage(double used, double max, double happiness)
    {
        return used / max * happiness;
    }
    
    private double CalculateFoodHappiness(Resources resources, double foodHappiness)
    {
        var foodUsed = resources.FoodData.GetValueOrDefault(ResourceDetailsType.Used)
            ?? throw new AppException("Food used not found", 404);
        foodUsed.TryGetValue("Total", out var foodUsedTotal);
        
        var foodProduction = resources.FoodData.GetValueOrDefault(ResourceDetailsType.Production)
            ?? throw new AppException("Food production not found", 404);
        foodProduction.TryGetValue("Available", out var foodAvailable);
        
        return CalculateHappinessPourcentage(foodAvailable, foodUsedTotal, foodHappiness);
    }
}