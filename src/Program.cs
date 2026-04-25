using Sonyachna_Data_Forge.Infrastructure.Logging;
using Sonyachna_Data_Forge.Domain;

using Microsoft.Extensions.Logging;
using Sonyachna_Data_Forge.Service;
using System.Threading.Tasks;
namespace Sonyachna_Data_Forge;
class Program
{
    static async Task Main(string[] args)
    {
        var request = new Request
        {
            Ticker = PrimaryTicker.EURUSD,
            Timeframe = Timeframe.D,
            StartDate = new DateTime(2020, 1, 1),
            EndDate = new DateTime(2020, 12, 31),
            OutputFormat = OutputFormat.Csv,
            LogLevel = Domain.LogLevel.DevMode
            
        };

        using var loggerFactory = LoggerFactoryProvider.Create(request.LogLevel);
        var logger = loggerFactory.CreateLogger<Program>();
        logger.LogError("This is an error message indicating a critical issue. \n");
        logger.LogInformation("This is an informational message about the application's normal operation. \n"); 
        logger.LogDebug("This is a debug message for development purposes. \n");
        logger.LogTrace("This is a trace message for detailed debugging. \n");
        logger.LogWarning("This is a warning message indicating a potential issue. \n");
        logger.LogCritical("This is a critical message indicating a severe failure. \n");

        var godRunner = new GodRunner(loggerFactory.CreateLogger<GodRunner>());
        var result = await godRunner.RunAsync(request);

    }
}