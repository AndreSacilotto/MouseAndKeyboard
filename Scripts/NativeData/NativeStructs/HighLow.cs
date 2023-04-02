using System.Runtime.InteropServices;

namespace MouseAndKeyboard.Native;

//https://stackoverflow.com/a/2934866
[StructLayout(LayoutKind.Explicit)]
internal readonly struct HighLowWORD
{
    [FieldOffset(0)] public readonly WORD Value;
    [FieldOffset(0)] public readonly byte Low;
    [FieldOffset(1)] public readonly byte High;
    public HighLowWORD(short value) => Value = value;
}
[StructLayout(LayoutKind.Explicit)]
internal readonly struct HighLowDWORD
{
    [FieldOffset(0)] public readonly DWORD Value;
    [FieldOffset(0)] public readonly WORD Low;
    [FieldOffset(2)] public readonly WORD High;
    public HighLowDWORD(int value) => Value = value;
}