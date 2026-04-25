// Domain/DataRows/OhlcDataRow.cs
namespace Sonyachna_Data_Forge.Domain;
public record OhlcDataRow
{
    public DateTime Timestamp { get; init; }
    public decimal Open { get; init; }
    public decimal High { get; init; }
    public decimal Low { get; init; }
    public decimal Close { get; init; }
    public decimal Volume { get; init; }
    public decimal Spread { get; init; }

}