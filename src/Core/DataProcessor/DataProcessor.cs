//src/Core/DataProcessor/DataProcessor.cs
using Sonyachna_Data_Forge.Domain;
using Sonyachna_Data_Forge.Indicators.IndicatorFactory;
using Sonyachna_Data_Forge.Infrastructure.Logging;
using Microsoft.Extensions.Logging;
namespace Sonyachna_Data_Forge.Core;
public class DataProcessor : IDataProcessor
{
    private readonly IIndicatorFactory _indicatorFactory;
    private readonly ILogger<DataProcessor> _logger;
    private readonly ILoggerFactory _loggerFactory;

    public DataProcessor(IIndicatorFactory indicatorFactory, ILoggerFactory loggerFactory)
    {
        _indicatorFactory = indicatorFactory;
        _loggerFactory = loggerFactory;
        _logger = loggerFactory.CreateLogger<DataProcessor>();
    }

    public List<ProcessedDataRow> ProcessData(List<OhlcDataRow> primaryData, Dictionary<string, List<ExternalDataRow>> externalDataDict, List<IndicatorDefinition> indicatorDefinitions)
    {
        _logger.LogInformation("Starting data processing with {PrimaryDataCount} primary data rows, {ExternalDefCount} external definitions, and {IndicatorDefCount} indicator definitions.",
            primaryData.Count, externalDataDict.Count, indicatorDefinitions.Count);
        var dataFuserLogger = _loggerFactory.CreateLogger<DataFuser>();
        var dataFuser = new DataFuser(dataFuserLogger);
        var fusedData = dataFuser.FuseData(primaryData, externalDataDict);
        _logger.LogInformation("Completed data fusion. Fused data contains {FusedDataCount} rows.", fusedData.Count);

        var dataEngneer = new DataEngineer(_indicatorFactory, _loggerFactory.CreateLogger<DataEngineer>());
        var processedData = dataEngneer.ProcessData(fusedData, indicatorDefinitions);
        _logger.LogInformation("Completed data engineering. Processed data contains {ProcessedDataCount} rows.", processedData.Count);
        return processedData;
    }

}