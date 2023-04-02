using System.Runtime.InteropServices;

namespace MouseAndKeyboard.Native;

//https://stackoverflow.com/a/2934866

/// <summary>Same as HIGH (value >> 8) and LOW (value &#38; 0b_0000_0000_1111_1111) </summary>
[StructLayout(LayoutKind.Explicit)]
public readonly struct HighLowWORD
{
    [FieldOffset(0)] public readonly WORD Value;
    [FieldOffset(0)] public readonly byte Low;
    [FieldOffset(1)] public readonly byte High;
    public HighLowWORD(short value) => Value = value;

    public const int HALF_WORD_BITS = sizeof(WORD) * 8 / 2;
    public static int GetLow(WORD value) => value & 0b_0000_0000_1111_1111;
    public static int GetHigh(WORD value) => value >> HALF_WORD_BITS;
}

/// <summary>Same as HIGH (value >> 16) and LOW (value &#38; 0b_0000_0000_0000_0000_1111_1111_1111_1111) </summary>
[StructLayout(LayoutKind.Explicit)]
public readonly struct HighLowDWORD
{
    [FieldOffset(0)] public readonly DWORD Value;
    [FieldOffset(0)] public readonly WORD Low;
    [FieldOffset(2)] public readonly WORD High;
    public HighLowDWORD(int value) => Value = value;

    public const int HALF_DWORD_BITS = sizeof(DWORD) * 8 / 2;
    public static int GetLow(WORD value) => value & 0b_0000_0000_0000_0000_1111_1111_1111_1111;
    public static int GetHigh(WORD value) => value >> HALF_DWORD_BITS;
}