namespace MouseAndKeyboard.InputSimulation;

/*
1600 x 900
1360 x 768
=
2960 x 900
*/

public static class ScreenUtil
{

    public readonly struct ScreenSize
    {
        internal readonly int width;
        internal readonly int height;
        internal ScreenSize(int width, int height)
        {
            this.width = width;
            this.height = height;
        }
        public override string ToString() => $"W: {width}, H: {height}";
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
        int width = SystemMetrics.GetSystemMetrics(SystemMetrics.SM.SM_CXSCREEN);
        int height = SystemMetrics.GetSystemMetrics(SystemMetrics.SM.SM_CYSCREEN);
        GetPrimaryScreenSize = new ScreenSize(width, height);
    }

    public static void UpdateVirtualScreenSize()
    {
        int width = SystemMetrics.GetSystemMetrics(SystemMetrics.SM.SM_CXVIRTUALSCREEN);
        int height = SystemMetrics.GetSystemMetrics(SystemMetrics.SM.SM_CYVIRTUALSCREEN);
        GetVirtualScreenSize = new ScreenSize(width, height);
    }

    public static int CountVisibleMonitors() => SystemMetrics.GetSystemMetrics(SystemMetrics.SM.SM_CMONITORS);

}