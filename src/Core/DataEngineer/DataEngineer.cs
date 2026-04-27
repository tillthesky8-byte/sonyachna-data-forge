// src/DataEngineer/DataEngineer.cs

using Sonyachna_Data_Forge.Domain;
using Sonyachna_Data_Forge.Core;
using Sonyachna_Data_Forge.Indicators;
using Sonyachna_Data_Forge.Indicators.IndicatorFactory;
using Microsoft.Extensions.Logging;

namespace Sonyachna_Data_Forge.Core;
public class DataEngineer : IDataEngineer
{
    private readonly IIndicatorFactory _indicatorFactory;
    private readonly ILogger<DataEngineer> _logger;

    public DataEngineer(IIndicatorFactory indicatorFactory, ILogger<DataEngineer> logger)
    {
        _indicatorFactory = indicatorFactory;
        _logger = logger;
    }

    public List<ProcessedDataRow> ProcessData(List<FusedDataRow> fusedData, List<IndicatorDefinition> indicatorDefinitions)
    {
        var processedData = fusedData.Select(fd => new ProcessedDataRow
        {
            Timestamp = fd.Timestamp,
            Open = fd.Open,
            High = fd.High,
            Low = fd.Low,
            Close = fd.Close,
            Volume = fd.Volume,
            Spread = fd.Spread,
            ExternalValues = fd.ExternalValues
        }).ToList();

        foreach (var definition in indicatorDefinitions)
        {
            _logger.LogInformation("Processing indicator: {IndicatorType}", definition.Type);
            var indicator = _indicatorFactory.ResolveIndicator(definition.Type);
            var indicatorValues = indicator.Calculate(fusedData, definition);

            if (indicatorValues.Count != processedData.Count)
            {
                throw new InvalidOperationException($"Indicator calculation for {definition.Name} returned {indicatorValues.Count} values, but expected {processedData.Count}.");
            }

            for (int i = 0; i < processedData.Count; i++)
            {
                processedData[i].IndicatorValues[definition.Name] = indicatorValues[i];
            }
        }

        return processedData;
    }
}

