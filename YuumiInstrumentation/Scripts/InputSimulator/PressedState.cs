using System;

namespace InputSimulation
{
    [Flags]
    public enum PressedState : byte
    {
        None = 0,
        Down = 1,
        Up = 2,
        Click = Down | Up,

        /// <summary>Not Implemented</summary>
        Press = 4,
    }
}