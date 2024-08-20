using LittleConqueror.AppService.Domain.Models.Entities.Base;

namespace LittleConqueror.AppService.Domain.Models.Entities.ActionEntities;

public abstract class Action : Entity
{
    public string getActionName() => GetType().Name;
    public DateTime StartTime { get; set; }
    
    // 1:1 relationship
    public required City City { get; set; }
}