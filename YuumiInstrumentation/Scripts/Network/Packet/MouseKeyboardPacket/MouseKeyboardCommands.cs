using System;

namespace MouseKeyboardPacket
{
    public enum Commands : byte
    {
        /// <summary>Command [B]</summary>
        Ping = 0,

        /// <summary>Command [B]</summary>
        Shutdown,

        /// <summary>Command [B] X [I] Y [I]</summary>
        MouseMove,
        /// <summary>Command [B] MouseButton [I]</summary>
        MouseClick,
        /// <summary>Command [B] MouseButton [I] Quant [i]</summary>
        MouseDoubleClick,
        /// <summary>Command [B] Quant [I]</summary>
        MouseScroll,

        /// <summary>Command [B] Key [I]</summary>
        KeyDown,
        /// <summary>Command [B] Key [I]</summary>
        KeyUp,

        /// <summary>Command [B] Key [I] Mods [B]</summary>
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

}

