using LittleConqueror.AppService.Domain.Models.Entities.Base;

namespace LittleConqueror.AppService.Domain.Models.Entities;

public class Territory : Entity
{

    // 1:n relationship
    public List<City> Cities { get; set; } = new();
    
    // 1:1 relationship
    public long OwnerId { get; set; }
    public User Owner { get; set; }
}