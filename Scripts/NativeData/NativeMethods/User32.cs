using System.Runtime.InteropServices;
using System.Text;

namespace MouseAndKeyboard.Native;

internal static partial class User32
{
    internal const string USER_32 = "user32.dll";

    // https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-callnexthookex?redirectedfrom=MSDN
    [LibraryImport(USER_32)]
    internal static partial IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

    // https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-enumdisplaymonitors
    [LibraryImport(USER_32)]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static partial bool EnumDisplayMonitors(IntPtr hdc, IntPtr lprcClip, MonitorEnumProc lpfnEnum, IntPtr dwData);

    // https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getcursorpos
    [LibraryImport(USER_32)]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static partial bool GetCursorPos(out Point lpMousePoint);

    // https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getdoubleclicktime
    [LibraryImport(USER_32)]
    internal static partial uint GetDoubleClickTime();

    // https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getforegroundwindow
    [LibraryImport(USER_32)]
    internal static partial IntPtr GetForegroundWindow();

    // https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getkeyboardstate
    [LibraryImport(USER_32, SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static partial bool GetKeyboardState(byte[] pbVirtualKeytate);

    // https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getkeyboardlayout
    [LibraryImport(USER_32)]
    internal static partial IntPtr GetKeyboardLayout(DWORD dwLayout);

    // https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getkeystate
    [LibraryImport(USER_32)]
    internal static partial short GetKeyState(int nVirtKey);

    // https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getmonitorinfow
    [LibraryImport(USER_32)]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static partial bool GetMonitorInfoW(IntPtr hMonitor, ref MonitorInfoEx lpmi);

    // https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getwindowthreadprocessid
    [LibraryImport(USER_32, SetLastError = true)]
    internal static partial DWORD GetWindowThreadProcessId(IntPtr handle, out UDWORD processId);
    [LibraryImport(USER_32)]
    internal static partial DWORD GetWindowThreadProcessId(IntPtr handle, IntPtr processId);

    // https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-monitorfrompoint
    [LibraryImport(USER_32)]
    internal static partial IntPtr MonitorFromPoint(Point pt, DWORD flags);

    // https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-setwindowshookexw
    [LibraryImport(USER_32, SetLastError = true)]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
    internal static partial IntPtr SetWindowsHookExW(HookId hookType, LowLevelMKProc lpfn, IntPtr hMod, DWORD dwThreadId);

    // https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-mapvirtualkeyw
    [LibraryImport(USER_32)]
    internal static partial uint MapVirtualKeyW(uint uCode, MapType uMapType);
    [LibraryImport(USER_32)]
    internal static partial short MapVirtualKeyW(VirtualKey uCode, MapType uMapType);

    // https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-registerhotkey
    [LibraryImport(USER_32, SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static partial bool RegisterHotKey(IntPtr hWnd, int id, InputModifiers fsModifiers, uint vk);

    // https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-tounicodeex
    [DllImport(USER_32)]
    internal static extern int ToUnicodeEx(uint wVirtKey, uint wScanCode, byte[] lpKeyState, [Out, MarshalAs(UnmanagedType.LPWStr, SizeConst = 64)] StringBuilder pwszBuff, int cchBuff, uint wFlags, IntPtr dwhkl);
    [DllImport(USER_32)]
    internal static extern int ToUnicodeEx(VirtualKey wVirtKey, ScanCode wScanCode, byte[] lpKeyState, [Out, MarshalAs(UnmanagedType.LPWStr, SizeConst = 64)] StringBuilder pwszBuff, int cchBuff, KeyEventF wFlags, IntPtr dwhkl);

    // https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-unhookwindowshookex
    [LibraryImport(USER_32, SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static partial bool UnhookWindowsHookEx(IntPtr hhk);

    // https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-unregisterhotkey
    [LibraryImport(USER_32, SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static partial bool UnregisterHotKey(IntPtr hWnd, int id);

    //https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-vkkeyscanw
    [LibraryImport(USER_32, StringMarshalling = StringMarshalling.Utf16)]
    internal static partial short VkKeyScanW(char ch);
}
