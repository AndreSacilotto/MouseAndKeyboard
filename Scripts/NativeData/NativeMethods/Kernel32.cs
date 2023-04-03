using System.Runtime.InteropServices;

namespace MouseAndKeyboard.Native;

internal static partial class Kernel32
{
    internal const string KERNEL_32 = "kernel32.dll";

    // https://learn.microsoft.com/en-us/windows/win32/api/processthreadsapi/nf-processthreadsapi-getcurrentthreadid
    [LibraryImport(KERNEL_32)]
    internal static partial DWORD GetCurrentThreadId();

    //https://learn.microsoft.com/en-us/windows/win32/api/libloaderapi/nf-libloaderapi-getmodulehandlew
    [LibraryImport(KERNEL_32, SetLastError = true, StringMarshalling = StringMarshalling.Utf16)]
    internal static partial IntPtr GetModuleHandleW([Optional, MarshalAs(UnmanagedType.LPWStr)] string? lpModuleName);


    [LibraryImport(KERNEL_32, SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static partial bool AllocConsole();

}
