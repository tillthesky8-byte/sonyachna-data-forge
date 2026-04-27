//src/Indicators/Library/RSI.cs

using System.Globalization;
using Sonyachna_Data_Forge.Domain;
using Sonyachna_Data_Forge.Indicators;

namespace Sonyachna_Data_Forge.Indicators.Library;
public class RelativeStrengthIndex : IIndicator
{
    public IndicatorType Type => IndicatorType.RSI;
    public List<decimal?> Calculate(List<FusedDataRow> fusedDataRows, IndicatorDefinition definition)
    {
        int period = Utilities.ReadIntParameter(definition.Parameters, "period", 14, 1, int.MaxValue);
        string sourceKey = Utilities.ReadStringParameter(definition.Parameters, "source", "close").ToLower().Trim();

        var rsiValues = new List<decimal?>(fusedDataRows.Count);
        decimal? prevValue = null;
        decimal gainSum = 0m;
        decimal lossSum = 0m;

        for (int i = 0; i < fusedDataRows.Count; i++)
        {
            decimal? sourceValue = Utilities.ReadAnyRowValue(fusedDataRows[i], sourceKey);
            if (sourceValue.HasValue)
            {
                if (prevValue.HasValue)
                {
                    decimal change = sourceValue.Value - prevValue.Value;
                    gainSum += Math.Max(change, 0);
                    lossSum += Math.Max(-change, 0);
                }
                prevValue = sourceValue;

                if (i >= period)
                {
                    if (i > period)
                    {
                        // Remove the contribution of the value that is sliding out of the window
                        decimal? oldSourceValue = Utilities.ReadAnyRowValue(fusedDataRows[i - period], sourceKey);
                        if (oldSourceValue.HasValue && prevValue.HasValue)
                        {
                            decimal oldChange = oldSourceValue.Value - prevValue.Value;
                            gainSum -= Math.Max(oldChange, 0);
                            lossSum -= Math.Max(-oldChange, 0);
                        }
                    }

                    decimal rs = lossSum == 0 ? decimal.MaxValue : gainSum / lossSum;
                    rsiValues.Add(100 - (100 / (1 + rs)));
                }
                else
                {
                    rsiValues.Add(null);
                }
            }
            else
            {
                rsiValues.Add(null);
            }
        }
        return rsiValues;
    }
}
