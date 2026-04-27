// src/Core/DataEngineer/IDataEngineer.cs
using Sonyachna_Data_Forge.Domain;
namespace Sonyachna_Data_Forge.Core;
public interface IDataEngineer
{
    List<ProcessedDataRow> ProcessData(List<FusedDataRow> fusedData, List<IndicatorDefinition> indicatorDefinitions);
}