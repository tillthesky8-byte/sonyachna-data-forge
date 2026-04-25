// Domain/Others/Response.cs
namespace Sonyachna_Data_Forge.Domain;
public record Response
{
    public bool Success { get; init; }
    public string? Message { get; init; }
    public List<ProcessedDataRow> Data { get; init; } = new ();
}