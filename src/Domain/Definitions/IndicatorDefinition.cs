// Domain/Definitions/IndicatorDefinition.cs
namespace Sonyachna_Data_Forge.Domain;
public record IndicatorDefinition
{
    public required string Name { get; init; }
    public IndicatorType Type { get; init; }
    public Dictionary<string, string> Parameters { get; init; } = new (StringComparer.OrdinalIgnoreCase);
}