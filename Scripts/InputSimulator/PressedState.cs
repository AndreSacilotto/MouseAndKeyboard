namespace MouseAndKeyboard.InputSimulator;

[Flags]
public enum PressState : byte
{
    None = 0,
    Down = 1,
    Up = 2,
    Click = Down | Up,

    /// <summary>Not Implemented</summary>
    Pressed = 4,
}