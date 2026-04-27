// tests/Core/FuseDataTest.cs
using Sonyachna_Data_Forge.Domain;
using Sonyachna_Data_Forge.Core;
using Xunit;
namespace Sonyachna_Data_Forge.Tests.Core;

public class FuseDataTest
{
    [Fact]
    public void FuseData_CorrectlyFusesData()
    {
        // Arrange
        var primaryData = new List<OhlcDataRow>
        {
            new OhlcDataRow { Timestamp = new DateTime(2023, 1, 1), Open = 100, High = 110, Low = 90, Close = 105, Volume = 1000, Spread = 20 },
            new OhlcDataRow { Timestamp = new DateTime(2023, 1, 2), Open = 105, High = 115, Low = 95, Close = 110, Volume = 1500, Spread = 20 },
            new OhlcDataRow { Timestamp = new DateTime(2023, 1, 3), Open = 110, High = 120, Low = 100, Close = 115, Volume = 2000, Spread = 20 },
            new OhlcDataRow { Timestamp = new DateTime(2023, 1, 4), Open = 115, High = 125, Low = 105, Close = 120, Volume = 2500, Spread = 20 }
        };
        System.Console.WriteLine($"Primary Data first row: {primaryData[0].Timestamp}, {primaryData[0].Open}, {primaryData[0].High}, {primaryData[0].Low}, {primaryData[0].Close}, {primaryData[0].Volume}, {primaryData[0].Spread}");
        System.Console.WriteLine($"Primary Data second row: {primaryData[1].Timestamp}, {primaryData[1].Open}, {primaryData[1].High}, {primaryData[1].Low}, {primaryData[1].Close}, {primaryData[1].Volume}, {primaryData[1].Spread}");
        System.Console.WriteLine($"Primary Data third row: {primaryData[2].Timestamp}, {primaryData[2].Open}, {primaryData[2].High}, {primaryData[2].Low}, {primaryData[2].Close}, {primaryData[2].Volume}, {primaryData[2].Spread}");

        var externalDataDict = new Dictionary<string, List<ExternalDataRow>>
        {
            {
                "SourceA", new List<ExternalDataRow>
                {
                    new ExternalDataRow { Timestamp = new DateTime(2023, 1, 1), Value = 10 },
                    new ExternalDataRow { Timestamp = new DateTime(2023, 1, 2), Value = 20 }
                }
            },
            {
                "SourceB", new List<ExternalDataRow>
                {
                    new ExternalDataRow { Timestamp = new DateTime(2023, 1, 2), Value = 200 },
                    new ExternalDataRow { Timestamp = new DateTime(2023, 1, 3), Value = 300 }
                }
            },
            {
                "SourceC", new List<ExternalDataRow>
                {
                    new ExternalDataRow { Timestamp = new DateTime(2023, 1, 1, 0, 0, 0), Value = 1000 },
                    new ExternalDataRow { Timestamp = new DateTime(2023, 1, 1, 12, 0, 0), Value = 2000 },
                    new ExternalDataRow { Timestamp = new DateTime(2023, 1, 2, 0, 0, 0), Value = 3000 },
                    new ExternalDataRow { Timestamp = new DateTime(2023, 1, 2, 12, 0, 0), Value = 4000 },
                    new ExternalDataRow { Timestamp = new DateTime(2023, 1, 3, 0, 0, 0), Value = 5000 },
                    new ExternalDataRow { Timestamp = new DateTime(2023, 1, 3, 12, 0, 0), Value = 6000 }
                }
            },
                {
                "SourceD", new List<ExternalDataRow>()
                {
                    new ExternalDataRow { Timestamp = new DateTime(2023, 1, 2), Value = 500 },
                    new ExternalDataRow { Timestamp = new DateTime(2023, 1, 4), Value = 600 }
                }
            }
        };
        System.Console.WriteLine($"External Data SourceA first row: {externalDataDict["SourceA"][0].Timestamp}, {externalDataDict["SourceA"][0].Value}");
        System.Console.WriteLine($"External Data SourceA second row: {externalDataDict["SourceA"][1].Timestamp}, {externalDataDict["SourceA"][1].Value}");

        System.Console.WriteLine($"External Data SourceB first row: {externalDataDict["SourceB"][0].Timestamp}, {externalDataDict["SourceB"][0].Value}");
        System.Console.WriteLine($"External Data SourceB second row: {externalDataDict["SourceB"][1].Timestamp}, {externalDataDict["SourceB"][1].Value}");

        System.Console.WriteLine($"External Data SourceC first row: {externalDataDict["SourceC"][0].Timestamp}, {externalDataDict["SourceC"][0].Value}");
        System.Console.WriteLine($"External Data SourceC second row: {externalDataDict["SourceC"][1].Timestamp}, {externalDataDict["SourceC"][1].Value}");
        System.Console.WriteLine($"External Data SourceD first row: {externalDataDict["SourceD"][0].Timestamp}, {externalDataDict["SourceD"][0].Value}");
        System.Console.WriteLine($"External Data SourceD second row: {externalDataDict["SourceD"][1].Timestamp}, {externalDataDict["SourceD"][1].Value}");


        var dataFuser = new DataFuser();
        // Act
        var fusedData = dataFuser.FuseData(primaryData, externalDataDict);
        System.Console.WriteLine($"Fused Data first row: {fusedData[0].Timestamp}, {fusedData[0].Open}, {fusedData[0].High}, {fusedData[0].Low}, {fusedData[0].Close}, {fusedData[0].Volume}, {fusedData[0].Spread}, SourceA: {fusedData[0].ExternalValues["SourceA"]}, SourceB: {fusedData[0].ExternalValues["SourceB"]}, SourceC: {fusedData[0].ExternalValues["SourceC"]}, SourceD: {fusedData[0].ExternalValues["SourceD"]}");
        System.Console.WriteLine($"Fused Data second row: {fusedData[1].Timestamp}, {fusedData[1].Open}, {fusedData[1].High}, {fusedData[1].Low}, {fusedData[1].Close}, {fusedData[1].Volume}, {fusedData[1].Spread}, SourceA: {fusedData[1].ExternalValues["SourceA"]}, SourceB: {fusedData[1].ExternalValues["SourceB"]}, SourceC: {fusedData[1].ExternalValues["SourceC"]}, SourceD: {fusedData[1].ExternalValues["SourceD"]}");
        System.Console.WriteLine($"Fused Data third row: {fusedData[2].Timestamp}, {fusedData[2].Open}, {fusedData[2].High}, {fusedData[2].Low}, {fusedData[2].Close}, {fusedData[2].Volume}, {fusedData[2].Spread}, SourceA: {fusedData[2].ExternalValues["SourceA"]}, SourceB: {fusedData[2].ExternalValues["SourceB"]}, SourceC: {fusedData[2].ExternalValues["SourceC"]}, SourceD: {fusedData[2].ExternalValues["SourceD"]}");
        System.Console.WriteLine($"Fused Data fourth row: {fusedData[3].Timestamp}, {fusedData[3].Open}, {fusedData[3].High}, {fusedData[3].Low}, {fusedData[3].Close}, {fusedData[3].Volume}, {fusedData[3].Spread}, SourceA: {fusedData[3].ExternalValues["SourceA"]}, SourceB: {fusedData[3].ExternalValues["SourceB"]}, SourceC: {fusedData[3].ExternalValues["SourceC"]}, SourceD: {fusedData[3].ExternalValues["SourceD"]}");
        // Assert
        Assert.Equal(4, fusedData.Count);
        Assert.Equal(new DateTime(2023, 1, 1), fusedData[0].Timestamp);
        Assert.Equal(100, fusedData[0].Open);
        Assert.Equal(10, fusedData[0].ExternalValues["SourceA"]);
        Assert.Null(fusedData[0].ExternalValues["SourceB"]);
        Assert.Equal(new DateTime(2023, 1, 2), fusedData[1].Timestamp);
        Assert.Equal(105, fusedData[1].Open);
        Assert.Equal(20, fusedData[1].ExternalValues["SourceA"]);
        Assert.Equal(200, fusedData[1].ExternalValues["SourceB"]);
        Assert.Equal(new DateTime(2023, 1, 3), fusedData[2].Timestamp);
        Assert.Equal(110, fusedData[2].Open);
        Assert.Equal(20, fusedData[2].ExternalValues["SourceA"]);
        Assert.Equal(300, fusedData[2].ExternalValues["SourceB"]);
    }
}