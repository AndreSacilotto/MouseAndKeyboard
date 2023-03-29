using MouseAndKeyboard.Native;

namespace MouseAndKeyboard.InputListener;

/// <summary>
///     Contains a snapshot of a keyboard state at certain moment and provides methods
///     of querying whether specific keys are pressed or locked.
/// </summary>
/// <remarks>
///     This class is basically a managed wrapper of GetKeyboardState API function
///     http://msdn.microsoft.com/en-us/library/ms646299
/// </remarks>
public class KeyboardSnapshot
{
    private readonly byte[] keyboardStateNative;

    public byte[] KeyboardStateNative => keyboardStateNative;

    private KeyboardSnapshot(byte[] keyboardStateNative)
    {
        this.keyboardStateNative = keyboardStateNative;
    }

    /// <summary>
    ///     Makes a snapshot of a keyboard state to the moment of call and returns an
    ///     instance of <see cref="KeyboardSnapshot" /> class.
    /// </summary>
    /// <returns>An instance of <see cref="KeyboardSnapshot" /> class representing a snapshot of keyboard state at certain moment.</returns>
    public static KeyboardSnapshot GetCurrent()
    {
        var keyboardStateNative = new byte[byte.MaxValue + 1];
        KeyboardNativeMethods.GetKeyboardState(keyboardStateNative);
        return new KeyboardSnapshot(keyboardStateNative);
    }

    /// <summary>
    ///     Indicates whether specified key was down at the moment when snapshot was created or not.
    /// </summary>
    /// <param name="key">Key (corresponds to the virtual code of the key)</param>
    /// <returns><b>true</b> if key was down, <b>false</b> - if key was up.</returns>
    public bool IsDown(Keys key)
    {
        if ((int)key <= byte.MaxValue) return IsDownRaw(key);
        return key switch
        {
            Keys.Alt => IsDownRaw(Keys.LMenu) || IsDownRaw(Keys.RMenu),
            Keys.Shift => IsDownRaw(Keys.LShiftKey) || IsDownRaw(Keys.RShiftKey),
            Keys.Control => IsDownRaw(Keys.LControlKey) || IsDownRaw(Keys.RControlKey),
            _ => false
        };
    }

    private bool IsDownRaw(Keys key)
    {
        var keyState = GetKeyState(key);
        var isDown = GetHighBit(keyState);
        return isDown;
    }

    /// <summary>
    ///     Indicate weather specified key was toggled at the moment when snapshot was created or not.
    /// </summary>
    /// <param name="key">Key (corresponds to the virtual code of the key)</param>
    /// <returns>
    ///     <b>true</b> if toggle key like (CapsLock, NumLocke, etc.) was on. <b>false</b> if it was off.
    ///     Ordinal (non toggle) keys return always false.
    /// </returns>
    public bool IsToggled(Keys key)
    {
        var keyState = GetKeyState(key);
        var isToggled = GetLowBit(keyState);
        return isToggled;
    }

    /// <summary>
    ///     Indicates weather every of specified keys were down at the moment when snapshot was created.
    ///     The method returns false if even one of them was up.
    /// </summary>
    /// <param name="keys">Keys to verify whether they were down or not.</param>
    /// <returns><b>true</b> - all were down. <b>false</b> - at least one was up.</returns>
    public bool AreAllDown(IEnumerable<Keys> keys)
    {
        foreach (var item in keys)
            if (IsDown(item))
                return false;
        return true;
    }

    private byte GetKeyState(Keys key)
    {
        var virtualKeyCode = (int)key;
        if (virtualKeyCode < 0 || virtualKeyCode > byte.MaxValue)
            throw new ArgumentOutOfRangeException(nameof(key), key, "The value must be between 0 and 255.");
        return keyboardStateNative[virtualKeyCode];
    }

    private static bool GetHighBit(byte value)
    {
        return value >> 7 != 0;
    }

    private static bool GetLowBit(byte value)
    {
        return (value & 1) != 0;
    }
}