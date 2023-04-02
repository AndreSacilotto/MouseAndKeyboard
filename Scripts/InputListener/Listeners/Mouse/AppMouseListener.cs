using MouseAndKeyboard.Native;
using System.Runtime.InteropServices;

namespace MouseAndKeyboard.InputListener;

internal class AppMouseListener : MouseListener
{
    public AppMouseListener() : base(WinHook.HookAppMouse())
    {
    }

    protected override MouseEventData GetEventArgs(IntPtr wParam, IntPtr lParam)
    {
        var mouseHookStruct = (MouseInput)WinHook.MarshalHookParam<AppMouseInput>(lParam);
        return MouseEventData.NewEvent((WindowsMessages)wParam, ref mouseHookStruct, swapButtonThreshold);
    }

    /// <summary>AppMouseInput structure contains information about a application-level mouse input event</summary>
    [StructLayout(LayoutKind.Explicit)]
    public readonly struct AppMouseInput
    {
        [FieldOffset(0x00)] public readonly Point Point;
        [FieldOffset(0x16)] public readonly short MouseData_x86;
        [FieldOffset(0x22)] public readonly short MouseData_x64;
        public static explicit operator MouseInput(AppMouseInput other) =>
            new(other.Point.X, other.Point.Y, MouseEventF.None, IntPtr.Size == 4 ? other.MouseData_x86 : other.MouseData_x64, Environment.TickCount);
    }

}
