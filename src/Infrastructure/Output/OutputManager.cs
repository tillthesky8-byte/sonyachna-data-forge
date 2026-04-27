// infrastructure/Output/OutputManager.cs
using Sonyachna_Data_Forge.Domain;
using Microsoft.Extensions.Logging;
using CsvHelper;
using System.Globalization;
namespace Sonyachna_Data_Forge.Infrastructure.Output;

public class OutputManager : IOutputManager
{
    private readonly ILogger<OutputManager> _logger;
    public OutputManager(ILogger<OutputManager> logger)
    {
        _logger = logger;
    }
    public Response WriteAsync(List<ProcessedDataRow> processedData, OutputFormat outputFormat, string? outputPath)
    {
        return outputFormat switch
        {
            OutputFormat.Csv => WriteCsvAsync(processedData, outputPath ?? $"output_{DateTime.Now:yyyyMMddHHmmss}.csv"),
            OutputFormat.InMemory => WriteInMemoryAsync(processedData),
            _ => new Response { Success = false, Message = $"Unsupported output format: {outputFormat}" }
        };
    }

    public Response WriteCsvAsync(List<ProcessedDataRow> processedData, string outputPath)
    {
        var path = string.IsNullOrEmpty(outputPath) ? $"output_{DateTime.Now:yyyyMMddHHmmss}.csv" : outputPath;
        var externalColumns = processedData.SelectMany(row => row.ExternalValues.Keys).Distinct(StringComparer.OrdinalIgnoreCase).ToList();
        var indicatorColumns = processedData.SelectMany(row => row.IndicatorValues.Keys).Distinct(StringComparer.OrdinalIgnoreCase).ToList();
        var header = new List<string>
        {
            "timestamp",
            "open",
            "high",
            "low",
            "close",
            "volume",
            "spread"
        };
        header.AddRange(externalColumns);
        header.AddRange(indicatorColumns);

        using var writer = new StreamWriter(path);
        writer.WriteLine(string.Join(",", header));
        foreach (var row in processedData)
        {
            var rowValues = new List<string>
            {
                row.Timestamp.ToString("o"),
                row.Open.ToString(CultureInfo.InvariantCulture),
                row.High.ToString(CultureInfo.InvariantCulture),
                row.Low.ToString(CultureInfo.InvariantCulture),
                row.Close.ToString(CultureInfo.InvariantCulture),
                row.Volume.ToString(CultureInfo.InvariantCulture),
                row.Spread.ToString(CultureInfo.InvariantCulture)
            };
            rowValues.AddRange
            (
                externalColumns.Select
                (
                    col => row.ExternalValues.TryGetValue(col, out var val) ? val?.ToString(CultureInfo.InvariantCulture) ?? "" : ""
                )
            );
            rowValues.AddRange
            (
                indicatorColumns.Select
                (
                    col => row.IndicatorValues.TryGetValue(col, out var val) ? val?.ToString(CultureInfo.InvariantCulture) ?? "" : ""
                )
            );
            writer.WriteLine(string.Join(",", rowValues));
        }
        _logger.LogInformation("Data successfully written to CSV at {Path}", path);
        return new Response { Success = true, Message = $"Data written to {path}", Data = processedData};

    }

    public Response WriteInMemoryAsync(List<ProcessedDataRow> processedData)
    {
        _logger.LogInformation("Data successfully stored in memory. Row count: {Count}", processedData.Count);
        return new Response { Success = true, Message = "Data stored in memory", Data = processedData };
    }
}