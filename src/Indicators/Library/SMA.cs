// src/Indicators/SMA.cs

using System.Globalization;
using Sonyachna_Data_Forge.Domain;
using Sonyachna_Data_Forge.Indicators;
namespace Sonyachna_Data_Forge.Indicators.Library;
public class SMA : IIndicator
{
    IndicatorType IIndicator.Type => IndicatorType.SMA;
    public List<decimal?> Calculate(List<FusedDataRow> fusedDataRows, IndicatorDefinition definition)
    {
        int period = Utilities.ReadIntParameter(definition.Parameters, "period", 14, 1, int.MaxValue);
        string sourceKey = Utilities.ReadStringParameter(definition.Parameters, "source", "close").ToLower().Trim();

        var smaValues = new List<decimal?>(fusedDataRows.Count);
        var window = new Queue<decimal>();
        decimal runningSum = 0m;

        foreach (var row in fusedDataRows)
        {
            decimal? sourceValue = Utilities.ReadAnyRowValue(row, sourceKey);
            if (sourceValue.HasValue)
            {
                window.Enqueue(sourceValue.Value);
                runningSum += sourceValue.Value;
                if (window.Count > period)
                {
                    runningSum -= window.Dequeue();
                }
                smaValues.Add(window.Count == period ? runningSum / period : null);
            }
            else
            {
                smaValues.Add(null);
            }
        }
        return smaValues;
    }
}

