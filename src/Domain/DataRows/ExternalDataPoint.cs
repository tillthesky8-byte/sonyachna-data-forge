// Domain/DataRows/ExternalDataRow.cs
namespace Sonyachna_Data_Forge.Domain;

public record ExternalDataRow
{
    public DateTime Timestamp { get; init; }
    public decimal Value { get; init; }
}