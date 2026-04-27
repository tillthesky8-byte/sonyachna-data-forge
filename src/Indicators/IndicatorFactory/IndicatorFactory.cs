// src/Indicators/IndicatorFactory/IndicatorFactory.cs
using Sonyachna_Data_Forge.Domain;
using Sonyachna_Data_Forge.Indicators.Library;
namespace Sonyachna_Data_Forge.Indicators.IndicatorFactory;
public class IndicatorFactory : IIndicatorFactory
{
    public IIndicator ResolveIndicator(IndicatorType type)
    {        return type switch
        {
            IndicatorType.SMA => new SMA(),
            IndicatorType.RSI => new RSI(),
            _ => throw new NotSupportedException($"Indicator type {type} is yet implemented.")
        };
    }
}

