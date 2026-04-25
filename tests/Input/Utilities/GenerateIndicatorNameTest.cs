// tests/Input/Utilities/GenerateIndicatorNameTest.cs
using Sonyachna_Data_Forge.Input;
namespace Sonyachna_Data_Forge.Tests.Input;
public class GenerateIndicatorNameTest
{
    [Theory]
    [InlineData("SMA", "period=14,source=close", "sma_14_close")]
    [InlineData("EMA", "source=open,period=20", "ema_20_open")]
    [InlineData("RSI", "name=My RSI,period=14", "MyRSI")]
    [InlineData("MACD", "", "macd")]
    [InlineData("ADX", "name=,period=14", "adx_14")]
    [InlineData("ADX", "name=   ,period=14", "adx_14")]
    [InlineData("ADX", "name=My ADX,period=14", "MyADX")]
    public void GenerateIndicatorName_ValidInput_ReturnsExpectedName(string type, string parametersExpression, string expected)
    {
        var result = Utilities.GenerateIndicatorName(type, parametersExpression);
        Assert.Equal(expected, result); 

    }
}