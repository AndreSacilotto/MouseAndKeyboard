namespace MouseAndKeyboard.Native;

//https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-mapvirtualkeyexw
public enum MapType : uint
{
    /// <summary>
    /// VirtualKey to ScanCode
    /// <br/>The uCode parameter is a virtual-key code and is translated into a scan code. 
    /// <br/>If it is a virtual-key code that does not distinguish between left- and right-hand keys, the left-hand scan code is returned. 
    /// <br/>If there is no translation, the function returns 0.
    /// </summary>
    VK_TO_VSC = 0,
    /// <summary>
    /// ScanCode to VirtualKey
    /// <br/>The uCode parameter is a scan code and is translated into a virtual-key code that does not distinguish between left- and right-hand keys. 
    /// <br/>If there is no translation, the function returns 0.
    /// </summary>
    VSC_TO_VK = 1,
    /// <summary>
    /// VirtualKey to Undefined Char
    /// <br/>The uCode parameter is a virtual-key code and is translated into an unshifted character value in the low order word of the return value. 
    /// <br/>Dead keys (diacritics) are indicated by setting the top bit of the return value. 
    /// <br/>If there is no translation, the function returns 0. See Remarks.
    /// </summary>
    VK_TO_CHAR = 2,
    /// <summary>
    /// ScanCode to VirtualKey with L and R
    /// <br/>The uCode parameter is a scan code and is translated into a virtual-key code that distinguishes between left- and right-hand keys. 
    /// <br/>If there is no translation, the function returns 0.
    /// </summary>
    VSC_TO_VK_EX = 3,
    /// <summary>
    /// VirtualKey to ScanCode
    /// <br/>The uCode parameter is a virtual-key code and is translated into a scan code. 
    /// <br/>If it is a virtual-key code that does not distinguish between left- and right-hand keys, the left-hand scan code is returned. 
    /// <br/>If the scan code is an extended scan code, the high byte of the uCode value can contain either 0xe0 or 0xe1 to specify the extended scan code. 
    /// <br/>If there is no translation, the function returns 0.
    /// <br/>~Only on Windows Vista and later~ 
    /// </summary>
    VK_TO_VSC_EX = 4
}
