using System.Runtime.InteropServices;

namespace MouseAndKeyboard.Native;

/// <summary>
///     The CallWndProc hook procedure is an application-defined or library-defined Callback
///     function used with the SetWindowsHookEx function. The HOOKPROC type defines a pointer
///     to this Callback function. CallWndProc is a placeholder for the application-defined
///     or library-defined function name.
/// </summary>
/// <param name="nCode">
///     [in] Specifies whether the hook procedure must process the message.
///     If nCode is HC_ACTION, the hook procedure must process the message.
///     If nCode is less than zero, the hook procedure must pass the message to the
///     CallNextHookEx function without further processing and must return the
///     Value returned by CallNextHookEx.
/// </param>
/// <param name="wParam">
///     [in] Specifies whether the message was sent by the current thread.
///     If the message was sent by the current thread, it is nonzero; otherwise, it is zero.
/// </param>
/// <param name="lParam">
///     [in] Pointer to a CWPSTRUCT structure that contains details about the message.
/// </param>
/// <returns>
///     If nCode is less than zero, the hook procedure must return the Value returned by CallNextHookEx.
///     If nCode is greater than or equal to zero, it is highly recommended that you call CallNextHookEx
///     and return the Value it returns; otherwise, other applications that have installed WH_CALLWNDPROC
///     hooks will not receive hook notifications and may behave incorrectly as a result. If the hook
///     procedure does not call CallNextHookEx, the return Value should be zero.
/// </returns>
// https://learn.microsoft.com/en-us/windows/win32/winmsg/lowlevelmouseproc
// or
// https://learn.microsoft.com/en-us/windows/win32/winmsg/lowlevelkeyboardproc
// or
// https://learn.microsoft.com/en-us/previous-versions/windows/desktop/legacy/ms644988(v=vs.85)#syntax
// or
// https://learn.microsoft.com/en-us/previous-versions/windows/desktop/legacy/ms644984(v=vs.85)#syntax
[UnmanagedFunctionPointer(CallingConvention.Winapi)]
internal delegate IntPtr LowLevelMKProc(int nCode, IntPtr wParam, IntPtr lParam);

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
[UnmanagedFunctionPointer(CallingConvention.Winapi)]
internal delegate bool MonitorEnumProc(IntPtr hMonitor, IntPtr hdcMonitor, ref NativeRect lprcMonitor, IntPtr dwData);

// https://learn.microsoft.com/en-us/windows/win32/api/winuser/nc-winuser-wndproc
[UnmanagedFunctionPointer(CallingConvention.Winapi)]
internal delegate IntPtr WindowProc(IntPtr hwnd, uint uMsg, IntPtr wParam, IntPtr lParam);


