// tests/Input/Utilities/ParseParametersFromExpressionTest.cs
using Microsoft.VisualBasic;
using Sonyachna_Data_Forge.Input;
namespace Sonyachna_Data_Forge.Tests.Input;
public class ParseParametersFromExpressionTest
{    
    public static IEnumerable<object[]> GetTestCases()
    {
        yield return new object[] { "period=14", new Dictionary<string, object> { { "period", "14" } } };
        yield return new object[] { "period=14,name=My Indicator", new Dictionary<string, object> { { "period", "14" }, { "name", "MyIndicator" } } };
        yield return new object[] { "period=14, name=My Indicator", new Dictionary<string, object> { { "period", "14" }, { "name", "MyIndicator" } } };
        yield return new object[] { " name = My Indicator , period = 14 ", new Dictionary<string, object> { { "name", "MyIndicator" }, { "period", "14" } } };
    }
    [Theory]
    [MemberData(nameof(GetTestCases))]
    public void ParseParametersFromExpression_ValidInput_ReturnsExpectedDictionary(string expression, Dictionary<string, object> expected)
    {
        var result = Utilities.ParseParametersFromExpression(expression);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void ParseParametersFromExpression_EmptyOrNullInput_ReturnsEmptyDictionary(string expression)
    {
        var result = Utilities.ParseParametersFromExpression(expression);
        Assert.Empty(result);   
    }
}