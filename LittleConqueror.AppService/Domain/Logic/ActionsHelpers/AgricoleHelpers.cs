using System.Linq.Expressions;
using LittleConqueror.AppService.Domain.Models.Entities;
using ActionEntities = LittleConqueror.AppService.Domain.Models.Entities.ActionEntities;

namespace LittleConqueror.AppService.Domain.Logic.ActionsHelpers;

public static class AgricoleExpressions
{
    public static Expression<Func<City, double>> GetAgriculturalFertilityExpression(double baseFertility)
    {
        return city => Math.Round(MathHelpers.GetRandomPourcentageBasedOnLatLong(city.Latitude, city.Longitude) * 0.01
                       * baseFertility, 2);
    }

    public static Expression<Func<ActionEntities.Agricole, int>> GetFoodProductionExpression()
    {
        return actionAgricole => (int) (actionAgricole.City.AgriculturalFertility * actionAgricole.City.Population);
    }
}