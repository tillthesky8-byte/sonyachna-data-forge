// src/Core/DataFuser/IDataFuser.cs
using Sonyachna_Data_Forge.Domain;

namespace Sonyachna_Data_Forge.Core;
public interface IDataFuser
{
    List<FusedDataRow> FuseData(List<OhlcDataRow> primaryData, Dictionary<string, List<ExternalDataRow>> externalDataDict);
}
