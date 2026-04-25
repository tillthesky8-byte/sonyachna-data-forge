// tests/Input/Utilities/ValidateAndParseDateTimeTests.cs
using Sonyachna_Data_Forge.Input;
namespace Sonyachna_Data_Forge.Tests.Input;
public class ValidateAndParseDateTimeTests
{
    [Theory]
    [InlineData("2020-01-01", "StartDate", 2020, 1, 1)]
    [InlineData("12/31/2020", "EndDate", 2020, 12, 31)]
    [InlineData("2020-02-29", "StartDate", 2020, 2, 29)]
    public void ValidateAndParseDateTime_ValidInput_ReturnsExpectedDateTime(string rawValue, string optionName, int expectedYear, int expectedMonth, int expectedDay)
    {
        Utilities.ValidateAndParseDateTime(rawValue, optionName, out var result);
        Assert.Equal(expectedYear, result.Year);
        Assert.Equal(expectedMonth, result.Month);
        Assert.Equal(expectedDay, result.Day);
    } 

    [Theory]
    [InlineData("invalid-date", "StartDate")]
    [InlineData("2020-13-01", "EndDate")]
    [InlineData("2020-02-30", "StartDate")]
    public void ValidateAndParseDateTime_InvalidInput_ThrowsArgumentException(string rawValue, string optionName)
    {
        Assert.Throws<ArgumentException>(() => Utilities.ValidateAndParseDateTime(rawValue, optionName, out var result));
    }
}