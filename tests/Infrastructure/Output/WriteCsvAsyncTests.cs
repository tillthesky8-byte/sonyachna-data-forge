// test/Infrastructure/Output/WriteCsvAsyncTests.cs
using Sonyachna_Data_Forge.Domain;
using Sonyachna_Data_Forge.Infrastructure.Output;
using Microsoft.Extensions.Logging;
using Sonyachna_Data_Forge.Infrastructure.Logging;
using Xunit;
namespace Sonyachna_Data_Forge.Tests.Infrastructure.Output;
public class WriteCsvAsyncTests
{
    [Fact]
    public void WriteCsvAsync_ShouldWriteDataToCsvFile()
    {        // Arrange
        var loggerFactory = LoggerFactoryProvider.Create(Domain.LogLevel.DevMode);
        var outputManager = new OutputManager(loggerFactory.CreateLogger<OutputManager>());
        var processedData = new List<ProcessedDataRow>
        {
            new ProcessedDataRow
            {
                Timestamp = new DateTime(2023, 1, 1),
                Open = 1.0m,
                High = 1.5m,
                Low = 0.5m,
                Close = 1.2m,
                Volume = 1000,
                Spread = 0.1m,
                ExternalValues = new Dictionary<string, decimal> { { "External1", 0.8m } },
                IndicatorValues = new Dictionary<string, decimal> { { "Indicator1", 0.5m } }
            },
            new ProcessedDataRow
            {
                Timestamp = new DateTime(2023, 1, 2),
                Open = 1.2m,
                High = 1.6m,
                Low = 0.6m,
                Close = 1.3m,
                Volume = 1500,
                Spread = 0.1m,
                ExternalValues = new Dictionary<string, decimal> { { "External1", 0.9m } },
                IndicatorValues = new Dictionary<string, decimal> { { "Indicator1", 0.6m } }
            }
        };
        var outputPath = "../../../test_output.csv";
        // Act
        var response = outputManager.WriteCsvAsync(processedData, outputPath);
        // Assert
        Assert.True(response.Success);
        Assert.True(File.Exists(outputPath));
        var lines = File.ReadAllLines(outputPath);
        Assert.Equal(3, lines.Length); // Header + 2 data rows
        Assert.Equal("timestamp,open,high,low,close,volume,spread,External1,Indicator1", lines[0]);
        Assert.Equal("2023-01-01T00:00:00.0000000,1.0,1.5,0.5,1.2,1000,0.1,0.8,0.5", lines[1]);
        Assert.Equal("2023-01-02T00:00:00.0000000,1.2,1.6,0.6,1.3,1500,0.1,0.9,0.6", lines[2]);
        // Cleanup
        // File.Delete(outputPath);
    }
}