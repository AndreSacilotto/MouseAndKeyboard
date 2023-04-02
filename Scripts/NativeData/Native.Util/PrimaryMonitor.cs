namespace MouseAndKeyboard.Native;

public class PrimaryMonitor
{
    public static PrimaryMonitor Instance { get; private set; } = new(DeviceUtil.GetPrimaryScreenSize());

    public static void Update(Point size) => Instance = new(size);

    private const int SCREEN_ABSOLUTE = ushort.MaxValue;

    public int Width { get; init; }
    public int Height { get; init; }
    public Point Scale { get; init; }

    private PrimaryMonitor(Point size)
    {
        Width = size.X;
        Height = size.Y;
        Scale = new(SCREEN_ABSOLUTE / Width, SCREEN_ABSOLUTE / Height);
    }

    public Point CoordsToAbsolute(int x, int y) => new(x * Scale.X + 1, y * Scale.Y + 1);

    public Point GetSize() => new(Width, Height);
}
