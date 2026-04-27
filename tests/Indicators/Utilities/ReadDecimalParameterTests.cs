// tests/Indicators/Utilities/ReadDecimalParameterTests.cs
using Sonyachna_Data_Forge.Indicators;
namespace Sonyachna_Data_Forge.Tests.Indicators.Utilities;
public class ReadDecimalParameterTests
{
    [Fact]
    public void ReadDecimalParameter_ReturnsValue_WhenKeyExistsAndIsDecimal()
    {
        var parameters = new Dictionary<string, object>
        {
            { "threshold", 0.5m }
        };
        decimal result = Sonyachna_Data_Forge.Indicators.Utilities.ReadDecimalParameter(parameters, "threshold", 0.1m, 0.0m, 1.0m);
        Assert.Equal(0.5m, result);
    }

    [Fact]
    public void ReadDecimalParameter_ReturnsDefaultValue_WhenKeyDoesNotExist()
    {
        var parameters = new Dictionary<string, object>();
        decimal result = Sonyachna_Data_Forge.Indicators.Utilities.ReadDecimalParameter(parameters, "threshold", 0.1m, 0.0m, 1.0m);
        Assert.Equal(0.1m, result);
    }

    [Fact]
    public void ReadDecimalParameter_ReturnsDefaultValue_WhenKeyExistsButIsNotDecimal()
    {
        var parameters = new Dictionary<string, object>
        {
            { "threshold", "not a decimal" }
        };
        decimal result = Sonyachna_Data_Forge.Indicators.Utilities.ReadDecimalParameter(parameters, "threshold", 0.1m, 0.0m, 1.0m);
        Assert.Equal(0.1m, result);
    }

    [Fact]
    public void ReadDecimalParameter_ReturnsMinValue_WhenValueIsLessThanMin()
    {
        var parameters = new Dictionary<string, object>
        {
            { "threshold", -0.1m }
        };
        decimal result = Sonyachna_Data_Forge.Indicators.Utilities.ReadDecimalParameter(parameters, "threshold", 0.1m, 0.0m, 1.0m);
        Assert.Equal(0.0m, result);
    }

    [Fact]
    public void ReadDecimalParameter_ReturnsMaxValue_WhenValueIsGreaterThanMax()
    {
        var parameters = new Dictionary<string, object>
        {
            { "threshold", 1.1m }
        };
        decimal result = Sonyachna_Data_Forge.Indicators.Utilities.ReadDecimalParameter(parameters, "threshold", 0.1m, 0.0m, 1.0m);
        Assert.Equal(1.0m, result);
    }
}