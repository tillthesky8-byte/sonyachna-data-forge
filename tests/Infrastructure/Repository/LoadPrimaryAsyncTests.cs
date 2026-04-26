// test/Infrastructure/Repository/LoadPrimaryAsyncTests.cs
using Sonyachna_Data_Forge.Domain;
using Sonyachna_Data_Forge.Infrastructure.Repository;
using Sonyachna_Data_Forge.Infrastructure.Logging;
using Microsoft.Extensions.Logging;

namespace Sonyachna_Data_Forge.Tests.Infrastructure.Repository;
public class LoadPrimaryAsyncTests
{
    [Theory]
    [InlineData(PrimaryTicker.EURUSD, Timeframe.D, "2023-01-01", "2023-01-10")]
    [InlineData(PrimaryTicker.EURUSD, Timeframe.D, "1900-01-01", "2100-01-01")]
    [InlineData(PrimaryTicker.EURUSD, Timeframe.ANY, "2023-01-01", "2023-01-10")]
    [InlineData(PrimaryTicker.US100, Timeframe.D, "2023-01-01", "2023-01-10")]
    [InlineData(PrimaryTicker.US100, Timeframe.ANY, "2023-01-01", "2023-01-10")]
    public async Task LoadPrimaryAsync_ValidInput_ReturnsData(PrimaryTicker ticker, Timeframe timeframe, string startDateStr, string endDateStr)
    {
        // Arrange
        using var loggerFactory = LoggerFactoryProvider.Create(Domain.LogLevel.DevMode);
        var repository = new SqliteRepository("Data Source=../../../../market_data.db", loggerFactory.CreateLogger<SqliteRepository>());
        var startDate = DateTime.Parse(startDateStr);
        var endDate = DateTime.Parse(endDateStr);
        var result = await repository.LoadPrimaryAsync(ticker, timeframe, startDate, endDate);
        System.Console.WriteLine($"first row: {result.FirstOrDefault()?.Timestamp}, {result.FirstOrDefault()?.Open}, {result.FirstOrDefault()?.High}, {result.FirstOrDefault()?.Low}, {result.FirstOrDefault()?.Close}, {result.FirstOrDefault()?.Volume}, {result.FirstOrDefault()?.Spread}");
        System.Console.WriteLine($"last row: {result.LastOrDefault()?.Timestamp}, {result.LastOrDefault()?.Open}, {result.LastOrDefault()?.High}, {result.LastOrDefault()?.Low}, {result.LastOrDefault()?.Close}, {result.LastOrDefault()?.Volume}, {result.LastOrDefault()?.Spread}");
        // Assert
        Assert.NotNull(result);
        Assert.All(result, row =>
        {
            Assert.InRange(row.Timestamp, startDate, endDate);
            Assert.True(row.Open > 0);
            Assert.True(row.High > 0);
            Assert.True(row.Low > 0);
            Assert.True(row.Close > 0);
            Assert.True(row.Volume >= 0);
            Assert.True(row.Spread >= 0);
        });
    }
}