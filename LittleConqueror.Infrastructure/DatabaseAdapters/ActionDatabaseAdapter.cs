using LittleConqueror.AppService.Domain.Models;
using LittleConqueror.AppService.Domain.Models.Entities;
using ActionEntities = LittleConqueror.AppService.Domain.Models.Entities.ActionEntities;
using LittleConqueror.AppService.DrivenPorts;
using LittleConqueror.Infrastructure.Repositories;

namespace LittleConqueror.Infrastructure.DatabaseAdapters;

public class ActionDatabaseAdapter(
    ActionRepository actionRepository
    ) : IActionDatabasePort
{
    public async Task DeleteAction(ActionEntities.Action action) 
        => await actionRepository.RemoveAsync(action);

    public async Task AddAction(ActionEntities.Action action)
        => await actionRepository.CreateAsync(action);

    public async Task<(int total, List<ActionEntities.Action> actions)> GetPaginatedActionsByUserId(long userId, int skip, int take)
    {
        var res = await actionRepository.GetPaginatedActionsByUserId(userId, skip, take);
        return (res.TotalActions, res.Actions.ToList());
    }
    
    public async Task<int> ComputeTotal(ResourceType type, long userId)
        => type switch
        {
            ResourceType.Food => await actionRepository.ComputeTotalFood(userId),
            ResourceType.Wood => await actionRepository.ComputeTotalWood(userId),
            ResourceType.Stone => await actionRepository.ComputeTotalStone(userId),
            ResourceType.Iron => await actionRepository.ComputeTotalIron(userId),
            ResourceType.Gold => await actionRepository.ComputeTotalGold(userId),
            ResourceType.Diamond => await actionRepository.ComputeTotalDiamond(userId),
            ResourceType.Petrol => await actionRepository.ComputeTotalPetrol(userId),
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };
    
    public async Task<int> ComputeUsed(ResourceType type, long userId, ActionType? actionType = null)
        => type switch
        {
            ResourceType.Food => await actionRepository.ComputeUsedFood(userId, actionType),
            ResourceType.Wood => await actionRepository.ComputeUsedWood(userId, actionType),
            ResourceType.Stone => await actionRepository.ComputeUsedStone(userId, actionType),
            ResourceType.Iron => await actionRepository.ComputeUsedIron(userId, actionType),
            ResourceType.Gold => await actionRepository.ComputeUsedGold(userId, actionType),
            ResourceType.Diamond => await actionRepository.ComputeUsedDiamond(userId, actionType),
            ResourceType.Petrol => await actionRepository.ComputeUsedPetrol(userId, actionType),
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };
}