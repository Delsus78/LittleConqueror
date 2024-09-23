using LittleConqueror.AppService.Domain.DrivingModels.Queries;
using LittleConqueror.AppService.Domain.Handlers.ResourcesHandlers;
using LittleConqueror.AppService.Domain.Models;
using LittleConqueror.AppService.Domain.Models.Entities;
using LittleConqueror.AppService.DrivenPorts;

namespace LittleConqueror.AppService.Domain.Strategies.ResourceDetailsStrategies.Get;

public class GetDiamondResourceDetailsStrategy(IActionDatabasePort actionDatabase) : IGetResourceDetailsStrategy
{
    public async Task<Dictionary<ResourceDetailsType, Dictionary<string, int>>> Execute(GetResourceDetailsStrategyParams input, CancellationToken cancellationToken)
    {
        var result = new Dictionary<ResourceDetailsType, Dictionary<string, int>>();
        
        var usedDict = new Dictionary<string, int>();
        var productionDict = new Dictionary<string, int>();
        
        var totalDiamondUsed = 0;
        
        foreach (var actionType in Enum.GetValues<ActionType>())
        {
            var usedDiamond = await actionDatabase.ComputeUsed(ResourceType.Diamond, input.UserId, actionType);
            usedDict.Add(actionType.ToString(), usedDiamond);
            totalDiamondUsed += usedDiamond;
        }
        usedDict.Add("Total", totalDiamondUsed);
        
        
        // production food
        productionDict.Add("Production", await actionDatabase.ComputeTotal(ResourceType.Diamond, input.UserId));
        productionDict.Add("Available", productionDict["Production"] - totalDiamondUsed);

        
        result.Add(ResourceDetailsType.Production, productionDict);
        result.Add(ResourceDetailsType.Used, usedDict);
        
        return result;
    }
}