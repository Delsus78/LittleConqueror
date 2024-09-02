using System.Linq.Expressions;
using ActionEntities = LittleConqueror.AppService.Domain.Models.Entities.ActionEntities;

namespace LittleConqueror.AppService.Domain.Logic.ActionsHelpers;

public static class TechnologiqueExpressions
{
    public static Expression<Func<ActionEntities.Technologique, int>> GetFoodPriceExpression()
    {
        return actionTechnologique => actionTechnologique.City.Population;
    }
    
    public static Expression<Func<ActionEntities.Technologique, int>> GetResearchPointsProductionExpression()
    {
        return actionTechnologique => actionTechnologique.City.Population;
    }
}
