using LittleConqueror.AppService.Domain.DrivingModels.Queries;
using LittleConqueror.AppService.Domain.Handlers.ResourcesHandlers;
using LittleConqueror.AppService.Domain.Models;
using LittleConqueror.AppService.Domain.Models.Entities;
using LittleConqueror.AppService.DrivenPorts;

namespace LittleConqueror.AppService.Domain.Strategies.ResourceDetailsStrategies.Get;

public class GetIronResourceDetailsStrategy(IActionDatabasePort actionDatabase) : IGetResourceDetailsStrategy
{
    public async Task<Dictionary<ResourceDetailsType, Dictionary<string, int>>> Execute(GetResourceDetailsQuery input, CancellationToken cancellationToken)
    {
        var result = new Dictionary<ResourceDetailsType, Dictionary<string, int>>();
        
        var usedDict = new Dictionary<string, int>();
        var productionDict = new Dictionary<string, int>();
        
        var totalIronUsed = 0;
        
        foreach (var actionType in Enum.GetValues<ActionType>())
        {
            var usedIron = await actionDatabase.ComputeUsed(ResourceType.Iron, input.UserId, actionType);
            usedDict.Add(actionType.ToString(), usedIron);
            totalIronUsed += usedIron;
        }
        usedDict.Add("Total", totalIronUsed);
        
        
        // production food
        productionDict.Add("Production", await actionDatabase.ComputeTotal(ResourceType.Iron, input.UserId));
        productionDict.Add("Available", productionDict["Production"] - totalIronUsed);

        
        result.Add(ResourceDetailsType.Production, productionDict);
        result.Add(ResourceDetailsType.Used, usedDict);
        
        return result;
    }
}