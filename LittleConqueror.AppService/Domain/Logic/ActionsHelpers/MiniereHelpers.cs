using System.Linq.Expressions;
using ActionEntities = LittleConqueror.AppService.Domain.Models.Entities.ActionEntities;

namespace LittleConqueror.AppService.Domain.Logic.ActionsHelpers;

public static class MiniereExpressions
{
    public static Expression<Func<ActionEntities.Miniere, int>> GetFoodPriceExpression()
    {
        return actionMiniere => actionMiniere.City.Population;
    }
    
    // public static Expression<Func<ActionEntities.Miniere, ResourceType>> GetActionResourceType(Dictionary<ResourceType, double> probabilities)
    // {
    //     return actionMiniere => 
    // }
}