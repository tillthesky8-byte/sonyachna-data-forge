// Input/Utilities.cs
namespace Sonyachna_Data_Forge.Input;
public static class Utilities
{
    public static void ValidateAndParseEnum<TEnum>(string input, out TEnum result) where TEnum : struct, Enum
    {
        var trimmedInput = input.Replace(" ", "").Trim();
        if (!Enum.TryParse<TEnum>(trimmedInput, true, out result))
            throw new ArgumentException($"Invalid value for enum {typeof(TEnum).Name}: {trimmedInput}");
        
    }

    public static void ValidateAndParseDateTime(string rawValue, string optionName, out DateTime result)
    {
        if (!DateTime.TryParse(rawValue, out result))
            throw new ArgumentException($"Invalid date format for option {optionName}: {rawValue}");
    }


    // expected expression format: "param1=value1,param2=value2
    public static Dictionary<string, object> ParseParametersFromExpression(string expression)
    {
        var trimmedExpression = expression.Replace(" ", "").Trim();

        var parameters = new Dictionary<string, object>();
        if (string.IsNullOrEmpty(trimmedExpression))
            return parameters;

        var pairs = trimmedExpression.Split(',');
        foreach (var pair in pairs)
        {
            var keyValue = pair.Split('=', 2);
            if (keyValue.Length == 2)
            {
                var key = keyValue[0].Replace(" ", "").Trim();
                var value = keyValue[1].Replace(" ", "").Trim();
                parameters[key] = value;
            }
        }

        return parameters;
    }

    public static string GenerateIndicatorName(string type, string expression)
    {
        var parameters = ParseParametersFromExpression(expression);
        
        if (parameters == null || parameters.Count == 0) 
            return type.Replace(" ", "").Trim().ToLowerInvariant();

        if (parameters.TryGetValue("name", out var nameValue))
        {
            var nameStr = nameValue.ToString() ?? string.Empty;  
            parameters.Remove("name");  
            if (!string.IsNullOrWhiteSpace(nameStr))
                return nameStr.Replace(" ", "").Trim();
        }

        var prefix = type.Replace(" ", "").Trim().ToLowerInvariant();
        var suffix = string.Join("_", parameters
            .OrderBy(kv => kv.Key)
            .Select(kv => Convert.ToString(kv.Value)?.Replace(" ", "").Trim().ToLowerInvariant() ?? string.Empty));

        return $"{prefix}_{suffix}";

    }

}