using ActionEntities = LittleConqueror.AppService.Domain.Models.Entities.ActionEntities;
using LittleConqueror.AppService.DrivenPorts;
using LittleConqueror.Infrastructure.Repositories;

namespace LittleConqueror.Infrastructure.DatabaseAdapters;

public class ActionAgricoleDatabaseAdapter(
    ActionAgricoleRepository actionAgricoleRepository
    ) : IActionAgricoleDatabasePort
{
    public async Task DeleteActionAgricole(ActionEntities.Agricole actionAgricole)
    {
        await actionAgricoleRepository.RemoveAsync(actionAgricole);
    }
}