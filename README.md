# SONYACHNA-DATA-FORGE

## OVERVIEW

SONYACHNA-DATA-FORGE is part of the SONYACHNA project, which is focused on loading and transforming financial and other time-series data for use in machine learning models or backtesting engine (or both). The tool is designed to be flexible with dynamic handling of external datasets and indicators, allowing users to specify arbitrary combinations and amounts of external datasets and indicators through a simple command-line interface or YAML configuration.

## REQUIREMENTS
- *.NET 10*
- *C# 14*
- *SQLite* for data storage
- *System.CommandLine* for CLI parsing
- *YamlDotNet* for YAML configuration parsing
- *Microsoft.Extensions.Logging and Logging.Console* for logging
- *Microsoft.Data.Sqlite* for SQLite interactions
- *System.Globalization* for case-insensitive parsing

## USAGE
To use SONYACHNA-DATA-FORGE, run the command-line interface with the appropriate options. For example:
```bash
dotnet run -- --ticker GBPUSD --start-date 2008-08-17 --end-date 2025-12-31 --timeframe 1D --external us_interest_rate:timeframe=irregular --external gb_interest_rate --indicator sma:source=close,period=20 --output-format csv
``` 
This command will fetch daily OHLCV data for GBPUSD from August 17, 2008, to December 31, 2025, include external datasets for US and GB interest rates with LOCF alignment, compute a 20-period Simple Moving Average (SMA) based on the close price, and output the results in CSV format.


## ARCHITECTURE

0. Program.cs: Entry point of the application with root command definition and self-parsing options, which create *Request* object and pass it to *GodRunner*

1. *GodRunner*: Orchestrates the entire system after getting special *Request* object, coordinating between data loading, processing, and output.

2. *Repository*: Loads raw data from SQLite, providing access to primary OHLCV data and external datasets without performing any merging or indicator computation.

3. *CoreProcessor*: also orchestrates some of the other modules, it recieves list of OHLCV, dictionary of external datasets, and list of indicators definitions, then produces enriched list of *ProcessedDataPoint* which is the end result of the pipeline.

    - *DataFuser*: Sorts data aligns externals to primary timestamps using LOCF (Last Observation Carried Forward) without look-ahead bias and produces list of *FusedDataPoint* which is the input for feature engineering.

    - *DataEngineer*: Resolves indicator implementations through an indicator factory, computes indicators. Returns list of *ProcessedDataPoint* with indicators values attached.

        - *IndicatorFactory*: Maintains a registry of available indicators and their implementations, allowing for dynamic resolution based on indicator definitions.

        - *IIndicator*: Currently includes SMA and RSI, with the ability to easily add more indicators in the future.

4. *OutputManager*: Formats the processed data into the desired output format (e.g., CSV, InMemory) for final delivery. It also handles outputting to console or file as needed.