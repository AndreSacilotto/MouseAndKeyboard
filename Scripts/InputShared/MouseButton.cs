using MouseAndKeyboard.Native;

namespace MouseAndKeyboard.InputShared;

//[Flags]
public enum MouseButton : WORD
{
    /// <summary>No mouse button was pressed</summary>
    None = 0,

    /// <summary>The left mouse button. MB 1</summary>
    Left = VirtualKey.LeftButton,

    /// <summary>The right mouse button. MB 2</summary>
    Right = VirtualKey.RightButton,

    /// <summary>The middle mouse button.  MB 3</summary>
    Middle = VirtualKey.MiddleButton,

    /// <summary>MB 4</summary>
    XButton1 = VirtualKey.XButton1,

    /// <summary>MB 5</summary>
    XButton2 = VirtualKey.XButton2,
}
