using LittleConqueror.AppService.Domain.Logic;

namespace LittleConqueror.AppService.Domain.Models.Entities.ActionEntities;

public class Agricole : Action
{
    public double AgriculturalFertility
    {
        get
        {
            var baseFertility = GeoProceduralConfigs.BaseFertility;
            return AgricoleExpressions.GetAgriculturalFertilityExpression(baseFertility).Compile().Invoke(this);
        }
    }

    public int FoodProduction
    {
        get
        {
            var baseFertility = GeoProceduralConfigs.BaseFertility;
            return AgricoleExpressions.GetFoodProductionExpression(baseFertility).Compile().Invoke(this);
        }
    }
}