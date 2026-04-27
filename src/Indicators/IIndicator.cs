// src/Indicators/IIndicator.cs
using Sonyachna_Data_Forge.Domain;
namespace Sonyachna_Data_Forge.Indicators;
public interface IIndicator
{
    IndicatorType Type { get; }
    List<decimal?> Calculate(List<FusedDataRow> fusedDataPoints, IndicatorDefinition definition);
}