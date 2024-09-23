using LittleConqueror.AppService.Domain.DrivingModels.Queries;
using LittleConqueror.AppService.Domain.Handlers.ResourcesHandlers;
using LittleConqueror.AppService.Domain.Models;
using LittleConqueror.AppService.Domain.Models.Entities;
using LittleConqueror.AppService.DrivenPorts;

namespace LittleConqueror.AppService.Domain.Strategies.ResourceDetailsStrategies.Get;

public class GetFoodResourceDetailsStrategy(IActionDatabasePort actionDatabase) : IGetResourceDetailsStrategy
{
    public async Task<Dictionary<ResourceDetailsType, Dictionary<string, int>>> Execute(GetResourceDetailsStrategyParams input, CancellationToken cancellationToken)
    {
        var result = new Dictionary<ResourceDetailsType, Dictionary<string, int>>();
        
        var usedDict = new Dictionary<string, int>();
        var productionDict = new Dictionary<string, int>();
        
        var totalFoodUsed = 0;
        foreach (var actionType in Enum.GetValues<ActionType>())
        {
            var usedFood = await actionDatabase.ComputeUsed(ResourceType.Food, input.UserId, actionType);
            usedDict.Add(actionType.ToString(), usedFood);
            totalFoodUsed += usedFood;
        }
        usedDict.Add("Total", totalFoodUsed);
        
        
        // production food
        productionDict.Add("Production", await actionDatabase.ComputeTotal(ResourceType.Food, input.UserId));
        productionDict.Add("Available", productionDict["Production"] - totalFoodUsed);

        
        result.Add(ResourceDetailsType.Production, productionDict);
        result.Add(ResourceDetailsType.Used, usedDict);
        
        return result;
    }
}