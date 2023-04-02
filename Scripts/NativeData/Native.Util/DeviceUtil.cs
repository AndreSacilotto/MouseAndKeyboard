namespace MouseAndKeyboard.Native;

//https://learn.microsoft.com/en-us/windows/win32/gdi/the-virtual-screen
public static class DeviceUtil
{
    public readonly record struct DisplayInfo(IntPtr Handler, NativeRect Bounds, NativeRect WorkArea, string Name, bool IsPrimary)
    {
        public int WorkWidth => Math.Abs(WorkArea.Right - WorkArea.Left);
        public int WorkHeight => Math.Abs(WorkArea.Bottom - WorkArea.Top);

        public int Width => Math.Abs(Bounds.Right - Bounds.Left);
        public int Height => Math.Abs(Bounds.Bottom - Bounds.Top);
    }

    public static Point GetPrimaryScreenSize() => SystemMetrics.GetPrimaryMonitorScreenSize();

    public static Point GetVirtualScreenSize() => SystemMetrics.GetVirtualScreenSize();

    public static void GetCursorPosition(out int x, out int y)
    {
        MouseNativeMethods.GetCursorPos(out var pt);
        x = pt.X;
        y = pt.Y;
    }
    public static Point GetCursorPosition()
    {
        MouseNativeMethods.GetCursorPos(out var pt);
        return pt;
    }

    public static List<DisplayInfo> GetAllMonitors()
    {
        var list = new List<DisplayInfo>();
        DeviceNativeMethods.EnumDisplayMonitors(IntPtr.Zero, IntPtr.Zero, MonitorEnumProcFunc, IntPtr.Zero);
        return list;
        bool MonitorEnumProcFunc(IntPtr hMonitor, IntPtr hdcMonitor, ref NativeRect lprcMonitor, IntPtr dwData)
        {
            var monitorInfoEx = new MonitorInfoEx();
            if (DeviceNativeMethods.GetMonitorInfoW(hMonitor, ref monitorInfoEx))
            {
                var d = new DisplayInfo()
                {
                    Handler = hMonitor,
                    Bounds = monitorInfoEx.rcMonitor,
                    WorkArea = monitorInfoEx.rcWork,
                    Name = monitorInfoEx.DeviceName,
                    IsPrimary = monitorInfoEx.dwFlags > 0,
                };
                list.Add(d);
            }
            return true;
        }
    }

    /// <summary>
    /// Gets the input locale identifier for the active application's thread.  Using this combined with the ToUnicodeEx and
    /// MapVirtualKeyEx enables Windows to properly translate keys based on the keyboard layout designated for the application.
    /// </summary>
    public static IntPtr GetActiveKeyboardLayout()
    {
        var hActiveWnd = HookNativeMethods.GetForegroundWindow(); //handle to focused window
        var hCurrentWnd = HookNativeMethods.GetWindowThreadProcessId(hActiveWnd, IntPtr.Zero); //thread of focused window
        return KeyboardNativeMethods.GetKeyboardLayout(hCurrentWnd); //get the layout identifier for the thread whose window is focused
    }



}