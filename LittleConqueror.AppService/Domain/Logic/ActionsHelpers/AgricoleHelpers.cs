using LittleConqueror.AppService.Domain.Models.Entities;

namespace LittleConqueror.AppService.Domain.Logic.ActionsHelpers;

public static class AgricoleExpressions
{
    public static double GetAgriculturalFertilityExpression(this City city, double baseFertility)
    {
        return Math.Round(MathHelpers.GetRandomPourcentageBasedOnLatLong(city.Latitude, city.Longitude) * 0.01
                       * baseFertility, 2);
    }

    public static int GetFoodProductionExpression(City city) 
        => (int) (city.AgriculturalFertility * city.Population);
}