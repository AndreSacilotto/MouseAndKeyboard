using System.Runtime.InteropServices;

namespace MouseAndKeyboard.Native;

internal partial class DeviceNativeMethods
{
    // https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-monitorfrompoint
    [LibraryImport("User32.dll")]
    internal static partial IntPtr MonitorFromPoint(Point pt, DWORD flags);

    // https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getmonitorinfow
    [LibraryImport("User32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static partial bool GetMonitorInfoW(IntPtr hMonitor, ref MonitorInfoEx lpmi);

    // https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-enumdisplaymonitors
    [LibraryImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static partial bool EnumDisplayMonitors(IntPtr hdc, IntPtr lprcClip, MonitorEnumProc lpfnEnum, IntPtr dwData);
}
