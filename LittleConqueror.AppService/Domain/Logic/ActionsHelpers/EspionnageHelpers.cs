using System.Linq.Expressions;
using ActionEntities = LittleConqueror.AppService.Domain.Models.Entities.ActionEntities;

namespace LittleConqueror.AppService.Domain.Logic.ActionsHelpers;

public static class EspionnageExpressions
{
    public static Expression<Func<ActionEntities.Espionnage, int>> GetFoodPriceExpression()
    {
        return actionEspionnage => actionEspionnage.City.Population;
    }
}
