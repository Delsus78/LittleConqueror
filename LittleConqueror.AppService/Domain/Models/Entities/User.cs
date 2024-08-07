using LittleConqueror.AppService.Domain.Models.Entities.Base;

namespace LittleConqueror.AppService.Domain.Models.Entities;

public class User : Entity
{
    public string Name { get; set; }
    
    public Territory? Territory { get; set; }
    
    public AuthUser AuthUser { get; set; }
}