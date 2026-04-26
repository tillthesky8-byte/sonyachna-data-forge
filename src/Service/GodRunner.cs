// Service/GodRunner.cs
using Sonyachna_Data_Forge.Domain;
using Microsoft.Extensions.Logging;
namespace Sonyachna_Data_Forge.Service;
public class GodRunner : IGodRunner
{
    private readonly ILogger<GodRunner> _logger;

    public GodRunner(ILogger<GodRunner> logger)
    {
        _logger = logger;
    }

    public async Task<int> RunAsync(Request request)
    {
        _logger.LogInformation("Starting GodRunner with the request \n");
        await Task.Delay(1000);
        _logger.LogInformation("Completed GodRunner execution \n");
        return 0; 
    }
}