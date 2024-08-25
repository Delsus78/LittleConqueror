namespace LittleConqueror.AppService.Domain.Logic;

public class GeoProceduralConfigs
{
    public double BaseTemperature { get; set; } = 15.0;
    public double TemperatureVariance { get; set; } = 10.0;
    public double IdealTemperature { get; set; } = 22.0;
    public double MaxTemperatureVariance { get; set; } = 40.0;

    public double BaseFertility { get; set; } = 1;
    public double FertilityVariance { get; set; } = 0.2;

    public double StoneFactor { get; set; } = 0.7;
    public double IronFactor { get; set; } = 0.5;
    public double GoldFactor { get; set; } = 0.2;
    public double DiamondFactor { get; set; } = 0.05;
    public double OilFactor { get; set; } = 0.3;

    public double ViabilityVariance { get; set; } = 0.1;
    
    public static double Perturb(double value, double variance, Random random)
    {
        return value + (random.NextDouble() - 0.5) * variance;
    }
    public static double Clamp(double value, double min, double max)
    {
        return Math.Max(min, Math.Min(max, value));
    }
}