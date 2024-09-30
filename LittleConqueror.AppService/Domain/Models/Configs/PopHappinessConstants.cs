using LittleConqueror.AppService.Domain.Models.Entities;
namespace LittleConqueror.AppService.Domain.Models.Configs;

public static class PopHappinessinititialisator
{
    public static Dictionary<ResourceType, double> Values { get; } = new() {
        {ResourceType.Food, 1},
        {ResourceType.Iron, 1},
        {ResourceType.Wood, 1},
        {ResourceType.Stone, 1},
        {ResourceType.Gold, 1},
        {ResourceType.Diamond, 1},
        {ResourceType.Petrol, 1}
    };
}