// tests/Utilities/ParseExternalDefinitionTest.cs
using Sonyachna_Data_Forge.Domain;
using Sonyachna_Data_Forge.Input;
namespace Sonyachna_Data_Forge.Tests.Input;
public class ParseExternalDefinitionTest
{    
    [Theory]
    [InlineData("us_interest_rate:D", ExternalTicker.us_interest_rate, Timeframe.D)]
    [InlineData("us_interest_rate:W", ExternalTicker.us_interest_rate, Timeframe.W)]
    [InlineData("us_interest_rate:M", ExternalTicker.us_interest_rate, Timeframe.M)]
    [InlineData("gb_interest_rate", ExternalTicker.gb_interest_rate, Timeframe.ANY)]
    [InlineData("gb_interest_rate:ANY", ExternalTicker.gb_interest_rate, Timeframe.ANY)]
    [InlineData("gb_interest_rate : ANY", ExternalTicker.gb_interest_rate, Timeframe.ANY)]
    [InlineData("gb_ interest_ rate : A NY ", ExternalTicker.gb_interest_rate, Timeframe.ANY)]
    // [InlineData("gb_interest_rate:invalid", ExternalTicker.gb_interest_rate, Timeframe.ANY)] in future.
    public void ParseExternalDefinition_ValidInput_ReturnsExpectedExternalDefinition(string expression, ExternalTicker expectedTicker, Timeframe expectedTimeframe)
    {        
        var result = Utilities.ParseExternalDefinition(expression);
        Assert.Equal(expectedTicker, result.Ticker);
        Assert.Equal(expectedTimeframe, result.Timeframe);
    }

    [Theory]
    [InlineData("invalid_expression")]
    [InlineData("us_interest_rate:invalid_timeframe")]
    public void ParseExternalDefinition_InvalidInput_ThrowsArgumentException(string expression)
    {        
        Assert.Throws<ArgumentException>(() => Utilities.ParseExternalDefinition(expression));
    }
}
