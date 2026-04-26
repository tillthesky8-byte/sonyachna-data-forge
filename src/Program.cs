using Sonyachna_Data_Forge.Infrastructure.Logging;
using Sonyachna_Data_Forge.Domain;
using Sonyachna_Data_Forge.Input.Options;

using Microsoft.Extensions.Logging;
using Sonyachna_Data_Forge.Service;

using System.CommandLine;
using Sonyachna_Data_Forge.Input;

namespace Sonyachna_Data_Forge;
class Program
{
    static async Task<int> Main(string[] args)
    {


        RootCommand rootCommand = new RootCommand
        {
            TickerOption.PrimaryTicker, // --ticker, -t
            TimeframeOption.Timeframe, // --timeframe, -tf
            StartDateOption.StartDate, // --start-date, -sd
            EndDateOption.EndDate, // --end-date, -ed
            ExternalExpressionOption.ExternalExpressions, // --external, -e (multiple allowed)
            IndicatorExpressionOption.IndicatorExpressions, // --indicator, -i (multiple allowed)
            OutputFormatOption.OutputFormat, // --output-format, -of
            OutputPathOption.OutputPath, // --output-path, -op
            LogLevelOption.LogLevel // --log-level, -ll
        };

        rootCommand.SetAction(async parseRsult =>
        {
            var request = new Request
            {
                Ticker = parseRsult.GetValue(TickerOption.PrimaryTicker),
                Timeframe = parseRsult.GetValue(TimeframeOption.Timeframe),
                StartDate = parseRsult.GetValue(StartDateOption.StartDate),
                EndDate = parseRsult.GetValue(EndDateOption.EndDate),
                OutputFormat = parseRsult.GetValue(OutputFormatOption.OutputFormat),
                OutputPath = parseRsult.GetValue(OutputPathOption.OutputPath),
                LogLevel = parseRsult.GetValue(LogLevelOption.LogLevel),
                ExternalDefinitions = parseRsult.GetValue(ExternalExpressionOption.ExternalExpressions) ?? new List<ExternalDefinition>(),
                IndicatorDefinitions = parseRsult.GetValue(IndicatorExpressionOption.IndicatorExpressions) ?? new List<IndicatorDefinition>()
            };

            using var loggerFactory = LoggerFactoryProvider.Create(request.LogLevel);
            var logger = loggerFactory.CreateLogger<Program>();

            logger.LogError("This is an error message indicating a critical issue. \n");
            logger.LogInformation("This is an informational message about the application's normal operation. \n"); 
            logger.LogDebug("This is a debug message for development purposes. \n");
            logger.LogTrace("This is a trace message for detailed debugging. \n");
            logger.LogWarning("This is a warning message indicating a potential issue. \n");
            logger.LogCritical("This is a critical message indicating a severe failure. \n");

            logger.LogInformation($"\n Created Request:\n Ticker: {request.Ticker} \n Timeframe: {request.Timeframe} \n StartDate: {request.StartDate} \n EndDate: {request.EndDate} \n OutputFormat: {request.OutputFormat} \n OutputPath: {request.OutputPath} \n LogLevel: {request.LogLevel} \n ExternalDefinitions: {string.Join(", ", request.ExternalDefinitions.Select(ed => $"{ed.Ticker}:{ed.Timeframe}"))} \n IndicatorDefinitions: {string.Join(", ", request.IndicatorDefinitions.Select(static id => $"{id.Name}({string.Join(",", id.Parameters.Select(kv => $"{kv.Key}={kv.Value}"))})"))}\n");

            var godRunner = new GodRunner(loggerFactory.CreateLogger<GodRunner>());
           

            return await godRunner.RunAsync(request);
        });


        return rootCommand.Parse(args).InvokeAsync().Result;
    }
}