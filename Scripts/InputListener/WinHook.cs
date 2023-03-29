using Microsoft.Win32.SafeHandles;
using MouseAndKeyboard.Native;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace MouseAndKeyboard.InputListener;

public class WinHook : IDisposable
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
                if (HookNativeMethods.UnhookWindowsHookEx(handle))
                    Dispose();
                handle = IntPtr.Zero;
            }
            return true;
        }
    }

    public event NextHookProcedure? Callback;
    private HookProcedureHandle? hookProc;

    private readonly LowLevelMKProc llmkProc;
    private readonly GCHandle gc_llmkProc;

    public WinHook() 
    {
        llmkProc = HookCallback;
        //https://stackoverflow.com/a/69105090
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
        return HookNativeMethods.CallNextHookEx(IntPtr.Zero, nCode, wParam, lParam);
    }

    private static int swapButtonThreshold;
    public static int SwapButtonThreshold => swapButtonThreshold;

    public static T MarshalHookParam<T>(IntPtr lParam) where T : struct => (T)Marshal.PtrToStructure(lParam, typeof(T))!;

    private static WinHook CreateHook(HookType hookType, IntPtr process, uint thread = 0)
    {
        var hook = new WinHook();

        var hookHandle = HookNativeMethods.SetWindowsHookExW(hookType, hook.llmkProc, process, thread);
        var hookProcHandle = new HookProcedureHandle(hookHandle);
        hook.hookProc = hookProcHandle;

        if (hookProcHandle.IsInvalid)
        {
            var errorCode = Marshal.GetLastWin32Error();
            throw new Win32Exception(errorCode);
        }

        swapButtonThreshold = SystemMetrics.GetSwapButtonThreshold();

        return hook;
    }

    #region MK Hooks
    public static WinHook HookAppMouse() => CreateHook(HookType.WH_MOUSE, IntPtr.Zero, HookNativeMethods.GetCurrentThreadId());
    public static WinHook HookAppKeyboard() => CreateHook(HookType.WH_KEYBOARD, IntPtr.Zero, HookNativeMethods.GetCurrentThreadId());
    public static WinHook HookGlobalMouse()
    {
        using Process p = Process.GetCurrentProcess();
        using ProcessModule curModule = p.MainModule!;
        return CreateHook(HookType.WH_MOUSE_LL, curModule.BaseAddress, 0);
    }
    public static WinHook HookGlobalKeyboard()
    {
        using Process p = Process.GetCurrentProcess();
        using ProcessModule curModule = p.MainModule!;
        return CreateHook(HookType.WH_KEYBOARD_LL, curModule.BaseAddress, 0);
    }
    #endregion


}