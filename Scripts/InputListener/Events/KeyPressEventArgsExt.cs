namespace MouseAndKeyboard.InputListener;

/// <summary>
///     Provides extended data for the <see cref='KeyboardListener.KeyPress' /> event.
/// </summary>
public class KeyPressEventArgsExt : KeyPressEventArgs
{
    internal KeyPressEventArgsExt(char keyChar, uint timestamp) : base(keyChar)
    {
        IsNonChar = keyChar == (char)0x0;
        Timestamp = timestamp;
    }


    /// <param name="keyChar">
    ///     Character corresponding to the key pressed. 
    ///     0 char if represents a system or functional non char key.
    /// </param>
    public KeyPressEventArgsExt(char keyChar = (char)0x0) : this(keyChar, (uint)Environment.TickCount)
    {
    }

    /// <summary>
    ///     True if represents a system or functional non char key.
    /// </summary>
    public bool IsNonChar { get; }

    /// <summary>
    ///     The system tick count of when the event occurred.
    /// </summary>
    public uint Timestamp { get; }




}