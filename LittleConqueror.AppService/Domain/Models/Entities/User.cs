using LittleConqueror.AppService.Domain.Models.Entities.Base;

namespace LittleConqueror.AppService.Domain.Models.Entities;

public class User : Entity
{
    public string Name { get; set; }
    
    // 1:1 relationship
    public Territory? Territory { get; set; }
    public Resources? Resources { get; set; }
    public AuthUser AuthUser { get; set; }
    
    // 1:n relationship
    public List<TechResearch> TechResearches { get; set; }
}