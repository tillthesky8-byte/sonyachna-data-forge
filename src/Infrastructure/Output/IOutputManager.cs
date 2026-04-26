// src/Infrastructure/Output/IOutputManager.cs

using Sonyachna_Data_Forge.Domain;

namespace Sonyachna_Data_Forge.Infrastructure.Output;
public interface IOutputManager
{
    Response WriteAsync(List<ProcessedDataRow> processedData, OutputFormat outputFormat, string? outputPath);
    Response WriteCsvAsync(List<ProcessedDataRow> processedData, string outputPath);
    Response WriteInMemoryAsync(List<ProcessedDataRow> processedData);
}