﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;
using InputSimulation;


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
        public static MKInput GetFunc(Commands index) => dict[index];

        public static void FindMouse(MKPacketContent content, out MouseEventF dwFlags, out MouseDataXButton mdata)
        {
            dwFlags = 0;
            mdata = 0;
            switch (content.mouseButton)
            {
                case MouseButtons.Left:
                    dwFlags = MouseEventF.LeftClick;
                    break;
                case MouseButtons.Right:
                    dwFlags = MouseEventF.RightClick;
                    break;
                case MouseButtons.Middle:
                    dwFlags = MouseEventF.MiddleClick;
                    break;
                case MouseButtons.XButton1:
                    dwFlags = MouseEventF.XClick;
                    mdata = MouseDataXButton.XButton1;
                    break;
                case MouseButtons.XButton2:
                    dwFlags = MouseEventF.XClick;
                    mdata = MouseDataXButton.XButton2;
                    break;
            }
        }

        public static void MouseMove(MKPacketContent content)
        {
            Mouse.MoveAbsolute(content.x, content.y);
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
            KeyboardVK.SendKeyDown(content.keys);
        }

        public static void KeyUp(MKPacketContent content)
        {
            KeyboardVK.SendKeyUp(content.keys);
        }
    }
}