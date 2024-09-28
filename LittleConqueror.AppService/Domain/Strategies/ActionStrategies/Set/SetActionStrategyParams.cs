using LittleConqueror.AppService.Domain.Models;
using LittleConqueror.AppService.Domain.Models.Configs;
using LittleConqueror.AppService.Domain.Models.Entities;

namespace LittleConqueror.AppService.Domain.Strategies.ActionStrategies.Set;

public class SetActionStrategyParams
{
    public ActionType ActionType { get; set; }
    public City city { get; set; }
    
    private ResourceType? _resourceType;
    public ResourceType? ResourceType
    {
        get => _resourceType;
        set 
        {
            if (ActionType != ActionType.Miniere)
                throw new ArgumentException("ResourceType can only be set for Miniere action type");
            _resourceType = value;
        }
    }

    private TechResearchCategory? _techResearchCategory;
    public TechResearchCategory? TechResearchCategory
    {
        get => _techResearchCategory;
        set 
        {
            if (ActionType != ActionType.Technologique)
                throw new ArgumentException("TechResearchCategory can only be set for Technologique action type");
            _techResearchCategory = value;
        }
    }
}