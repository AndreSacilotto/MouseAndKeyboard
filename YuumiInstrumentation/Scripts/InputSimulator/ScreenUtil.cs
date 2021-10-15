using System.Runtime.InteropServices;

public static class ScreenUtil
{
    /// <summary>https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getsystemmetrics</summary>
    [DllImport("user32.dll")]
    static extern int GetSystemMetrics(int smIndex);

    public struct ScreenSize
    {
        internal readonly int width;
        internal readonly int height;
        public ScreenSize(int width, int height)
        {
            this.width = width;
            this.height = height;
        }
    }

    public static ScreenSize GetPrimaryScreenSize { get; private set; }
    public static ScreenSize GetVirtualScreenSize { get; private set; }

    static ScreenUtil()
    {
        UpdatePrimaryScreenSize();
        UpdateVirtualScreenSize();
    }

    public static void UpdatePrimaryScreenSize()
    {
        //SM_CXSCREEN
        int width = GetSystemMetrics(0);
        //SM_CYSCREEN
        int height = GetSystemMetrics(1);
        GetPrimaryScreenSize = new ScreenSize(width, height);
    }

    public static void UpdateVirtualScreenSize()
    {
        //SM_CXVIRTUALSCREEN
        int width = GetSystemMetrics(78);
        //SM_CYVIRTUALSCREEN
        int height = GetSystemMetrics(79);
        GetVirtualScreenSize = new ScreenSize(width, height);
    }

    //SM_CMONITORS
    public static int CountVisibleMonitors() => GetSystemMetrics(80);

}
