namespace MouseAndKeyboard.InputListener;

public class MKListener : IDisposable
{
    public static MKListener FactoryGlobal() => new(new GlobalKeyboardListener(), new GlobalMouseListener());
    public static MKListener FactoryApp() => new(new AppKeyboardListener(), new AppMouseListener());

    private MKListener(KeyboardListener keyListener, MouseListener mouseListener)
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