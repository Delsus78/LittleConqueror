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
    
    private int? GetFoodProduction()
    {
        if (City == null) return null;
        var fertility = GetAgriculturalFertility();
        
        // round
        return (int) Math.Round((double)(fertility * City.Population));
    }
    
    public int? FoodProduction => GetFoodProduction();
    public double? AgriculturalFertility => GetAgriculturalFertility();
}