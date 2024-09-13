using LittleConqueror.AppService.Domain.Models;
using LittleConqueror.AppService.Domain.Models.Entities;
using LittleConqueror.AppService.Domain.Models.TechResearches;
using ActionEntities = LittleConqueror.AppService.Domain.Models.Entities.ActionEntities;
namespace LittleConqueror.AppService.DrivenPorts;

public interface IActionDatabasePort
{
    Task DeleteAction(ActionEntities.Action action);
    Task AddAction(ActionEntities.Action action);
    Task<(int total, List<ActionEntities.Action> actions)> GetPaginatedActionsByUserId(long userId, int skip, int take);
    
    Task<int> ComputeTotal(ResourceType type, long userId);
    Task<int> ComputeUsed(ResourceType type, long userId, ActionType? actionType = null);
    Task<int> ComputeTotalResearch(long userId, TechResearchCategories category);
}