// src/Input/Options/LogLevelOption.cs
using Sonyachna_Data_Forge.Domain;
using System.CommandLine;

namespace Sonyachna_Data_Forge.Input.Options;

public class LogLevelOption
{
    public static Option<LogLevel> LogLevel = new("--log-level", "-ll")
    {
        Description = "Logging level (e.g., UserMode, DevMode, TraceMode).",
        CustomParser = result =>
        {
            var token = result.Tokens.Count > 0 ? result.Tokens[0].Value : null;
            if (string.IsNullOrWhiteSpace(token))
                throw new ArgumentException("Log level value is required.");

            Utilities.ValidateAndParseEnum<LogLevel>(token, out var parsedLogLevel);
            return parsedLogLevel;
        }
    };
}
