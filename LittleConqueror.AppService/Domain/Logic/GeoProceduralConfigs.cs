namespace LittleConqueror.AppService.Domain.Logic;

public static class GeoProceduralConfigs
{
    public static double BaseTemperature { get; set; } = 15.0;
    public static double TemperatureVariance { get; set; } = 10.0;
    public static double IdealTemperature { get; set; } = 22.0;
    public static double MaxTemperatureVariance { get; set; } = 40.0;

    public static double BaseFertility { get; set; } = 1;
    public static double FertilityVariance { get; set; } = 0.2;

    public static double StoneFactor { get; set; } = 0.7;
    public static double IronFactor { get; set; } = 0.5;
    public static double GoldFactor { get; set; } = 0.2;
    public static double DiamondFactor { get; set; } = 0.05;
    public static double OilFactor { get; set; } = 0.3;

    public static double ViabilityVariance { get; set; } = 0.1;
}