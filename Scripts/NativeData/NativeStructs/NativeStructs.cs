using System.Runtime.InteropServices;

namespace MouseAndKeyboard.Native;

// https://learn.microsoft.com/en-us/windows/win32/api/winuser/ns-winuser-input
[StructLayout(LayoutKind.Sequential)]
public readonly struct InputStruct
{
    public enum InputType : DWORD
    {
        Mouse = 0,
        Keyboard = 1,
        Hardware = 2
    }

    [StructLayout(LayoutKind.Explicit)]
    public readonly struct InputUnion
    {
        [FieldOffset(0)] public readonly MouseInput mi;
        [FieldOffset(0)] public readonly KeyboardInput ki;
        [FieldOffset(0)] public readonly HardwareInput hi;
        internal InputUnion(MouseInput mi) => this.mi = mi;
        internal InputUnion(KeyboardInput ki) => this.ki = ki;
        internal InputUnion(HardwareInput hi) => this.hi = hi;
    }

    public readonly InputType type;
    public readonly InputUnion union;
    private InputStruct(InputType type, InputUnion union = default)
    {
        this.type = type;
        this.union = union;
    }
    public static int Size => Marshal.SizeOf(typeof(InputStruct));

    public static InputStruct NewInput(MouseInput input) => new(InputType.Mouse, new(input));
    public static InputStruct NewInput(KeyboardInput input) => new(InputType.Keyboard, new(input));
    public static InputStruct NewInput(HardwareInput input) => new(InputType.Hardware, new(input));
}

// https://learn.microsoft.com/en-us/windows/win32/api/winuser/ns-winuser-hardwareinput
[StructLayout(LayoutKind.Sequential)]
public readonly struct HardwareInput
{
    public readonly DWORD uMsg;
    public readonly WORD wParamL;
    public readonly WORD wParamH;
    public HardwareInput(int uMsg = default, short wParamL = default, short wParamH = default)
    {
        this.uMsg = uMsg;
        this.wParamL = wParamL;
        this.wParamH = wParamH;
    }
}

/// <summary> Keyboard Input Struct </summary>
// https://docs.microsoft.com/en-us/windows/win32/api/winuser/ns-winuser-keybdinput
[StructLayout(LayoutKind.Sequential)]
public readonly struct KeyboardInput
{
    public readonly VirtualKey wVk;
    public readonly ScanCode wScan;
    public readonly KeyEventF dwFlags;
    public readonly DWORD time;
    public readonly UIntPtr dwExtraInfo;
    public KeyboardInput(VirtualKey wVk, ScanCode wScan, KeyEventF dwFlags, int time = default, UIntPtr dwExtraInfo = default)
    {
        this.wVk = wVk;
        this.wScan = wScan;
        this.dwFlags = dwFlags;
        this.time = time;
        this.dwExtraInfo = dwExtraInfo;
    }
}

/// <summary> Keyboard Low-Level Input Struct </summary>
// https://learn.microsoft.com/en-us/windows/win32/api/winuser/ns-winuser-kbdllhookstruct
[StructLayout(LayoutKind.Sequential)]
public readonly struct KeyboardLLInput
{
    public readonly DWORD wVk;
    public readonly DWORD wScan;
    public readonly KeyEventF dwFlags;
    public readonly DWORD time;
    public readonly UIntPtr dwExtraInfo;
    public KeyboardLLInput(DWORD wVk, DWORD wScan, KeyEventF dwFlags, int time = default, UIntPtr dwExtraInfo = default)
    {
        this.wVk = wVk;
        this.wScan = wScan;
        this.dwFlags = dwFlags;
        this.time = time;
        this.dwExtraInfo = dwExtraInfo;
    }
}

/// <summary> Mouse Low-Level Input Struct </summary>
// https://docs.microsoft.com/en-us/windows/win32/api/winuser/ns-winuser-mouseinput
// https://learn.microsoft.com/en-us/windows/win32/api/winuser/ns-winuser-msllhookstruct
[StructLayout(LayoutKind.Sequential)]
public readonly struct MouseInput
{
    //public readonly Point pt;
    public readonly int X;
    public readonly int Y;
    /// <summary>MouseDataXButton or ScrollAmount</summary>
    public readonly HighLowDWORD mouseData;
    public readonly MouseEventF dwFlags;
    public readonly DWORD time;
    public readonly UIntPtr dwExtraInfo;
    public MouseInput(Point pt, MouseEventF dwFlags, int mouseData = default, int time = default, UIntPtr dwExtraInfo = default)
    {
        X = pt.X;
        Y = pt.Y;
        this.mouseData = new(mouseData);
        this.dwFlags = dwFlags;
        this.time = time;
        this.dwExtraInfo = dwExtraInfo;
    }
    public MouseInput(int x, int y, MouseEventF dwFlags, int mouseData = default, int time = default, nuint dwExtraInfo = default)
    {
        X = x;
        Y = y;
        this.mouseData = new(mouseData);
        this.dwFlags = dwFlags;
        this.time = time;
        this.dwExtraInfo = dwExtraInfo;
    }

    public Point GetPoint() => new(X, Y);
    public MouseDataXButton GetXButton() => (MouseDataXButton)mouseData.High;
    internal short GetWheelDelta() => mouseData.High;
}

// https://learn.microsoft.com/en-us/windows/win32/api/winuser/ns-winuser-mousehookstruct
// https://learn.microsoft.com/en-us/windows/win32/api/winuser/ns-winuser-mousehookstructex
[StructLayout(LayoutKind.Sequential)]
internal readonly struct MouseHookStructEx
{
    public readonly Point pt;
    public readonly IntPtr hwnd;
    public readonly uint wHitTestCode;
    public readonly UIntPtr dwExtraInfo;
    public readonly DWORD mouseData;
    public MouseHookStructEx(Point pt, nint hwnd, uint wHitTestCode, nuint dwExtraInfo, int mouseData)
    {
        this.pt = pt;
        this.hwnd = hwnd;
        this.wHitTestCode = wHitTestCode;
        this.dwExtraInfo = dwExtraInfo;
        this.mouseData = mouseData;
    }
    public MouseInput ToMouseInput() => new(pt.X, pt.Y, MouseEventF.None, mouseData, Environment.TickCount, dwExtraInfo);
}

/// <summary>
/// The MONITORINFO structure contains information about a display monitor.
/// <br/>The GetMonitorInfo function stores information in a MONITORINFO structure or a MONITORINFOEX structure.
/// <br/>The MONITORINFO structure is a subset of the MONITORINFOEX structure.The MONITORINFOEX structure adds a string member to contain a name for the display monitor.
/// </summary>
// https://learn.microsoft.com/en-us/windows/win32/api/winuser/ns-winuser-monitorinfo
// https://github.com/dapplo/Dapplo.Windows/blob/master/src/Dapplo.Windows.User32/Structs/MonitorInfoEx.cs
[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
internal unsafe struct MonitorInfoEx
{
    private readonly DWORD cbSize = Marshal.SizeOf(typeof(MonitorInfoEx));
    public readonly NativeRect rcMonitor;
    public readonly NativeRect rcWork;
    public readonly DWORD dwFlags;
    private fixed char szDevice[32];
    public MonitorInfoEx() { }
    public string DeviceName
    {
        get
        {
            fixed (char* deviceName = szDevice)
                return new string(deviceName);
        }
    }
}
