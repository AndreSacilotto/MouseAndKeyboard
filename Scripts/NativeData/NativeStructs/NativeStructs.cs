using System.Runtime.InteropServices;

namespace MouseAndKeyboard.Native;


[StructLayout(LayoutKind.Sequential)]
public readonly record struct Point
{
    public static Point Zero => new(0, 0);

    public readonly int X;
    public readonly int Y;

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

[StructLayout(LayoutKind.Sequential)]
/// <summary>https://docs.microsoft.com/en-us/windows/win32/api/winuser/ns-winuser-keybdinput</summary>
public readonly struct KeyboardInput
{
    /// <summary>https://docs.microsoft.com/en-us/windows/win32/inputdev/virtual-key-codes</summary>
    public readonly VirtualKeyShort wVk;
    /// <summary>https://www.millisecond.com/support/docs/v6/html/language/scancodes.htm</summary>
    public readonly ScanCodeShort wScan;
    public readonly KeyEventF dwFlags;
    public readonly int time;
    public readonly UIntPtr dwExtraInfo;
    public KeyboardInput(VirtualKeyShort wVk, ScanCodeShort wScan, KeyEventF dwFlags, int time = default, UIntPtr dwExtraInfo = default)
    {
        this.wVk = wVk;
        this.wScan = wScan;
        this.dwFlags = dwFlags;
        this.time = time;
        this.dwExtraInfo = dwExtraInfo;
    }
}

///<summary>https://docs.microsoft.com/en-us/windows/win32/api/winuser/ns-winuser-mouseinput</summary>
[StructLayout(LayoutKind.Sequential)]
public readonly struct MouseInput
{
    public readonly Point pt;
    public readonly int mouseData; //MouseDataXButton or ScrollAmount
    public readonly MouseEventF dwFlags;
    public readonly int time;
    public readonly UIntPtr dwExtraInfo;
    public MouseInput(Point pt, MouseEventF dwFlags, int mouseData = default, int time = default, nuint dwExtraInfo = default)
    {
        this.pt = pt;
        this.mouseData = mouseData;
        this.dwFlags = dwFlags;
        this.time = time;
        this.dwExtraInfo = dwExtraInfo;
    }
    public Point GetPoint() => pt;
    public MouseDataXButton AsXButton() => (MouseDataXButton)mouseData;

    private int GetHighWORD() => mouseData >> (sizeof(int) * 4);
    internal int GetWheelDelta() => GetHighWORD();
}

/// <summary>
///     The AppMouseInput structure contains information about a application-level mouse input event.
/// </summary>
[StructLayout(LayoutKind.Explicit)]
public readonly struct AppMouseInput
{
    /// <summary>
    ///     Specifies a Point structure that contains the X- and Y-coordinates of the cursor, in screen coordinates.
    /// </summary>
    [FieldOffset(0x00)] public readonly Point Point;

    /// <summary>
    ///     Specifies information associated with the message.
    /// </summary>
    /// <remarks>
    ///     The possible values are:
    ///     <list type="bullet">
    ///         <item>
    ///             <description>0 - No Information</description>
    ///         </item>
    ///         <item>
    ///             <description>1 - X-Button1 Click</description>
    ///         </item>
    ///         <item>
    ///             <description>2 - X-Button2 Click</description>
    ///         </item>
    ///         <item>
    ///             <description>120 - Mouse Scroll Away from User</description>
    ///         </item>
    ///         <item>
    ///             <description>-120 - Mouse Scroll Toward User</description>
    ///         </item>
    ///     </list>
    /// </remarks>
    [FieldOffset(0x16)] public readonly short MouseData_x86;

    [FieldOffset(0x22)] public readonly short MouseData_x64;

    /// <summary>
    ///     Converts the current <see cref="AppMouseInput" /> into a <see cref="MouseInput" />.<br/>
    ///     The AppMouseInput does not have a timestamp, thus one is generated at the time of this call.
    /// </summary>
    public static explicit operator MouseInput(AppMouseInput other) =>
        new(other.Point, MouseEventF.None, IntPtr.Size == 4 ? other.MouseData_x86 : other.MouseData_x64, Environment.TickCount);
}