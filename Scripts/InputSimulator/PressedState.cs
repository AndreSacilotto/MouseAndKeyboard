namespace MouseAndKeyboard.InputSimulator;

public enum PressedState : byte
{
    None = 0,
    Down = 1,
    Up = 2,
    Click = 3,

    /// <summary>Not Implemented</summary>
    Pressed = 4,
}