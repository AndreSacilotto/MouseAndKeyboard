
using MouseAndKeyboard.Native;
using System.Text;

namespace MouseAndKeyboard.InputListener;

internal static class KeyboardStateHelper
{
    /// <summary>
    /// Contains a snapshot of a keyboard state at certain moment and provides methods
    /// of querying whether specific keys are pressed or locked.
    /// </summary>
    public class KeyboardSnapshot
    {
        private readonly byte[] keyboardStateNative;

        public byte[] KeyboardStateNative => keyboardStateNative;

        private KeyboardSnapshot(byte[] keyboardStateNative) => this.keyboardStateNative = keyboardStateNative;

        /// <summary>Makes a snapshot of a keyboard state to the moment of call and returns an instance of <see cref="KeyboardSnapshot"/> class</summary>
        /// <returns>An instance of <see cref="KeyboardSnapshot" /> class representing a snapshot of keyboard state at certain moment</returns>
        public static KeyboardSnapshot CreateSnapshot()
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
        public bool IsDown(VirtualKey key)
        {
            if ((int)key <= byte.MaxValue) return IsDownRaw(key);
            return key switch
            {
                VirtualKey.Alt => IsDownRaw(VirtualKey.LeftMenu) || IsDownRaw(VirtualKey.RightMenu),
                VirtualKey.Shift => IsDownRaw(VirtualKey.LeftShift) || IsDownRaw(VirtualKey.RightShift),
                VirtualKey.Control => IsDownRaw(VirtualKey.LeftControl) || IsDownRaw(VirtualKey.RightControl),
                _ => false
            };
        }

        private bool IsDownRaw(VirtualKey key)
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
        public bool IsToggled(VirtualKey key)
        {
            var keyState = GetKeyState(key);
            var isToggled = GetLowBit(keyState);
            return isToggled;
        }

        /// <summary>
        ///     Indicates weather every of specified keys were down at the moment when snapshot was created.
        ///     The method returns false if even one of them was up.
        /// </summary>
        /// <param name="Keys">Keys to verify whether they were down or not.</param>
        /// <returns><b>true</b> - all were down. <b>false</b> - at least one was up.</returns>
        public bool AreAllDown(IEnumerable<VirtualKey> keys)
        {
            foreach (var item in keys)
                if (IsDown(item))
                    return false;
            return true;
        }

        private byte GetKeyState(VirtualKey key)
        {
            var vk = (int)key;
            if (vk < 0 || vk > byte.MaxValue)
                throw new ArgumentOutOfRangeException(nameof(key), key, "The Value must be between 0 and 255");
            return keyboardStateNative[vk];
        }

        private static bool GetHighBit(byte value) => value >> 7 != 0;
        private static bool GetLowBit(byte value) => (value & 1) != 0;
    }

    //Used to pass Unicode characters as if they were keystrokes. The VK_PACKET key is the Low word of a 32-bit Virtual Key Value used for non-keyboard input methods
    private static VirtualKey lastVirtualKeyCode;
    private static ScanCode lastScanCode;

    private static byte[] lastKeyState = new byte[byte.MaxValue];
    private static bool lastIsDead;

    /// <summary>Translates a virtual key to its character equivalent using a specified keyboard layout</summary>
    internal static char[]? TryGetCharFromKeyboardState(VirtualKey virtualKeyCode, ScanCode scanCode, KeyEventF fuState)
    {
        char[]? chars;

        var dwhkl = DeviceUtil.GetActiveKeyboardLayout();

        var pwszBuff = new StringBuilder(64);
        var keyboardState = KeyboardSnapshot.CreateSnapshot();
        var currentKeyboardState = keyboardState.KeyboardStateNative;
        var isDead = false;

        if (keyboardState.IsDown(VirtualKey.Shift))
            currentKeyboardState[(int)VirtualKey.Shift] = 0x80;

        if (keyboardState.IsToggled(VirtualKey.CapitalLock))
            currentKeyboardState[(int)VirtualKey.CapitalLock] = 0x01;

        var relevantChars = KeyNativeMethods.ToUnicodeEx(virtualKeyCode, (uint)scanCode, currentKeyboardState, pwszBuff, pwszBuff.Capacity, fuState, dwhkl);

        switch (relevantChars)
        {
            case -1:
            // ClearKeyboardBuffer
            var sb = new StringBuilder(10);
            int rc;
            do
            {
                var lpKeyStateNull = new byte[byte.MaxValue];
                rc = KeyNativeMethods.ToUnicodeEx(virtualKeyCode, (uint)scanCode, lpKeyStateNull, sb, sb.Capacity, 0, dwhkl);
            } while (rc < 0);
            isDead = true;
            chars = null;
            break;

            case 0:
            chars = null;
            break;

            case 1:
            if (pwszBuff.Length > 0)
                chars = new[] { pwszBuff[0] };
            else
                chars = null;
            break;

            // Two or more (only two of them is relevant)
            default:
            if (pwszBuff.Length > 1)
                chars = new[] { pwszBuff[0], pwszBuff[1] };
            else
                chars = new[] { pwszBuff[0] };
            break;
        }

        if (lastVirtualKeyCode != 0 && lastIsDead)
        {
            if (chars != null)
            {
                var sbTemp = new StringBuilder(5);
                _ = KeyNativeMethods.ToUnicodeEx((uint)lastVirtualKeyCode, (uint)lastScanCode, lastKeyState, sbTemp, sbTemp.Capacity, 0, dwhkl);
                lastIsDead = false;
                lastVirtualKeyCode = 0;
            }
        }
        else
        {
            lastScanCode = scanCode;
            lastVirtualKeyCode = virtualKeyCode;
            lastIsDead = isDead;
            lastKeyState = (byte[])currentKeyboardState.Clone();
        }

        return chars;
    }

}
