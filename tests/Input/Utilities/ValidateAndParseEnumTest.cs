// test/Input/Utilities/ValidateAndParseEnumTest.cs
using Sonyachna_Data_Forge.Domain;
using Sonyachna_Data_Forge.Input;
namespace Sonyachna_Data_Forge.Tests.Input;
public class ValidateAndParseEnumTest
{    

    [Theory]
    [InlineData("EURUSD", PrimaryTicker.EURUSD)]
    [InlineData("gbpusd", PrimaryTicker.GBPUSD)]
    [InlineData("BREnToIL", PrimaryTicker.BRENTOIL)]
    [InlineData(" eurusd ", PrimaryTicker.EURUSD)]
    [InlineData("e urus d ", PrimaryTicker.EURUSD)]
    public void ValidateAndParsePrimaryTicker_ValidInput_ReturnsExpectedEnum(string input, PrimaryTicker expected)
    {
        Utilities.ValidateAndParseEnum<PrimaryTicker>(input, out var result);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("asdifasd")]
    [InlineData(" ")]
    public void ValidateAndParsePrimaryTicker_InvalidInput_ThrowsArgumentException(string input)
    {
        Assert.Throws<ArgumentException>(() => Utilities.ValidateAndParseEnum<PrimaryTicker>(input, out _));
    }   


}