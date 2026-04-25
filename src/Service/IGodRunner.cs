// Service/IGodRunner.cs
using Sonyachna_Data_Forge.Domain;
namespace Sonyachna_Data_Forge.Service;
public interface IGodRunner
{
    Task<int> RunAsync(Request request);
}