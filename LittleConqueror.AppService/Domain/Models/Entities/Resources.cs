using LittleConqueror.AppService.Domain.Models.Entities.Base;

namespace LittleConqueror.AppService.Domain.Models.Entities;

public class Resources : Entity
{
    public int Food { get; set; }
    public int Wood { get; set; }
    public int Stone { get; set; }
    public int Iron { get; set; }
    public int Gold { get; set; }
    public int Diamond { get; set; }
    public int Petrol { get; set; }
    
    // 1:1 relationship
    public long UserId { get; set; }
    public User User { get; set; }
    
    public static Resources CreateDefaultResources(long userId)
        => new()
        {
            UserId = userId,
            Food = 2000,
            Wood = 1000,
            Stone = 1000,
            Iron = 0,
            Gold = 0,
            Diamond = 0,
            Petrol = 0
        };
}

public enum ResourceType
{
    Food,
    Wood,
    Stone,
    Iron,
    Gold,
    Diamond,
    Petrol
}