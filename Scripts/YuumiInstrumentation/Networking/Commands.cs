namespace YuumiInstrumentation;

/// <summary>
/// Description [Size]
/// <br/>[B] = byte
/// <br/>[I] = int
/// </summary>
public enum Commands : byte
{
    /// <summary>Command [B] X [I] Y [I]</summary>
    MouseMove,
    /// <summary>Command [B] Amount [I]</summary>
    MouseScroll,

    /// <summary>Command [B] MouseButton [I] PressState [B]</summary>
    MouseClick,

    /// <summary>Command [B] Key [I] PressState [B]</summary>
    Key,
    /// <summary>Command [B] Key [I] Modifier [I] PressState [B]</summary>
    KeyModifier,

    /// <summary>Command [B] Width [I] Height [I]</summary>
    Screen,
}

