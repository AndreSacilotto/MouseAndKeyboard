namespace YuumiInstrumentation;

public enum Commands : byte
{
    /// <summary>Command [B] X [I] Y [I]</summary>
    MouseMove,
    /// <summary>Command [B] Quant [I]</summary>
    MouseScroll,

    /// <summary>Command [B] MouseButton [I] PressState [B]</summary>
    MouseClick,

    /// <summary>Command [B] Key [I] PressState [B]</summary>
    Key,
    /// <summary>Command [B] Key [I] Modifier [I] PressState [B]</summary>
    KeyModifier,
}

