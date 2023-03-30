using MouseAndKeyboard.Native;
using System.Runtime.InteropServices;

namespace MouseAndKeyboard.InputSimulation;

//Base code: https://stackoverflow.com/a/20493025
internal static partial class InputSender
{
    /// https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-sendinput
    [LibraryImport("user32.dll")]
    internal static partial uint SendInput(uint nInputs, [MarshalAs(UnmanagedType.LPArray)] InputStruct[] pInputs, int cbSize);

    public static uint SendInput(params InputStruct[] inputs) => SendInput((uint)inputs.Length, inputs, InputStruct.Size);
}