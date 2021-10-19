using System.Runtime.InteropServices;

namespace InputSimulation
{
    //Base code: https://stackoverflow.com/questions/20482338/simulate-keyboard-input-in-c-sharp
    internal static class InputSender
    {
        /// <summary>https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-sendinput</summary>
        [DllImport("user32.dll")]
        static extern uint SendInput(uint nInputs, [MarshalAs(UnmanagedType.LPArray), In] InputStruct[] pInputs, int cbSize);

        internal static InputStruct NewMouseInput => new InputStruct(InputType.Mouse);
        internal static InputStruct NewKeyboardInput => new InputStruct(InputType.Keyboard);
        internal static InputStruct NewHardwareInput => new InputStruct(InputType.Hardware);

        public static int SendInput(params InputStruct[] inputs) => (int)SendInput((uint)inputs.Length, inputs, InputStruct.Size);
    }
}