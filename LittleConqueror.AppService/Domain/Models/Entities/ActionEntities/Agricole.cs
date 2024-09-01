using LittleConqueror.AppService.Domain.Logic;
using LittleConqueror.AppService.Domain.Logic.ActionsHelpers;

namespace LittleConqueror.AppService.Domain.Models.Entities.ActionEntities;

public class Agricole : Action
{
    public double AgriculturalFertility
    {
        get
        {
            var baseFertility = GeoProceduralConfigs.BaseFertility;
            return AgricoleExpressions.GetAgriculturalFertilityExpression(baseFertility).Compile().Invoke(City);
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