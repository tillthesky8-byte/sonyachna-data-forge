// src/Infrastructure/Repository/IRepository.cs
using Sonyachna_Data_Forge.Domain;

namespace Sonyachna_Data_Forge.Infrastructure.Repository;
public interface IRepository
{
    Task<List<OhlcDataRow>> LoadPrimaryAsync(PrimaryTicker ticker, Timeframe timeframe, DateTime startDate, DateTime endDate);
    Task<List<ExternalDataRow>> LoadExternalAsync(ExternalTicker ticker, Timeframe timeframe, DateTime startDate, DateTime endDate);
}