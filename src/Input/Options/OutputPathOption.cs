// src/Input/Options/OutputPathOption.cs
using System.CommandLine;

namespace Sonyachna_Data_Forge.Input.Options;

public class OutputPathOption
{
    public static Option<string?> OutputPath = new("--output-path", "-op")
    {
        Description = "Output path for exported files.",
        CustomParser = result =>
        {
            var token = result.Tokens.Count > 0 ? result.Tokens[0].Value : null;
            if (string.IsNullOrWhiteSpace(token))
                throw new ArgumentException("Output path value is required when using --output-path.");

            return token.Trim();
        }
    };
}
