using LittleConqueror.AppService.Domain.DrivingModels.Queries;
using LittleConqueror.AppService.Domain.Handlers.ResourcesHandlers;
using LittleConqueror.AppService.Domain.Models;
using LittleConqueror.AppService.Domain.Models.Entities;
using LittleConqueror.AppService.DrivenPorts;

namespace LittleConqueror.AppService.Domain.Strategies.ResourceDetailsStrategies.Get;

public class GetWoodResourceDetailsStrategy(IActionDatabasePort actionDatabase) : IGetResourceDetailsStrategy
{
    public async Task<Dictionary<ResourceDetailsType, Dictionary<string, int>>> Execute(GetResourceDetailsQuery input, CancellationToken cancellationToken)
    {
        var result = new Dictionary<ResourceDetailsType, Dictionary<string, int>>();
        
        var usedDict = new Dictionary<string, int>();
        var productionDict = new Dictionary<string, int>();
        
        var totalWoodUsed = 0;
        foreach (var actionType in Enum.GetValues<ActionType>())
        {
            var usedWood = await actionDatabase.ComputeUsed(ResourceType.Wood, input.UserId, actionType);
            usedDict.Add(actionType.ToString(), usedWood);
            totalWoodUsed += usedWood;
        }
        usedDict.Add("Total", totalWoodUsed);
        
        
        productionDict.Add("Production", await actionDatabase.ComputeTotal(ResourceType.Wood, input.UserId));
        productionDict.Add("Available", productionDict["Production"] - totalWoodUsed);

        
        result.Add(ResourceDetailsType.Production, productionDict);
        result.Add(ResourceDetailsType.Used, usedDict);
        
        return result;
    }
}