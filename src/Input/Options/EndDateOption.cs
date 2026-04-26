// src/Input/Options/EndDateOption.cs
using System.CommandLine;

namespace Sonyachna_Data_Forge.Input.Options;

public class EndDateOption
{
    public static Option<DateTime> EndDate = new("--end-date", "-ed")
    {
        Description = "End date for data retrieval (e.g., 2025-12-31).",
        CustomParser = result =>
        {
            var token = result.Tokens.Count > 0 ? result.Tokens[0].Value : null;
            if (string.IsNullOrWhiteSpace(token))
                throw new ArgumentException("End date value is required.");

            Utilities.ValidateAndParseDateTime(token, "end-date", out var parsedEndDate);
            return parsedEndDate;
        }
    };
}
