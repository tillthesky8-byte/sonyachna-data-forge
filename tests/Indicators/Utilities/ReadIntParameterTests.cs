// tests/Indicators/Utilities/ReadIntParameterTests.cs

using Sonyachna_Data_Forge.Indicators;

namespace Sonyachna_Data_Forge.Tests.Indicators.Utilities;
public class ReadIntParameterTests
{
    [Fact]
    public void ReadIntParameter_ReturnsValue_WhenKeyExistsAndIsInt()
    {        var parameters = new Dictionary<string, object>
        {
            { "period", 14 }
        };
        int result = Sonyachna_Data_Forge.Indicators.Utilities.ReadIntParameter(parameters, "period", 10, 1, 100);
        Assert.Equal(14, result);
    }


    [Fact]
    public void ReadIntParameter_ReturnsDefaultValue_WhenKeyDoesNotExist()
    {
        var parameters = new Dictionary<string, object>();
        int result = Sonyachna_Data_Forge.Indicators.Utilities.ReadIntParameter(parameters, "period", 10, 1, 100);
        Assert.Equal(10, result);
    }

    [Fact]
    public void ReadIntParameter_ReturnsDefaultValue_WhenKeyExistsButIsNotInt()
    {
        var parameters = new Dictionary<string, object>
        {
            { "period", "not an int" }
        };
        int result = Sonyachna_Data_Forge.Indicators.Utilities.ReadIntParameter(parameters, "period", 10, 1, 100);
        Assert.Equal(10, result);
    }

    [Fact]
    public void ReadIntParameter_ReturnsMinValue_WhenValueIsLessThanMin()
    {
        var parameters = new Dictionary<string, object>
        {
            { "period", 0 }
        };
        int result = Sonyachna_Data_Forge.Indicators.Utilities.ReadIntParameter(parameters, "period", 10, 1, 100);
        Assert.Equal(1, result);
    }

    [Fact]
    public void ReadIntParameter_ReturnsMaxValue_WhenValueIsGreaterThanMax()
    {
        var parameters = new Dictionary<string, object>
        {
            { "period", 101 }
        };
        int result = Sonyachna_Data_Forge.Indicators.Utilities.ReadIntParameter(parameters, "period", 10, 1, 100);
        Assert.Equal(100, result);
    }
}
