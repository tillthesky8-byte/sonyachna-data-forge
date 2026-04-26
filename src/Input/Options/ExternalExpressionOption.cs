// src/Input/Options/ExternalExpressionOption.cs
using Sonyachna_Data_Forge.Domain;
using System.CommandLine;

namespace Sonyachna_Data_Forge.Input.Options;

public class ExternalExpressionOption
{
    public static Option<List<ExternalDefinition>> ExternalExpressions = new("--external", "-e")
    {
        Description = "External definition expression. Can be repeated (e.g., us_interest_rate:D).",
        Arity = ArgumentArity.OneOrMore,
        AllowMultipleArgumentsPerToken = true,
        CustomParser = result =>
        {
            var tokens = result.Tokens
                .Select(t => t.Value)
                .Where(v => !string.IsNullOrWhiteSpace(v))
                .ToList();

            if (tokens.Count == 0)
                throw new ArgumentException("At least one external definition is required when using --external.");

            return tokens
                .Select(Utilities.ParseExternalDefinition)
                .ToList();
        }
    };
}
