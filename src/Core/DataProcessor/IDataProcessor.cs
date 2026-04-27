// src/Core/DataProcessor/IDataProcessor.cs
using Sonyachna_Data_Forge.Domain;

namespace Sonyachna_Data_Forge.Core;

public interface IDataProcessor
{
    List<ProcessedDataRow> ProcessData(List<OhlcDataRow> primaryData, Dictionary<string, List<ExternalDataRow>> externalDataDict, List<IndicatorDefinition> indicatorDefinitions);
}
