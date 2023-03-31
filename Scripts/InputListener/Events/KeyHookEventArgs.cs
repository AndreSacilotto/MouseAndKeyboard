using MouseAndKeyboard.Native;

namespace MouseAndKeyboard.InputListener;

public class KeyHookEventArgs : KeyEventArgs
{
    internal KeyHookEventArgs(Keys keyData, ScanCode scanCode, int timestamp, bool isKeyDown, bool isKeyUp, bool isExtendedKey) :
        base(keyData)
    {
        ScanCode = scanCode;
        Timestamp = timestamp;
        IsKeyDown = isKeyDown;
        IsKeyUp = isKeyUp;
        IsExtendedKey = isExtendedKey;
    }

    /// <summary>hardware scan code</summary>
    public ScanCode ScanCode { get; }

    /// <summary>The system tick count of when the event occurred</summary>
    public int Timestamp { get; }

    public bool IsKeyDown { get; }

    public bool IsKeyUp { get; }

    /// <summary>If true the ScanCode consists of a sequence of two bytes, where the first byte has a value of 0xE0</summary>
    public bool IsExtendedKey { get; }
}