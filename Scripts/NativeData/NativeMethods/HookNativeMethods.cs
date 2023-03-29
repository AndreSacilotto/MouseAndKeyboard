using System.Runtime.InteropServices;

namespace MouseAndKeyboard.Native;

internal static partial class HookNativeMethods
{
    /// <summary>
    ///     Passes the hook information to the next hook procedure in the current hook chain. A hook procedure can call this
    ///     function either before or after processing the hook information.
    ///     <para>
    ///     See [ https://msdn.microsoft.com/en-us/library/windows/desktop/ms644974%28v=vs.85%29.aspx ] for more
    ///     information.
    ///     </para>
    /// </summary>
    /// <param name="hhk">C++ ( hhk [in, optional]. Type: HHOOK )<br />This parameter is ignored. </param>
    /// <param name="nCode">
    ///     C++ ( nCode [in]. Type: int )<br />The hook code passed to the current hook procedure. The next
    ///     hook procedure uses this code to determine how to process the hook information.
    /// </param>
    /// <param name="wParam">
    ///     C++ ( wParam [in]. Type: WPARAM )<br />The wParam value passed to the current hook procedure. The
    ///     meaning of this parameter depends on the type of hook associated with the current hook chain.
    /// </param>
    /// <param name="lParam">
    ///     C++ ( lParam [in]. Type: LPARAM )<br />The lParam value passed to the current hook procedure. The
    ///     meaning of this parameter depends on the type of hook associated with the current hook chain.
    /// </param>
    /// <returns>
    ///     C++ ( Type: LRESULT )<br />This value is returned by the next hook procedure in the chain. The current hook
    ///     procedure must also return this value. The meaning of the return value depends on the hook type. For more
    ///     information, see the descriptions of the individual hook procedures.
    /// </returns>
    /// <remarks>
    ///     <para>
    ///     CreateHook procedures are installed in chains for particular hook types. <see cref="CallNextHookEx" /> calls the
    ///     next hook in the chain.
    ///     </para>
    ///     <para>
    ///     Calling CallNextHookEx is optional, but it is highly recommended; otherwise, other applications that have
    ///     installed hooks will not receive hook notifications and may behave incorrectly as a result. You should call
    ///     <see cref="CallNextHookEx" /> unless you absolutely need to prevent the notification from being seen by other
    ///     applications.
    ///     </para>
    /// </remarks>
    [LibraryImport("user32.dll")]
    internal static partial IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

    /// <summary>
    ///     The SetWindowsHookEx function installs an application-defined hook procedure into a hook chain.
    ///     You would install a hook procedure to monitor the system for certain types of events. These events
    ///     are associated either with a specific thread or with all threads in the same desktop as the calling thread.
    /// </summary>
    /// <param name="idHook">
    ///     [in] Specifies the type of hook procedure to be installed. This parameter can be one of the following values.
    /// </param>
    /// <param name="lpfn">
    ///     [in] Pointer to the hook procedure. If the dwThreadId parameter is zero or specifies the identifier of a
    ///     thread created by a different process, the lpfn parameter must point to a hook procedure in a dynamic-link
    ///     library (DLL). Otherwise, lpfn can point to a hook procedure in the code associated with the current process.
    /// </param>
    /// <param name="hMod">
    ///     [in] Handle to the DLL containing the hook procedure pointed to by the lpfn parameter.
    ///     The hMod parameter must be set to NULL if the dwThreadId parameter specifies a thread created by
    ///     the current process and if the hook procedure is within the code associated with the current process.
    /// </param>
    /// <param name="dwThreadId">
    ///     [in] Specifies the identifier of the thread with which the hook procedure is to be associated.
    ///     If this parameter is zero, the hook procedure is associated with all existing threads running in the
    ///     same desktop as the calling thread.
    /// </param>
    /// <returns>
    ///     If the function succeeds, the return value is the handle to the hook procedure.
    ///     If the function fails, the return value is NULL. To get extended error information, call GetLastError.
    /// </returns>
    /// <remarks>
    ///     http://msdn.microsoft.com/library/default.asp?url=/library/en-us/winui/winui/windowsuserinterface/windowing/hooks/hookreference/hookfunctions/setwindowshookex.asp
    /// </remarks>
    [LibraryImport("user32.dll", SetLastError = true)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
    internal static partial IntPtr SetWindowsHookExW(HookType hookType, LowLevelMKProc lpfn, IntPtr hMod, uint dwThreadId);

    /// <summary>
    ///     The UnhookWindowsHookEx function removes a hook procedure installed in a hook chain by the SetWindowsHookEx
    ///     function.
    /// </summary>
    /// <param name="idHook">
    ///     [in] Handle to the hook to be removed. This parameter is a hook handle obtained by a previous call to
    ///     SetWindowsHookEx.
    /// </param>
    /// <returns>
    ///     If the function succeeds, the return value is nonzero.
    ///     If the function fails, the return value is zero. To get extended error information, call GetLastError.
    /// </returns>
    /// <remarks>
    ///     http://msdn.microsoft.com/library/default.asp?url=/library/en-us/winui/winui/windowsuserinterface/windowing/hooks/hookreference/hookfunctions/setwindowshookex.asp
    /// </remarks>
    [LibraryImport("user32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static partial bool UnhookWindowsHookEx(IntPtr hhk);

    /// <summary>
    ///     Retrieves the unmanaged thread identifier of the calling thread.
    /// </summary>
    /// <returns></returns>
    [LibraryImport("kernel32")]
    internal static partial uint GetCurrentThreadId();

    /// <summary>
    ///     Retrieves a handle to the foreground window (the window with which the user is currently working).
    ///     The system assigns a slightly higher priority to the thread that creates the foreground window than it does to
    ///     other threads.
    /// </summary>
    /// <returns></returns>
    [LibraryImport("user32.dll")]
    internal static partial IntPtr GetForegroundWindow();

    /// <summary>
    ///     Retrieves the identifier of the thread that created the specified window and, optionally, the identifier of the
    ///     process that
    ///     created the window.
    /// </summary>
    /// <param name="handle">A handle to the window. </param>
    /// <param name="processId">
    ///     A pointer to a variable that receives the process identifier. If this parameter is not NULL,
    ///     GetWindowThreadProcessId copies the identifier of the process to the variable; otherwise, it does not.
    /// </param>
    /// <returns>The return value is the identifier of the thread that created the window. </returns>
    [LibraryImport("user32.dll", SetLastError = true)]
    internal static partial uint GetWindowThreadProcessId(IntPtr handle, out uint processId);

    [LibraryImport("user32.dll")]
    internal static partial uint GetWindowThreadProcessId(IntPtr handle, IntPtr processId);

    [LibraryImport("kernel32.dll", SetLastError = true, StringMarshalling = StringMarshalling.Utf16)]
    public static partial IntPtr GetModuleHandleW([MarshalAs(UnmanagedType.LPWStr)] string lpModuleName);
}