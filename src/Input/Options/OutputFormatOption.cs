// src/Input/Options/OutputFormatOption.cs
using Sonyachna_Data_Forge.Domain;
using System.CommandLine;

namespace Sonyachna_Data_Forge.Input.Options;

public class OutputFormatOption
{
    public static Option<OutputFormat> OutputFormat = new("--output-format", "-of")
    {
        Description = "Output format for processed data (e.g., Csv, InMemory).",
        CustomParser = result =>
        {
            var token = result.Tokens.Count > 0 ? result.Tokens[0].Value : null;
            if (string.IsNullOrWhiteSpace(token))
                throw new ArgumentException("Output format value is required.");

            Utilities.ValidateAndParseEnum<OutputFormat>(token, out var parsedOutputFormat);
            return parsedOutputFormat;
        }
    };
}
