// src/Infrastructure/Repository/SqliteRepository.cs

using Sonyachna_Data_Forge.Domain;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Logging;

namespace Sonyachna_Data_Forge.Infrastructure.Repository;
public class SqliteRepository : IRepository
{
    private readonly string _connectionString;
    private readonly ILogger<SqliteRepository> _logger;
    
    public SqliteRepository(string connectionString, ILogger<SqliteRepository> logger)
    {
        _connectionString = connectionString;
        _logger = logger;
    }

    public async Task<List<OhlcDataRow>> LoadPrimaryAsync(PrimaryTicker ticker, Timeframe timeframe, DateTime startDate, DateTime endDate)
    {
        _logger.LogDebug("\n Loading primary data for {Ticker} at {Timeframe} from {StartDate} to {EndDate}", ticker, timeframe, startDate, endDate);
        string query;
        if (timeframe != Timeframe.ANY)
            query = @"SELECT timestamp, open, high, low, close, volume, spread FROM ohlcv 
                          WHERE ticker = @Ticker AND timeframe = @Timeframe AND timestamp BETWEEN @StartDate AND @EndDate
                          ORDER BY timestamp ASC";

        else 
            query = @"SELECT timestamp, open, high, low, close, volume, spread FROM ohlcv 
                          WHERE ticker = @Ticker AND timestamp BETWEEN @StartDate AND @EndDate
                          ORDER BY timestamp ASC";

        var data = new List<OhlcDataRow>();
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();
        using var command = new SqliteCommand(query, connection);
        command.Parameters.AddWithValue("@Ticker", ticker.ToString());
        command.Parameters.AddWithValue("@Timeframe", timeframe.ToString());
        command.Parameters.AddWithValue("@StartDate", startDate);
        command.Parameters.AddWithValue("@EndDate", endDate);
        using var reader = await command.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            data.Add(new OhlcDataRow
            {
                Timestamp = reader.GetDateTime(0),
                Open = reader.GetDecimal(1),
                High = reader.GetDecimal(2),
                Low = reader.GetDecimal(3),
                Close = reader.GetDecimal(4),
                Volume = reader.GetInt64(5),
                Spread = reader.GetDecimal(6)
            });
        };
        _logger.LogDebug("\n Loaded {Count} primary data rows for {Ticker} at {Timeframe}", data.Count, ticker, timeframe);
        return data;
    }
    public async Task<List<ExternalDataRow>> LoadExternalAsync(ExternalTicker ticker, Timeframe timeframe, DateTime startDate, DateTime endDate)
    {
        _logger.LogDebug("\n Loading external data for {Ticker} at {Timeframe} from {StartDate} to {EndDate}", ticker, timeframe, startDate, endDate);
        string query;
        if (timeframe != Timeframe.ANY)
            query = @"SELECT timestamp, value FROM external
                          WHERE ticker = @Ticker AND timeframe = @Timeframe AND timestamp BETWEEN @StartDate AND @EndDate
                          ORDER BY timestamp ASC";

        else 
            query = @"SELECT timestamp, value FROM external
                          WHERE ticker = @Ticker AND timestamp BETWEEN @StartDate AND @EndDate
                          ORDER BY timestamp ASC";

        var data = new List<ExternalDataRow>();
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();
        using var command = new SqliteCommand(query, connection);
        command.Parameters.AddWithValue("@Ticker", ticker.ToString());
        command.Parameters.AddWithValue("@Timeframe", timeframe.ToString());
        command.Parameters.AddWithValue("@StartDate", startDate);
        command.Parameters.AddWithValue("@EndDate", endDate);
        using var reader = await command.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            data.Add(new ExternalDataRow
            {
                Timestamp = reader.GetDateTime(0),
                Value = reader.GetDecimal(1)
            });
        };
        _logger.LogDebug("\n Loaded {Count} external data rows for {Ticker} at {Timeframe}", data.Count, ticker, timeframe);
        return data;
    }
}
