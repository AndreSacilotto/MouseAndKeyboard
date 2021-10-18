using System;
using System.Collections.Generic;
using System.Windows.Forms;
using InputSimulation;


namespace MouseKeyboard.Network
{
    public static class MKInputSender
    {
        public delegate void MKInput(MKPacketContent content);

        private static Dictionary<Commands, MKInput> dict = new Dictionary<Commands, MKInput> {
            { Commands.MouseMove, MouseMove },
            { Commands.MouseScroll, MouseScroll },
            { Commands.MouseClick, MouseClick },
            { Commands.MouseDoubleClick, MouseDoubleClick },
            { Commands.Key, Key },
        };

        public static bool TryGetFunc(Commands index, out MKInput mk) => dict.TryGetValue(index, out mk);
        public static MKInput GetFunc(Commands index) => dict[index];

        public static void MouseMove(MKPacketContent content)
        {
            Mouse.MoveAbsolute(content.x, content.y);
        }

        public static void MouseScroll(MKPacketContent content)
        {
            Mouse.ScrollWheel(content.quant);
        }

        public static void MouseClick(MKPacketContent content)
        {
            MouseButtonExplicit.Click(content.pressedState, content.mouseButton);
        }

        public static void MouseDoubleClick(MKPacketContent content)
        {
            MouseButtonExplicit.Click(content.pressedState, content.mouseButton, content.quant);
        }

        public static void Key(MKPacketContent content)
        {
            if(content.pressedState == PressedState.Down)
                KeyboardVK.SendKeyDown(content.keys);
            else if(content.pressedState == PressedState.Up)
                KeyboardVK.SendKeyUp(content.keys);
            else
                KeyboardVK.SendFull(content.keys);
        }
    }
}