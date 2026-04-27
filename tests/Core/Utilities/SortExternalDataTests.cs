// tests/Core/Utilities/SortExternalDataTests.cs
using Sonyachna_Data_Forge.Domain;

using Xunit;

namespace Sonyachna_Data_Forge.Tests.Core.Utilities;
public class SortExternalDataTests
{
    [Fact]
    public void SortExternalData_SortsByTimestamp()
    {
        // Arrange
        var externalDataDict = new Dictionary<string, List<ExternalDataRow>>
        {
            {
                "SourceA", new List<ExternalDataRow>
                {
                    new ExternalDataRow { Timestamp = new DateTime(2023, 1, 3), Value = 30 },
                    new ExternalDataRow { Timestamp = new DateTime(2023, 1, 1), Value = 10 },
                    new ExternalDataRow { Timestamp = new DateTime(2023, 1, 2), Value = 20 }
                }
            },
            {
                "SourceB", new List<ExternalDataRow>
                {
                    new ExternalDataRow { Timestamp = new DateTime(2023, 2, 2), Value = 200 },
                    new ExternalDataRow { Timestamp = new DateTime(2023, 2, 1), Value = 100 }
                }
            }
        };

        // Act
        var sortedDict = Sonyachna_Data_Forge.Core.Utilities.SortExternalData(externalDataDict);

        // Assert
        Assert.Equal(new DateTime(2023, 1, 1), sortedDict["SourceA"][0].Timestamp);
        Assert.Equal(new DateTime(2023, 1, 2), sortedDict["SourceA"][1].Timestamp);
        Assert.Equal(new DateTime(2023, 1, 3), sortedDict["SourceA"][2].Timestamp);

        Assert.Equal(new DateTime(2023, 2, 1), sortedDict["SourceB"][0].Timestamp);
        Assert.Equal(new DateTime(2023, 2, 2), sortedDict["SourceB"][1].Timestamp);
    }
}