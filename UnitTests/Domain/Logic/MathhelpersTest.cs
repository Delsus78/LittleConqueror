using LittleConqueror.AppService.Domain.Logic;
using Xunit.Abstractions;

namespace UnitTests.Domain.Logic;

public class MathhelpersTest(ITestOutputHelper _testOutputHelper)
{
    /***
     * Test with a and b random doubles, and difficulty from 1 to 10 (inclusive) 4 times each
     */
    [Fact]
    public void GetPipedDiceByRarityAndSeededByABTest()
    {
        var tries = 1000000;
        var rarity = new Dictionary<int, int>
        {
            {6, 0},
            {5, 10},
            {4, 30},
            {3, 60},
            {2, 90},
            {1, 100}
        };

        var resultedProb = new Dictionary<int, int>
        {
            { 1, 0 },
            { 2, 0 },
            { 3, 0 },
            { 4, 0 },
            { 5, 0 },
            { 6, 0 }
        };
        
        for (int i = 0; i < tries; i++)
        {
            // Arrange
            var a = new Random().NextDouble();
            var b = new Random().NextDouble();

            // Act
            var result = MathHelpers.GetRandomElement(rarity, a, b);

            // print
            resultedProb[result]++;
        
            // Assert
            Assert.InRange(result, 1, 6);
        }
        
        _testOutputHelper.WriteLine("Resulted probabilities:");
        foreach (var (key, value) in resultedProb)
        {
            _testOutputHelper.WriteLine($"{key}: {(double)value / tries * 100}%");
        }
    }
}