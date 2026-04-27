// Service/GodRunner.cs
using Sonyachna_Data_Forge.Domain;
using Microsoft.Extensions.Logging;
using Sonyachna_Data_Forge.Infrastructure.Repository;
using Sonyachna_Data_Forge.Infrastructure.Output;
using Sonyachna_Data_Forge.Core;
using SQLitePCL;
namespace Sonyachna_Data_Forge.Service;
public class GodRunner : IGodRunner
{
    private readonly ILogger<GodRunner> _logger;
    private readonly IRepository _repository;
    private readonly IOutputManager _outputManager;
    private readonly IDataProcessor _dataProcessor;

    public GodRunner(ILogger<GodRunner> logger, IRepository repository, IOutputManager outputManager, IDataProcessor dataProcessor)
    {
        _logger = logger;
        _repository = repository;
        _outputManager = outputManager;
        _dataProcessor = dataProcessor;
    }

    public async Task<int> RunAsync(Request request)
    {
        _logger.LogInformation("Starting GodRunner with the request \n");

        var primaryData = await _repository.LoadPrimaryAsync(request.Ticker, request.Timeframe, request.StartDate, request.EndDate);
        _logger.LogInformation("Loaded primary data with {Count} rows \n", primaryData.Count);
        _logger.LogDebug("Primary data sample: \n {Sample} \n", primaryData.Take(5).Select(d => $"{d.Timestamp}:{d.Open}/{d.Close}").ToList());

        var externalDataDict = new Dictionary<string, List<ExternalDataRow>>();
        foreach (var extDef in request.ExternalDefinitions)
        {
            var extData = await _repository.LoadExternalAsync(extDef.Ticker, extDef.Timeframe, request.StartDate, request.EndDate);
            _logger.LogInformation("Loaded external data for {Ticker} with {Count} rows \n", extDef.Ticker, extData.Count);
            _logger.LogDebug("External data sample for {Ticker}: \n {Sample} \n", extDef.Ticker, extData.Take(5).Select(d => $"{d.Timestamp}:{d.Value}").ToList());
            externalDataDict[$"{extDef.Ticker}"] = extData;
        }

        var processedData = _dataProcessor.ProcessData(primaryData, externalDataDict, request.IndicatorDefinitions);
        _logger.LogInformation("Processed data contains {Count} rows \n", processedData.Count);
        _logger.LogDebug("Processed data sample: \n {Sample} \n", processedData.Take(5).Select(d => $"{d.Timestamp}: Close={d.Close}, Indicators={string.Join(";", d.IndicatorValues.Select(kv => $"{kv.Key}={kv.Value}"))}").ToList());
        
        var outputResult = _outputManager.WriteAsync(processedData, request.OutputFormat, request.OutputPath);
        if (!outputResult.Success)        {
            _logger.LogError("Failed to write output: {Message} \n", outputResult.Message);
            return 1;
        }

        _logger.LogInformation("Completed GodRunner execution \n");
        return 0; 
    }
}