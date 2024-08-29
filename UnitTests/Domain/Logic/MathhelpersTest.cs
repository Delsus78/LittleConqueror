using LittleConqueror.AppService.Domain.Logic;
using Xunit.Abstractions;

namespace UnitTests.Domain.Logic;

public class MathhelpersTest(ITestOutputHelper testOutputHelper)
{
    /***
     * Test with a and b random doubles, and difficulty from 1 to 10 (inclusive) 4 times each
     */
    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    [InlineData(6)]
    [InlineData(7)]
    [InlineData(8)]
    [InlineData(9)]
    [InlineData(10)]
    public void GetPipedDiceByRarityAndSeededByABTest(int difficulty)
    {
        for (int i = 0; i < 4; i++)
        {
            // Arrange
            var a = new Random().NextDouble();
            var b = new Random().NextDouble();

            // Act
            var result = MathHelpers.GetPipedDiceByRarityAndSeededByAB(a, b, difficulty);

            // print
            testOutputHelper.WriteLine($"Try n'{i} =>  a: {a}, b: {b}, result: {result}");
        
            // Assert
            Assert.InRange(result, 1, 6);
        }
    }
}