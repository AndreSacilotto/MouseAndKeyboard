namespace MouseAndKeyboard.InputListener.Hook;

public class MKHookListener : IDisposable
{
    public static MKHookListener FactoryGlobal() => new(new GlobalKeyboardListener(), new GlobalMouseListener());
    public static MKHookListener FactoryApp() => new(new AppKeyboardListener(), new AppMouseListener());

    private MKHookListener(KeyboardListener keyListener, MouseListener mouseListener)
    {
        KeyListener = keyListener;
        MouseListener = mouseListener;
    }

    public KeyboardListener KeyListener { get; }
    public MouseListener MouseListener { get; }

    public void Dispose()
    {
        MouseListener.Dispose();
        KeyListener.Dispose();
        GC.SuppressFinalize(this);
    }

}