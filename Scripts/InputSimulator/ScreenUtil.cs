using MouseAndKeyboard.Native;
using static MouseAndKeyboard.Native.SystemMetrics;

namespace MouseAndKeyboard.InputSimulation;

public static class ScreenUtil
{
    private const int ABSOLUTE_MAX = ushort.MaxValue;

    public readonly record struct ScreenSize(int Width, int Height, Point Ratio)
    {
        public override string ToString() => $"W: {Width}, H: {Height}";
    }

    public static ScreenSize PrimaryScreenSize { get; private set; }
    public static ScreenSize VirtualScreenSize { get; private set; }

    static ScreenUtil()
    {
        UpdatePrimaryScreenSize();
        UpdateVirtualScreenSize();
    }

    public static void UpdatePrimaryScreenSize()
    {
        int width = GetSystemMetrics(SystemMetric.SM_CXSCREEN);
        int height = GetSystemMetrics(SystemMetric.SM_CYSCREEN);
        var ratio = new Point(ABSOLUTE_MAX / width, ABSOLUTE_MAX / height);
        PrimaryScreenSize = new ScreenSize(width, height, ratio);
    }

    public static void UpdateVirtualScreenSize()
    {
        int width = GetSystemMetrics(SystemMetric.SM_CXVIRTUALSCREEN);
        int height = GetSystemMetrics(SystemMetric.SM_CYVIRTUALSCREEN);
        var ratio = new Point(ABSOLUTE_MAX / width, ABSOLUTE_MAX / height);
        VirtualScreenSize = new ScreenSize(width, height, ratio);
    }

    public static int CountVisibleMonitors() => GetSystemMetrics(SystemMetric.SM_CMONITORS);

}