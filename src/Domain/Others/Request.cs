// Domain/Others/Request.cs
namespace Sonyachna_Data_Forge.Domain;
public record Request
{
    public PrimaryTicker Ticker { get; init; }
    public Timeframe Timeframe { get; init; }
    public DateTime StartDate { get; init; }
    public DateTime EndDate { get; init; }
    public List<ExternalDefinition> ExternalDefinitions { get; init; } = new ();
    public List<IndicatorDefinition> IndicatorDefinitions { get; init; } = new ();
    public OutputFormat OutputFormat { get; init; }
    public string? OutputPath { get; init; }
    public LogLevel LogLevel { get; init; }
}