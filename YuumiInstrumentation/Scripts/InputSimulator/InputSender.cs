using System;
using System.Runtime.InteropServices;

namespace InputSimulation
{
    //Base code: https://stackoverflow.com/questions/20482338/simulate-keyboard-input-in-c-sharp
    public static class InputSender
    {
        /// <summary>https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-sendinput</summary>
        [DllImport("user32.dll")]
        static extern uint SendInput(uint nInputs, [MarshalAs(UnmanagedType.LPArray), In] InputStruct[] pInputs, int cbSize);

        public static InputStruct NewMouseInput => new InputStruct(InputType.Mouse);
        public static InputStruct NewKeyboardInput => new InputStruct(InputType.Keyboard);
        public static InputStruct NewHardwareInput => new InputStruct(InputType.Hardware);

        public static int SendInput(params InputStruct[] inputs) => (int)SendInput((uint)inputs.Length, inputs, InputStruct.Size);
    }
}