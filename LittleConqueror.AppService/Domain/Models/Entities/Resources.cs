using System.ComponentModel.DataAnnotations.Schema;
using LittleConqueror.AppService.Domain.Handlers.ResourcesHandlers;
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
    
    // not mapped to the database
    [NotMapped]
    public Dictionary<ResourceDetailsType, Dictionary<string, int>> FoodData { get; set; } = new();
    [NotMapped]
    public Dictionary<ResourceDetailsType, Dictionary<string, int>> WoodData { get; set; } = new();
    [NotMapped]
    public Dictionary<ResourceDetailsType, Dictionary<string, int>> StoneData { get; set; } = new();
    [NotMapped]
    public Dictionary<ResourceDetailsType, Dictionary<string, int>> IronData { get; set; } = new();
    [NotMapped]
    public Dictionary<ResourceDetailsType, Dictionary<string, int>> GoldData { get; set; } = new();
    [NotMapped]
    public Dictionary<ResourceDetailsType, Dictionary<string, int>> DiamondData { get; set; } = new();
    [NotMapped]
    public Dictionary<ResourceDetailsType, Dictionary<string, int>> PetrolData { get; set; } = new();
    
    public Dictionary<ResourceDetailsType, Dictionary<string, int>> GetData(ResourceType type)
    {
        return type switch
        {
            ResourceType.Food => FoodData,
            ResourceType.Wood => WoodData,
            ResourceType.Stone => StoneData,
            ResourceType.Iron => IronData,
            ResourceType.Gold => GoldData,
            ResourceType.Diamond => DiamondData,
            ResourceType.Petrol => PetrolData,
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };
    }
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