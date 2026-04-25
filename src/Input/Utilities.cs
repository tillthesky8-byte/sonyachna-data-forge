// Input/Utilities.cs
namespace Sonyachna_Data_Forge.Input;
public static class Utilities
{
    public static void ValidateAndParseEnum<TEnum>(string input, out TEnum result) where TEnum : struct, Enum
    {
        var trimmedInput = input.Replace(" ", "").Trim();
        if (!Enum.TryParse<TEnum>(trimmedInput, true, out result))
            throw new ArgumentException($"Invalid value for enum {typeof(TEnum).Name}: {trimmedInput}");
        
    }

}