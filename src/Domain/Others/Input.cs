// Domain/Others/Input.cs

namespace Sonyachna_Data_Forge.Domain;
public record Input
{
    public string? TickerText { get; init; }
    public string? TimeframeText { get; init; }
    public string? StartDateText { get; init; }
    public string? EndDateText { get; init; }
    public List<string>? ExternalExpressionsText { get; init; }
    public List<string>? IndicatorExpressionsText { get; init; }
    public string? OutputFormatText { get; init; }
    public string? OutputPathText { get; init; }
    public string? LogLevelText { get; init; }

}