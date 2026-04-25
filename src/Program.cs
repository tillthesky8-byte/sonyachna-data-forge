using Sonyachna_Data_Forge.Infrastructure.Logging;
using Microsoft.Extensions.Logging;
namespace Sonyachna_Data_Forge;
class Program
{
    static void Main(string[] args)
    {
        using var loggerFactory = LoggerFactoryProvider.Create(Domain.LogLevel.DevMode);
        var logger = loggerFactory.CreateLogger<Program>();
        logger.LogError("This is an error message indicating a critical issue.");
        logger.LogInformation("This is an informational message about the application's normal operation."); 
        logger.LogDebug("This is a debug message for development purposes.");
        logger.LogTrace("This is a trace message for detailed debugging.");

    }
}