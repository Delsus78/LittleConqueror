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
    
    public async Task<int> ComputeTotalFood(long userId)
        => await actionRepository.ComputeTotalFood(userId);
}