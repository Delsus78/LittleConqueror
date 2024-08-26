using System.Linq.Expressions;
using ActionEntities = LittleConqueror.AppService.Domain.Models.Entities.ActionEntities;

namespace LittleConqueror.AppService.Domain.Logic;

public static class AgricoleExpressions
{
    public static Expression<Func<ActionEntities.Agricole, double>> GetAgriculturalFertilityExpression(double baseFertility)
    {
        return actionAgricole => 
            Math.Round((1 - Math.Abs(actionAgricole.City.Latitude) / 90.0) * baseFertility, 2);
    }

    public static Expression<Func<ActionEntities.Agricole, int>> GetFoodProductionExpression(double baseFertility)
    {
        return actionAgricole =>
            (int)Math.Round((1 - Math.Abs(actionAgricole.City.Latitude) / 90.0) * baseFertility *
                            actionAgricole.City.Population);
    }
}