using Microsoft.Win32.SafeHandles;
using MouseAndKeyboard.Native;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace MouseAndKeyboard.InputListener.Hook;

public class MKWinHook : IDisposable
{
    public delegate void NextHookProcedure(IntPtr wParam, IntPtr lParam);

    public class HookProcedureHandle : SafeHandleZeroOrMinusOneIsInvalid
    {
        public HookProcedureHandle() : base(true) { }
        public HookProcedureHandle(IntPtr handle) : base(true)
        {
            SetHandle(handle);
        }
        protected override bool ReleaseHandle()
        {
            if (!IsInvalid)
            {
                if (User32.UnhookWindowsHookEx(handle))
                    Dispose();
                handle = IntPtr.Zero;
            }
            return true;
        }
    }

    public event NextHookProcedure? Callback;
    private HookProcedureHandle? hookProc;

    /* https://stackoverflow.com/a/69105090 */
    private readonly LowLevelMKProc llmkProc;
    private readonly GCHandle gc_llmkProc;
    public MKWinHook()
    {
        llmkProc = HookCallback;
        gc_llmkProc = GCHandle.Alloc(llmkProc);
    }

    public void Dispose()
    {
        gc_llmkProc.Free();
        hookProc?.Dispose();
        GC.SuppressFinalize(this);
    }

    private IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
    {
        if (nCode == 0)
            Callback?.Invoke(wParam, lParam);
        return User32.CallNextHookEx(IntPtr.Zero, nCode, wParam, lParam);
    }

    public static T MarshalHookParam<T>(IntPtr lParam) where T : struct => (T)Marshal.PtrToStructure(lParam, typeof(T))!;

    private static MKWinHook CreateHook(HookId hookType, IntPtr process, int thread = 0)
    {
        var hook = new MKWinHook();

        var hookHandle = User32.SetWindowsHookExW(hookType, hook.llmkProc, process, thread);
        var hookProcHandle = new HookProcedureHandle(hookHandle);
        hook.hookProc = hookProcHandle;

        if (hookProcHandle.IsInvalid)
        {
            var errorCode = Marshal.GetLastWin32Error();
            throw new Win32Exception(errorCode);
        }

        return hook;
    }

    #region MK Hooks
    public static MKWinHook HookAppMouse() => CreateHook(HookId.WH_MOUSE, IntPtr.Zero, Kernel32.GetCurrentThreadId());
    public static MKWinHook HookAppKeyboard() => CreateHook(HookId.WH_KEYBOARD, IntPtr.Zero, Kernel32.GetCurrentThreadId());
    public static MKWinHook HookGlobalMouse()
    {
        using Process p = Process.GetCurrentProcess();
        using ProcessModule curModule = p.MainModule!;
        return CreateHook(HookId.WH_MOUSE_LL, curModule.BaseAddress, 0);
    }
    public static MKWinHook HookGlobalKeyboard()
    {
        using Process p = Process.GetCurrentProcess();
        using ProcessModule curModule = p.MainModule!;
        return CreateHook(HookId.WH_KEYBOARD_LL, curModule.BaseAddress, 0);
    }
    #endregion


}