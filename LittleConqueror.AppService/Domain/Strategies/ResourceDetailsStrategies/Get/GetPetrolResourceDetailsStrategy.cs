using LittleConqueror.AppService.Domain.DrivingModels.Queries;
using LittleConqueror.AppService.Domain.Handlers.ResourcesHandlers;
using LittleConqueror.AppService.Domain.Models;
using LittleConqueror.AppService.Domain.Models.Entities;
using LittleConqueror.AppService.DrivenPorts;

namespace LittleConqueror.AppService.Domain.Strategies.ResourceDetailsStrategies.Get;

public class GetPetrolResourceDetailsStrategy(IActionDatabasePort actionDatabase) : IGetResourceDetailsStrategy
{
    public async Task<Dictionary<ResourceDetailsType, Dictionary<string, int>>> Execute(GetResourceDetailsStrategyParams input, CancellationToken cancellationToken)
    {
        var result = new Dictionary<ResourceDetailsType, Dictionary<string, int>>();
        
        var usedDict = new Dictionary<string, int>();
        var productionDict = new Dictionary<string, int>();
        
        var totalPetrolUsed = 0;
        
        foreach (var actionType in Enum.GetValues<ActionType>())
        {
            var usedPetrol = await actionDatabase.ComputeUsed(ResourceType.Petrol, input.UserId, actionType);
            usedDict.Add(actionType.ToString(), usedPetrol);
            totalPetrolUsed += usedPetrol;
        }
        usedDict.Add("Total", totalPetrolUsed);
        
        
        // production food
        productionDict.Add("Production", await actionDatabase.ComputeTotal(ResourceType.Petrol, input.UserId));
        productionDict.Add("Available", productionDict["Production"] - totalPetrolUsed);

        
        result.Add(ResourceDetailsType.Production, productionDict);
        result.Add(ResourceDetailsType.Used, usedDict);
        
        return result;
    }
}