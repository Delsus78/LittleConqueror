using System.Numerics;

namespace LittleConqueror.AppService.Domain.Logic;

public class MathHelpers
{
    private static long xorShift64(long a)
    {
        a ^= (a << 21);
        a ^= (a >> 35);
        a ^= (a << 4);
        return a;
    }

    public static int GetRandomPourcentageBasedOnLatLong(double latitude, double longitude)
    {
        // Convertir les doubles en longs
        var latBits = BitConverter.DoubleToInt64Bits(latitude);
        var lonBits = BitConverter.DoubleToInt64Bits(longitude);

        // Mélange des bits des coordonnées pour générer un nombre pseudo-aléatoire
        var mix = xorShift64(latBits) + (long)BitOperations.RotateLeft((ulong)xorShift64(lonBits), 32) + 0xCAFEBABE;
        var result = xorShift64(mix);

        // Limiter la valeur pour qu'elle soit entre 0 et 100
        return (int)Math.Abs(result % 101);
    }
    
    public static T GetRandomElement<T>(Dictionary<T, int> elements, double latitude, double longitude) where T : notnull
    {
        var result = GetRandomPourcentageBasedOnLatLong(latitude, longitude);
        
        foreach (var (key, value) in elements)
        {
            if (result <= value)
            {
                return key;
            }
        }
        
        throw new InvalidOperationException("The elements dictionary is not valid. Resulted random : " + result);
    }
}