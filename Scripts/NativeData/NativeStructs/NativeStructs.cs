using System.Runtime.InteropServices;

namespace MouseAndKeyboard.Native;


[StructLayout(LayoutKind.Sequential)]
public record struct Point
{
    public int X;
    public int Y;
    public Point() { }
    public Point(int x, int y)
    {
        X = x;
        Y = y;
    }
}

[StructLayout(LayoutKind.Sequential)]
public readonly struct InputStruct
{
    public readonly InputType type;
    public readonly InputUnion union;
    public InputStruct(InputType type, InputUnion union = default)
    {
        this.type = type;
        this.union = union;
    }
    public static int Size => Marshal.SizeOf(typeof(InputStruct));
}

[StructLayout(LayoutKind.Explicit)]
public readonly struct InputUnion
{
    [FieldOffset(0)] public readonly MouseInput mi;
    [FieldOffset(0)] public readonly KeyboardInput ki;
    [FieldOffset(0)] public readonly HardwareInput hi;
    public InputUnion(MouseInput mi = default, KeyboardInput ki = default, HardwareInput hi = default)
    {
        this.mi = mi;
        this.ki = ki;
        this.hi = hi;
    }
}

/// <summary>Define HardwareInput struct</summary>
[StructLayout(LayoutKind.Sequential)]
public readonly struct HardwareInput
{
    public readonly uint uMsg;
    public readonly ushort wParamL;
    public readonly ushort wParamH;
    public HardwareInput(uint uMsg = default, ushort wParamL = default, ushort wParamH = default)
    {
        this.uMsg = uMsg;
        this.wParamL = wParamL;
        this.wParamH = wParamH;
    }
}

///<summary>https://docs.microsoft.com/en-us/windows/win32/api/winuser/ns-winuser-mouseinput</summary>
[StructLayout(LayoutKind.Sequential)]
public readonly record struct MouseInput
{
    public readonly int dx;
    public readonly int dy;
    public readonly short mouseData; //MouseDataXButton or ScrollAmount
    public readonly MouseEventF dwFlags;
    public readonly uint time;
    public readonly UIntPtr dwExtraInfo;
    public MouseInput(int dx, int dy, MouseEventF dwFlags, short mouseData = default, uint time = default, nuint dwExtraInfo = default)
    {
        this.dx = dx;
        this.dy = dy;
        this.mouseData = mouseData;
        this.dwFlags = dwFlags;
        this.time = time;
        this.dwExtraInfo = dwExtraInfo;
    }
    public Point GetPoint() => new(dx, dy);
    public MouseDataXButton AsXButton() => (MouseDataXButton)mouseData;
}

[StructLayout(LayoutKind.Sequential)]
/// <summary>https://docs.microsoft.com/en-us/windows/win32/api/winuser/ns-winuser-keybdinput</summary>
public readonly struct KeyboardInput
{
    /// <summary>https://docs.microsoft.com/en-us/windows/win32/inputdev/virtual-key-codes</summary>
    public readonly VirtualKeyShort wVk;
    /// <summary>https://www.millisecond.com/support/docs/v6/html/language/scancodes.htm</summary>
    public readonly ScanCodeShort wScan;
    public readonly KeyEventF dwFlags;
    public readonly uint time;
    public readonly UIntPtr dwExtraInfo;
    public KeyboardInput(VirtualKeyShort wVk, ScanCodeShort wScan, KeyEventF dwFlags, uint time = default, nuint dwExtraInfo = default)
    {
        this.wVk = wVk;
        this.wScan = wScan;
        this.dwFlags = dwFlags;
        this.time = time;
        this.dwExtraInfo = dwExtraInfo;
    }
}
