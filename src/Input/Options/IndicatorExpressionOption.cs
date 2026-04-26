// src/Input/Options/IndicatorExpressionOption.cs
using Sonyachna_Data_Forge.Domain;
using System.CommandLine;

namespace Sonyachna_Data_Forge.Input.Options;

public class IndicatorExpressionOption
{
    public static Option<List<IndicatorDefinition>> IndicatorExpressions = new("--indicator", "-i")
    {
        Description = "Indicator definition expression. Can be repeated (e.g., sma:source=close,period=20).",
        Arity = ArgumentArity.OneOrMore,
        AllowMultipleArgumentsPerToken = true,
        CustomParser = result =>
        {
            var tokens = result.Tokens
                .Select(t => t.Value)
                .Where(v => !string.IsNullOrWhiteSpace(v))
                .ToList();

            if (tokens.Count == 0)
                throw new ArgumentException("At least one indicator definition is required when using --indicator.");

            return tokens
                .Select(Utilities.ParseIndicatorDefinition)
                .ToList();
        }
    };
}
