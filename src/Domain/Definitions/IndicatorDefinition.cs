// Domain/Definitions/IndicatorDefinition.cs
namespace Sonyachna_Data_Forge.Domain;
public record IndicatorDefinition
{
    public string? Name { get; init; }
    public IndicatorType Type { get; init; }
    public Dictionary<string, object> Parameters { get; init; } = new (StringComparer.OrdinalIgnoreCase);
}