using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace MouseKeyboard.Network
{
    public static class MKInputSender
    {
        public delegate void MKInput(MKPacketContent content);

        private static Dictionary<Commands, MKInput> dict = new Dictionary<Commands, MKInput> {
            {Commands.MouseMove, MouseMove},
            {Commands.MouseClick, MouseClick},
            {Commands.MouseDoubleClick, MouseDoubleClick},
            {Commands.MouseScroll, MouseScroll},
            {Commands.KeyDown, KeyDown},
            {Commands.KeyUp, KeyUp},
        };

        //public MKInputSender(MKInput MouseMove, MKInput MouseClick, MKInput MouseDoubleClick, MKInput MouseScroll, MKInput KeyDown, MKInput KeyUp)
        public static bool TryGetFunc(Commands index, out MKInput mk) => dict.TryGetValue(index, out mk);

        public static void MouseMove(MKPacketContent content)
        {
            Mouse.MoveAbsolute(content.x, content.y);
        }

        public static void FindMouse(MKPacketContent content, out InputSender.MouseEventF dwFlags, out InputSender.MouseDataXButton mdata)
        {
            dwFlags = 0;
            mdata = 0;
            switch (content.mouseButton)
            {
                case MouseButtons.Left:
                    dwFlags = InputSender.MouseEventF.LeftClick;
                    break;
                case MouseButtons.Right:
                    dwFlags = InputSender.MouseEventF.RightClick;
                    break;
                case MouseButtons.Middle:
                    dwFlags = InputSender.MouseEventF.MiddleClick;
                    break;
                case MouseButtons.XButton1:
                    dwFlags = InputSender.MouseEventF.XClick;
                    mdata = InputSender.MouseDataXButton.XButton1;
                    break;
                case MouseButtons.XButton2:
                    dwFlags = InputSender.MouseEventF.XClick;
                    mdata = InputSender.MouseDataXButton.XButton2;
                    break;
            }
        }

        public static void MouseClick(MKPacketContent content)
        {
            FindMouse(content, out var dwFlags, out var mdata);
            if (mdata != 0)
                Mouse.Click(dwFlags, mdata);
            Mouse.Click(dwFlags);
        }

        public static void MouseDoubleClick(MKPacketContent content)
        {
            FindMouse(content, out var dwFlags, out var mdata);
            if (mdata != 0)
                Mouse.Click(dwFlags, mdata, content.quant);
            Mouse.Click(dwFlags, content.quant);
        }

        public static void MouseScroll(MKPacketContent content)
        {
            Mouse.ScrollWheel(content.quant);
        }


        public static void KeyDown(MKPacketContent content)
        {
            Keyboard.SendKeyDown(content.keys);
        }

        public static void KeyUp(MKPacketContent content)
        {
            Keyboard.SendKeyUp(content.keys);
        }
    }
}