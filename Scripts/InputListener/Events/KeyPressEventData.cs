namespace MouseAndKeyboard.InputListener;

/// <param name="KeyChar">Gets the character corresponding to the key pressed.</param>
/// <param name="IsNonChar">True if represents a system or functional non char key</param>
public record class KeyPressEventData : EventData
{
    public int KeyChar { get; init; }
    public bool IsNonChar { get; init; }

    public KeyPressEventData(char keyChar, int timestamp) : base(timestamp)
    {
        IsNonChar = keyChar == (char)0x0;
    }
}