using System;

public partial class MouseKeyboardPacket : Packet
{
    public enum Commands : byte
    {
        /// <summary>Command (1B)</summary>
        Ping = 0,

        /// <summary>Command (1B)</summary>
        TurnOn,
        /// <summary>Command (1B)</summary>
        TurnOff,

        /// <summary>Command X Y (1B 1I 1I)</summary>
        Move,
        /// <summary>Command MouseButton Quant (1B 1B 1B)</summary>
        Click,
        /// <summary>Command Move (1B 1I)</summary>
        Scroll,
        /// <summary>Command Key (1B 1I)</summary>
        Key,
        /// <summary>Command Key Quant (1B 1I 1B)</summary>
        MultKey,
        /// <summary>Command Key Mods (1B 1I 1B 1B)</summary>
        KeyWithModifier,
    }

    [Flags]
    public enum KeyModifier : byte
    {
        None = 0,
        Control = 1,
        Shift = 2,
        Alt = 4,
    }

    public enum MouseButton : byte
    {
        Left = 0,
        Right = 1,
        Middle = 2,
        XB1 = 3,
        XB2 = 4
    }
}

