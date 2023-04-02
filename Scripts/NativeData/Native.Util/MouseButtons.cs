namespace MouseAndKeyboard.Native;

[Flags]
public enum MouseButtonsF : byte
{
    /// <summary>No mouse button was pressed</summary>
    None = 0,
    /// <summary>The left mouse button. MB 1</summary>
    Left = 1 << 0,
    /// <summary>The right mouse button. MB 2</summary>
    Right = 1 << 1,
    /// <summary>The middle mouse button.  MB 3</summary>
    Middle = 1 << 2,
    /// <summary>MB 4</summary>
    XButton1 = 1 << 3,
    /// <summary>MB 5</summary>
    XButton2 = 1 << 4,
}
