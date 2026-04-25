// tests/Input/Utilities/ParseIndicatorDefinitionTests.cs
using Sonyachna_Data_Forge.Domain;
using Sonyachna_Data_Forge.Input;

namespace Sonyachna_Data_Forge.Tests.Input;
public class ParseIndicatorDefinitionTests
{
    public static IEnumerable<object[]> GetTestCases()
    {
        yield return new object[] { "sma:period=14,source=close", "sma", "sma_14_close", 14, "close" };
        yield return new object[] { "ema:source=open,period=20", "ema", "ema_20_open", 20, "open" };
        yield return new object[] { "rsi:name=My RSI,period = 1 4", "rsi", "MyRSI", 14, null };
        yield return new object[] { "macd", "macd", "macd", null, null };
        yield return new object[] { "adx:name=,period=14", "adx", "adx_14", 14, null };
        yield return new object[] {"adx:name=   ,period=14", "adx", "adx_14", 14, null };
        yield return new object[] { "adx:name=My ADX,period=14", "adx", "MyADX", 14, null };
    }
    [Theory]
    [MemberData(nameof(GetTestCases))]
    public void ParseIndicatorDefinition_ValidInput_ReturnsExpectedIndicatorDefinition(string expression, string expectedType, string expectedName, int? expectedPeriod, string? expectedSource)
    {
        var result = Utilities.ParseIndicatorDefinition(expression);
        Assert.Equal(expectedType, result.Type.ToString(), StringComparer.OrdinalIgnoreCase);
        Assert.Equal(expectedName, result.Name, StringComparer.OrdinalIgnoreCase);

        if (expectedPeriod.HasValue)
            Assert.Equal(expectedPeriod.Value, Convert.ToInt32(result.Parameters["period"]));
        else
            Assert.False(result.Parameters.ContainsKey("period"));
        
        if (expectedSource != null)
            Assert.Equal(expectedSource, result.Parameters["source"].ToString(), StringComparer.OrdinalIgnoreCase);
        else
            Assert.False(result.Parameters.ContainsKey("source"));
    }
}