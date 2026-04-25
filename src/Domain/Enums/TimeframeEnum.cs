// Domain/Enums/TimeframeEnum.cs
namespace Sonyachna_Data_Forge.Domain;

public enum Timeframe
{
    m1,
    m5,
    m15,
    m30,
    h1,
    h4,
    D,
    W,
    M,
    M3,
    Y,
    IRREGULAR,
    ANY // Special and default value to indicate that any timeframe is acceptable. So when loading data, the first available timeframe will be used.
}