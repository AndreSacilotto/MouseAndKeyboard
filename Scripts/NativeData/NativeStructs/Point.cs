using System.Runtime.InteropServices;

namespace MouseAndKeyboard.Native;

/// <summary>Point is represents a position</summary>
/// <remarks>
/// https://learn.microsoft.com/en-us/windows/win32/api/windef/ns-windef-point
/// A point is a special, it uses long on its definition everyone just use int for it for some reason
/// <remarks/>
[StructLayout(LayoutKind.Sequential)]
public readonly record struct Point(int X, int Y)
{
    public static Point Zero => new(0, 0);
}
