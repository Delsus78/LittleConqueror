using ActionEntities = LittleConqueror.AppService.Domain.Models.Entities.ActionEntities;
namespace LittleConqueror.AppService.DrivenPorts;

public interface IActionDatabasePort
{
    Task DeleteAction(ActionEntities.Action action);
    Task AddAction(ActionEntities.Action action);
    Task<(int total, List<ActionEntities.Action> actions)> GetPaginatedActionsByUserId(long userId, int skip, int take);
}