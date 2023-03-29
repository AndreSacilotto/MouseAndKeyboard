using MouseAndKeyboard.Native;

namespace MouseAndKeyboard.InputListener;

/// <summary>
///     Provides extended argument data for the <see cref='KeyboardListener.KeyDown' /> or
///     <see cref='KeyboardListener.KeyUp' /> event.
/// </summary>
public class KeyEventArgsExt : KeyEventArgs
{
    internal KeyEventArgsExt(Keys keyData, ScanCodeShort scanCode, int timestamp, bool isKeyDown, bool isKeyUp, bool isExtendedKey) : base(keyData)
    {
        ScanCode = scanCode;
        Timestamp = timestamp;
        IsKeyDown = isKeyDown;
        IsKeyUp = isKeyUp;
        IsExtendedKey = isExtendedKey;
    }

    /// <summary>hardware scan code</summary>
    public ScanCodeShort ScanCode { get; }

    /// <summary>The system tick count of when the event occurred</summary>
    public int Timestamp { get; }

    public bool IsKeyDown { get; }

    public bool IsKeyUp { get; }

    public bool IsExtendedKey { get; }




}