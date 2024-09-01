using LittleConqueror.AppService.Domain.Logic;
using LittleConqueror.AppService.Domain.Logic.ActionsHelpers;

namespace LittleConqueror.AppService.Domain.Models.Entities.ActionEntities;

public class Agricole : Action
{
    public double? AgriculturalFertility => City.AgriculturalFertility;

    public int FoodProduction 
        => AgricoleExpressions.GetFoodProductionExpression().Compile().Invoke(this);
}