// Domain/DataRows/FusedDataRow.cs

namespace Sonyachna_Data_Forge.Domain;
public record FusedDataRow
{
    public DateTime Timestamp { get; init; }
    public decimal Open { get; init; }
    public decimal High { get; init; }
    public decimal Low { get; init; }
    public decimal Close { get; init; }
    public decimal Volume { get; init; }
    public decimal Spread { get; init; }
    public Dictionary<string, decimal?> ExternalValues { get; init; } = new (StringComparer.OrdinalIgnoreCase);
}