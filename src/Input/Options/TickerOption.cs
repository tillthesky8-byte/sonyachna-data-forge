// src/Input/Options/TickerOption.cs
using Sonyachna_Data_Forge.Domain;
using System.CommandLine;

namespace Sonyachna_Data_Forge.Input.Options;

public class TickerOption
{
    public static Option<PrimaryTicker> PrimaryTicker = new ("--ticker", "-t")
    {
        Description = "Primary ticker for data retrieval (e.g., EURUSD, BTCUSD).",
        CustomParser = result =>
        {
            var token = result.Tokens.Count > 0 ? result.Tokens[0].Value : null;
            if (string.IsNullOrEmpty(token))
                new ArgumentException("Ticker value is required.");
            
            Utilities.ValidateAndParseEnum<PrimaryTicker>(token!, out var parsedTicker);
            return parsedTicker;
        }
    };
}
