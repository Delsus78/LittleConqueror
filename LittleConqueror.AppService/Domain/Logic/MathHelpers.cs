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

    public static int GetPipedDiceByRarityAndSeededByAB(double latitude, double longitude, int difficulty)
    {
        // Convertir les doubles en longs en les multipliant pour conserver la précision
        var latBits = BitConverter.DoubleToInt64Bits(latitude);
        var lonBits = BitConverter.DoubleToInt64Bits(longitude);

        // Mélange des bits des coordonnées pour générer un nombre pseudo-aléatoire
        var mix = xorShift64(latBits) + long.RotateLeft(xorShift64(lonBits), 32) + 0xCAFEBABE;
        var result = xorShift64(mix);

        // Utiliser la difficulté pour moduler le résultat
        result = (result >> difficulty) & 0xFFFFFFFFL;  // Réduire l'influence de la difficulté

        // Retourner une valeur entre 1 et 6
        return (int)(Math.Abs(result % 6) + 1);
    }
}