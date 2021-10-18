﻿using System;
using System.Text;
using System.Windows.Forms;

namespace MouseKeyboard.Network
{
    public struct MKPacketContent
    {
        public Commands command;
        public int x, y;
        public MouseButtons mouseButton;
        public InputSimulation.PressedState pressedState;
        public int quant;
        public Keys keys;
        public static int Size => sizeof(int) * 4 + sizeof(byte);
        public void Print()
        {
            var str = new StringBuilder();
            str.AppendLine("Command: " + command);
            str.AppendLine($"X: {x}, Y: {y}");
            str.AppendLine("MouseButton: " + mouseButton);
            str.AppendLine("Quant: " + quant);
            str.AppendLine("Keys: " + keys);
            Console.WriteLine(str.ToString());
        }
    }

    public enum Commands : byte
    {
        None = 0,
        /// <summary>Command [B] X [I] Y [I]</summary>
        MouseMove,
        /// <summary>Command [B] Quant [I]</summary>
        MouseScroll,

        /// <summary>Command [B] MouseButton [I] PressState [B]</summary>
        MouseClick,
        /// <summary>Command [B] MouseButton [I] Quant [i]</summary>
        MouseDoubleClick,

        /// <summary>Command [B] Key [I] PressState [B]</summary>
        Key,
    }
}

