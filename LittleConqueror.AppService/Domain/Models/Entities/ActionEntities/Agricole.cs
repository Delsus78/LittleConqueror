using LittleConqueror.AppService.Domain.Logic;

namespace LittleConqueror.AppService.Domain.Models.Entities.ActionEntities;

public class Agricole : Action
{
    private double? GetAgriculturalFertility()
    {
        if (City == null) return null;
        var config = new GeoProceduralConfigs();
        var random = new Random(City.Latitude.GetHashCode() + City.Longitude.GetHashCode());
        
        var fertility = (1 - Math.Abs(City.Latitude) / 90.0) * config.BaseFertility;
        return GeoProceduralConfigs.Perturb(fertility, config.FertilityVariance, random);
    }
    
    private double? GetFoodProduction()
    {
        if (City == null) return null;
        var fertility = GetAgriculturalFertility();
        return fertility * City.Population;
    }
    
    public double? FoodProduction => GetFoodProduction();
    public double? AgriculturalFertility => GetAgriculturalFertility();
}