namespace MouseAndKeyboard.Native;

/// <summary>
/// A MonitorEnumProc function is an application-defined callback function that is called by the EnumDisplayMonitors function.
/// A Value of type MONITORENUMPROC is a pointer to a MonitorEnumProc function.
/// </summary>
/// <param name="hMonitor">A handle to the display monitor. This Value will always be non-NULL.</param>
/// <param name="hdcMonitor">A handle to a device context.<br/>This Value is NULL if the hdc parameter of EnumDisplayMonitors was NULL.</param>
/// <param name="lprcMonitor">A pointer to a RECT structure.</param>
/// <param name="dwData">Application-defined data that EnumDisplayMonitors passes directly to the enumeration function.</param>
/// <returns></returns>
// https://learn.microsoft.com/en-us/windows/win32/api/winuser/nc-winuser-monitorenumproc
internal delegate bool MonitorEnumProc(IntPtr hMonitor, IntPtr hdcMonitor, ref NativeRect lprcMonitor, IntPtr dwData);
