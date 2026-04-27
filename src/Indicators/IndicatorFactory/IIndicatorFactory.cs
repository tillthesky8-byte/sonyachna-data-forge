// src/Indicators/IndicatorFactory/IIndicatorFactory.cs
using Sonyachna_Data_Forge.Domain;
namespace Sonyachna_Data_Forge.Indicators.IndicatorFactory;
public interface IIndicatorFactory
{
    IIndicator ResolveIndicator(IndicatorType type);
}