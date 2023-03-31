namespace MouseAndKeyboard.InputListener;

public interface IHandledEvent
{
    /// <summary>Value that represents if the event was already fully procesed</summary>
    bool Handled { get; }
    /// <summary>Set this event as handled, used to prevent further processing of this event</summary>
    void PreventProgation();
}

public class KeyHookPressEventArgs : KeyPressEventArgs
{
    internal KeyHookPressEventArgs(char keyChar, int timestamp) : base(keyChar)
    {
        IsNonChar = keyChar == (char)0x0;
        Timestamp = timestamp;
    }

    /// <summary>True if represents a system or functional non char key</summary>
    public bool IsNonChar { get; }

    /// <summary>The system tick count of when the event occurred</summary>
    public int Timestamp { get; }

}