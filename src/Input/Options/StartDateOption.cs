// src/Input/Options/StartDateOption.cs
using System.CommandLine;

namespace Sonyachna_Data_Forge.Input.Options;

public class StartDateOption
{
    public static Option<DateTime> StartDate = new("--start-date", "-sd")
    {
        Description = "Start date for data retrieval (e.g., 2008-08-17).",
        CustomParser = result =>
        {
            var token = result.Tokens.Count > 0 ? result.Tokens[0].Value : null;
            if (string.IsNullOrWhiteSpace(token))
                throw new ArgumentException("Start date value is required.");

            Utilities.ValidateAndParseDateTime(token, "start-date", out var parsedStartDate);
            return parsedStartDate;
        }
    };
}
