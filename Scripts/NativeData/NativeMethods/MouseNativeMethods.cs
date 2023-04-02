
using System.Runtime.InteropServices;

namespace MouseAndKeyboard.Native;

internal static partial class MouseNativeMethods
{
    /// <summary>
    ///     The GetDoubleClickTime function retrieves the current double-click time for the mouse. A double-click is a series
    ///     of two clicks of the
    ///     mouse button, the second occurring within a specified time after the first. The double-click time is the maximum
    ///     number of
    ///     milliseconds that may occur between the first and second click of a double-click.
    /// </summary>
    /// <returns>
    ///     The return Value specifies the current double-click time, in milliseconds.
    /// </returns>
    // https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getdoubleclicktime
    [LibraryImport("user32")]
    internal static partial uint GetDoubleClickTime();

    // https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getcursorpos
    [LibraryImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static partial bool GetCursorPos(out Point lpMousePoint);
}
