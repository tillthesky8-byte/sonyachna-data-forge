// src/Input/Options/TimeframeOption.cs
using Sonyachna_Data_Forge.Domain;
using System.CommandLine;
using Sonyachna_Data_Forge.Input;
namespace Sonyachna_Data_Forge.Input.Options;

public class TimeframeOption
{
    public static Option<Timeframe> Timeframe = new ("--timeframe", "-tf")
    {
        Description = "Timeframe for data retrieval (e.g., D, W, M).",
        CustomParser = result =>
        {
            var token = result.Tokens.Count > 0 ? result.Tokens[0].Value : null;
            if (string.IsNullOrEmpty(token))
                new ArgumentException("Timeframe value is required.");
            
            Utilities.ValidateAndParseEnum<Timeframe>(token!, out var parsedTimeframe);
            return parsedTimeframe;
        }
    };
}