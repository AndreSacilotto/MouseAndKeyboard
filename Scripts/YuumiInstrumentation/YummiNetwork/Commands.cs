namespace YuumiInstrumentation;

/// <summary>
/// Description [Size]
/// <br/>[B] = byte
/// <br/>[I] = int
/// </summary>
public enum Command : byte
{
    /// <summary>Command [B] X [I] Y [I]</summary>
    MouseMove,
    /// <summary>Command [B] Amount [I] IsHorizontal [Bool]</summary>
    MouseScroll,

    /// <summary>Command [B] MouseButtonVK [I] PressState [B]</summary>
    MouseClick,

    /// <summary>Command [B] KeyPress [I] PressState [B]</summary>
    KeyPress,
    /// <summary>Command [B] KeyPress [I] VirtualModifier [I] PressState [B]</summary>
    KeyPressWithModifier,

    /// <summary>Command [B] Width [I] Height [I]</summary>
    Screen,
}

