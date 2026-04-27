// src/Indicators/Utilities.cs
using Sonyachna_Data_Forge.Domain;
namespace Sonyachna_Data_Forge.Indicators;
public static class Utilities
{

    // utility for reading integer from parameters with default value
    public static int ReadIntParameter(Dictionary<string, object> parameters, string key, int defaultValue, int minValue, int maxValue)
    {
        if (parameters.TryGetValue(key, out var value) && value is int intValue)
        {
            if (intValue < minValue) return minValue;
            if (intValue > maxValue) return maxValue;
            return intValue;
        }
        return defaultValue;
    }

    public static decimal ReadDecimalParameter(Dictionary<string, object> parameters, string key, decimal defaultValue, decimal minValue, decimal maxValue)
    {
        if (parameters.TryGetValue(key, out var value) && value is decimal decimalValue)
        {
            if (decimalValue < minValue) return minValue;
            if (decimalValue > maxValue) return maxValue;
            return decimalValue;
        }
        return defaultValue;
    }

    public static string ReadStringParameter(Dictionary<string, object> parameters, string key, string defaultValue)
    {
        if (parameters.TryGetValue(key, out var value) && value is string strValue)
        {
            return strValue;
        }
        return defaultValue;
    }

    public static bool ReadBoolParameter(Dictionary<string, object> parameters, string key, bool defaultValue)
    {
        if (parameters.TryGetValue(key, out var value) && value is bool boolValue)
        {
            return boolValue;
        }
        return defaultValue;
    }

    public static T ReadEnumParameter<T>(Dictionary<string, object> parameters, string key, T defaultValue) where T : struct
    {
        if (parameters.TryGetValue(key, out var value) && value is string strValue && Enum.TryParse<T>(strValue, true, out var enumValue))
        {
            return enumValue;
        }
        return defaultValue;
    }

    public static decimal? ReadPrimaryRowValue(FusedDataRow dataRow, string key)
    {
        switch (key.ToLower().Trim())
        {
            case "open": return dataRow.Open;
            case "high": return dataRow.High;
            case "low": return dataRow.Low;
            case "close": return dataRow.Close;
            case "volume": return dataRow.Volume;
            case "spread": return dataRow.Spread;
            default: return null;
        };

    }
    public static decimal? ReadExternalRowValue(FusedDataRow dataRow, string key)
    {
        if (dataRow.ExternalValues.TryGetValue(key.Trim(), out var value))
        {
            return value;
        }
        return null;
    }

    public static decimal ReadAnyRowValue(FusedDataRow dataRow, string key)
    {
        var primaryValue = ReadPrimaryRowValue(dataRow, key);
        if (primaryValue.HasValue) return primaryValue.Value;

        var externalValue = ReadExternalRowValue(dataRow, key);
        if (externalValue.HasValue) return externalValue.Value;

        throw new ArgumentException($"Key '{key}' not found in either primary or external values.");
    }

}

