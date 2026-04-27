// src/Core/DataFuser/DataFuser.cs

using Sonyachna_Data_Forge.Domain;

namespace Sonyachna_Data_Forge.Core;

public class DataFuser : IDataFuser
{
    public List<FusedDataRow> FuseData(List<OhlcDataRow> primaryData, Dictionary<string, List<ExternalDataRow>> externalDataDict)
    {
        var fusedData = new List<FusedDataRow>();

        // Create cursors for each external data source
        var externalCursors = externalDataDict.ToDictionary(
            kvp => kvp.Key,
            kvp => new Utilities.ExternalCursor { Index = 0, LastObservedValue = null },
            StringComparer.OrdinalIgnoreCase);

        foreach (var ohlcRow in primaryData)
        {
            var fusedRow = new FusedDataRow
            {
                Timestamp = ohlcRow.Timestamp,
                Open = ohlcRow.Open,
                High = ohlcRow.High,
                Low = ohlcRow.Low,
                Close = ohlcRow.Close,
                Volume = ohlcRow.Volume,
                Spread = ohlcRow.Spread
            };

            // For each external data source, find the most recent value at or before the current timestamp
            foreach (var (name, series) in externalDataDict)
            {
                var cursor = externalCursors[name];

                while (cursor.Index < series.Count && series[cursor.Index].Timestamp <= ohlcRow.Timestamp)
                {
                    cursor.LastObservedValue = series[cursor.Index].Value;
                    cursor.Index++;
                }

                fusedRow.ExternalValues[name] = cursor.LastObservedValue;
            }

            fusedData.Add(fusedRow);
        }

        return fusedData;
    }
}