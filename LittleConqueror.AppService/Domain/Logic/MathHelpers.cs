using System.Numerics;
using System.Security.Cryptography;
using System.Text;

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
    
    public static long StringToLong(string input)
    {
        // Utilisation de SHA-256 pour hacher la chaîne
        using var sha256 = SHA256.Create();
        // Conversion du string en tableau de bytes
        var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
            
        // Extraire les 8 premiers octets du hachage et les convertir en long
        var result = BitConverter.ToInt64(bytes, 0);
            
        return result;
    }
}