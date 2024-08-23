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
}