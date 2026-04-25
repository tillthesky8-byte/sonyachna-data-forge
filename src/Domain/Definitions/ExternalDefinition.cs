// Domain/Definitions/ExternalDefinition.cs
namespace Sonyachna_Data_Forge.Domain;
public record ExternalDefinition
{
    public ExternalTicker Ticker { get; init; }
    public Timeframe Timeframe { get; init; }
}