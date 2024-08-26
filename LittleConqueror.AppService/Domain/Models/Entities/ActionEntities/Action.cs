using LittleConqueror.AppService.Domain.Models.Entities.Base;

namespace LittleConqueror.AppService.Domain.Models.Entities.ActionEntities;

public abstract class Action : Entity
{
    public DateTime StartTime { get; set; }
    
    // 1:1 relationship
    public City? City { get; set; }
}