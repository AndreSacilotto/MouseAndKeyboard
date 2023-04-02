
using System.Runtime.InteropServices;
using System.Text;

namespace MouseAndKeyboard.Native;

internal static partial class KeyNativeMethods
{
    /// <summary>
    ///     Translates the specified virtual-key code and keyboard state to the corresponding Unicode character or characters.
    /// </summary>
    /// <param name="wVirtKey">[in] The virtual-key code to be translated.</param>
    /// <param name="wScanCode">
    ///     [in] The hardware scan code of the key to be translated. The High-order bit of this Value is set if the key is up.
    /// </param>
    /// <param name="lpKeyState">
    ///     [in, optional] A pointer to a 256-byte array that contains the current keyboard state. Each
    ///     element (byte) in the array contains the state of one key. If the High-order bit of a byte is set, the key is down.
    /// </param>
    /// <param name="pwszBuff">
    ///     [out] The buffer that receives the translated Unicode character or characters. However, this
    ///     buffer may be returned without being null-terminated even though the variable name suggests that it is
    ///     null-terminated.
    /// </param>
    /// <param name="cchBuff">[in] The size, in characters, of the buffer pointed to by the pwszBuff parameter.</param>
    /// <param name="wFlags">[in] The behavior of the function. If bit 0 is set, a menu is active. Bits 1 through 31 are reserved</param>
    /// <param name="dwhkl">The input locale identifier used to translate the specified code.</param>
    /// <returns>
    ///     Range: -1..N
    ///     <br/><br/>
    ///     -1 = The specified virtual key is a dead-key character (accent or diacritic). This Value is returned
    ///     regardless of the keyboard layout, even if several characters have been typed and are stored in the
    ///     keyboard state. If possible, even with Unicode keyboard layouts, the function has written a spacing version
    ///     of the dead-key character to the buffer specified by pwszBuff. For example, the function writes the
    ///     character SPACING ACUTE (0x00B4), rather than the character NON_SPACING ACUTE (0x0301).
    ///     <br/><br/>
    ///     0 = The specified virtual key has no translation for the current state of the keyboard. Nothing was
    ///     written to the buffer specified by pwszBuff.
    ///     <br/><br/>
    ///     1 = One character was written to the buffer specified by pwszBuff
    ///     <br/><br/>
    ///     N = Two or more characters were written to the buffer specified by pwszBuff. The most common cause for 
    ///     this is that a dead-key character (accent or diacritic) stored in the keyboard layout could not be combined 
    ///     with the specified virtual key to form a single character. However, the buffer may contain more characters 
    ///     than the return Value specifies. When this happens, any extra characters are invalid and should be ignored.
    /// </returns>
    // https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-tounicodeex
    [DllImport("user32.dll")]
    internal static extern int ToUnicodeEx(uint wVirtKey, uint wScanCode, byte[] lpKeyState, [Out, MarshalAs(UnmanagedType.LPWStr, SizeConst = 64)] StringBuilder pwszBuff, int cchBuff, uint wFlags, IntPtr dwhkl);
    [DllImport("user32.dll")]
    internal static extern int ToUnicodeEx(VirtualKey wVirtKey, uint wScanCode, byte[] lpKeyState, [Out, MarshalAs(UnmanagedType.LPWStr, SizeConst = 64)] StringBuilder pwszBuff, int cchBuff, KeyEventF wFlags, IntPtr dwhkl);

    /// <summary>Translates (maps) a virtual-key code into a scan code or character Value, or translates a scan code into a virtual-key code.</summary>
    /// <param name="uCode">[in] The virtual key code or scan code for a key. How this Value is interpreted depends on the Value of the uMapType parameter.</param>
    /// <param name="uMapType">[in] The translation to be performed. The Value of this parameter depends on the Value of the uCode parameter.</param>
    /// <param name="dwhkl">[in] The input locale identifier used to translate the specified code.</param>
    // https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-mapvirtualkeyw
    [LibraryImport("user32.dll")]
    internal static partial uint MapVirtualKeyW(uint uCode, MapType uMapType);
    [LibraryImport("user32.dll")]
    internal static partial short MapVirtualKeyW(VirtualKey uCode, MapType uMapType);

    //https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-vkkeyscanw
    [LibraryImport("user32.dll", StringMarshalling = StringMarshalling.Utf16)]
    internal static partial short VkKeyScanW(char ch);
}
