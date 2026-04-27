// src/Core/Utilities.cs
using Sonyachna_Data_Forge.Domain;
namespace Sonyachna_Data_Forge.Core;
public static class Utilities
{
    // utility to sort external data by timestamp for efficient merging
    public record ExternalCursor
    {
        public int Index { get; set; }
        public decimal? LastObservedValue { get; set; }
    };
    public static Dictionary<string, List<ExternalDataRow>> SortExternalData(Dictionary<string, List<ExternalDataRow>> externalDataDict)
    {
        var sortedDict = new Dictionary<string, List<ExternalDataRow>>(StringComparer.OrdinalIgnoreCase);
        foreach (var kvp in externalDataDict)
        {
            sortedDict[kvp.Key] = kvp.Value.OrderBy(d => d.Timestamp).ToList();
        }
        return sortedDict;
    }

}

