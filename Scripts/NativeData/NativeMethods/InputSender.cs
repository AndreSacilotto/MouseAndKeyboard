using System.Runtime.InteropServices;

namespace MouseAndKeyboard.Native;

//Base code: https://stackoverflow.com/a/20493025
internal static partial class InputSender
{
    /// https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-sendinput
    [LibraryImport(User32.USER_32)]
    internal static partial uint SendInput(uint nInputs, [MarshalAs(UnmanagedType.LPArray)] InputStruct[] pInputs, int cbSize);
    [LibraryImport(User32.USER_32)]
    internal static partial uint SendInput(uint nInputs, ref InputStruct pInputs, int cbSize);

    public static uint SendInput(InputStruct input) => SendInput(1, ref input, InputStruct.Size);
    public static uint SendInput(params InputStruct[] inputs) => SendInput((uint)inputs.Length, inputs, InputStruct.Size);
}