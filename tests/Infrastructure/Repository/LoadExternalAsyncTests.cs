//tests/Infrastructure/Repository/LoadExternalAsyncTests.cs
using Sonyachna_Data_Forge.Domain;
using Sonyachna_Data_Forge.Infrastructure.Repository;
using Sonyachna_Data_Forge.Infrastructure.Logging;
using Microsoft.Extensions.Logging;

namespace Sonyachna_Data_Forge.Tests.Infrastructure.Repository;
public class LoadExternalAsyncTests
{
    [Theory]
    [InlineData(ExternalTicker.us_interest_rate, Timeframe.ANY, "2020-01-01", "2020-12-31")]
    [InlineData(ExternalTicker.gb_interest_rate, Timeframe.ANY, "2020-01-01", "2020-12-31")]
    [InlineData(ExternalTicker.us_interest_rate, Timeframe.IRREGULAR, "2020-01-01", "2020-12-31")]
    [InlineData(ExternalTicker.gb_interest_rate, Timeframe.IRREGULAR, "1900-01-01", "2100-12-31")]
    public async Task LoadExternalAsync_ValidInput_ReturnsData(ExternalTicker ticker, Timeframe timeframe, string startDateStr, string endDateStr)
    {
        // Arrange
        using var loggerFactory = LoggerFactoryProvider.Create(Domain.LogLevel.DevMode);
        var repository = new SqliteRepository("Data Source=../../../../market_data.db", loggerFactory.CreateLogger<SqliteRepository>());
        var startDate = DateTime.Parse(startDateStr);
        var endDate = DateTime.Parse(endDateStr);
        var result = await repository.LoadExternalAsync(ticker, timeframe, startDate, endDate);
        System.Console.WriteLine($"first row: {result.FirstOrDefault()?.Timestamp}, {result.FirstOrDefault()?.Value}");
        System.Console.WriteLine($"last row: {result.LastOrDefault()?.Timestamp}, {result.LastOrDefault()?.Value}");
        // Assert
        Assert.NotNull(result);
        Assert.All(result, row =>
        {
            Assert.InRange(row.Timestamp, startDate, endDate);
            Assert.True(row.Value > 0);
        });
    }
}