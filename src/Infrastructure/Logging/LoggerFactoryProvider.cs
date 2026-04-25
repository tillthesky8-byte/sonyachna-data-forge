// Infrastructure/Logging/LoggerFactoryProvider.cs

using Microsoft.Extensions.Logging;
namespace Sonyachna_Data_Forge.Infrastructure.Logging;
public static class LoggerFactoryProvider
{
    public static ILoggerFactory Create(Domain.LogLevel logLevel)
    {
        var logLevelText = logLevel switch
        {
            Domain.LogLevel.UserMode => "Error",
            Domain.LogLevel.DevMode => "Debug",
            Domain.LogLevel.TraceMode => "Trace",
            _ => "Information"
        };

        var loggerFactory = LoggerFactory.Create(builder =>
        {
            builder
                .SetMinimumLevel(Enum.Parse<Microsoft.Extensions.Logging.LogLevel>(logLevelText))
                .AddSimpleConsole( options =>
                {
                    options.TimestampFormat = "yyyy-MM-dd HH:mm:ss.fff ";
                    options.SingleLine = true;
                });
        });
        return loggerFactory;
    }
}