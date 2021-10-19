using System.Collections.Generic;
using System.Windows.Forms;
using MouseKeyboard.Network;
using InputSimulation;
using System;

namespace YuumiInstrumentation
{
    public class YuumiPacketRead : IDisposable
    {
        public event Action<int, int> MouseMove;

        public event Action<int> MouseScroll;

        public event Action<MouseButtons, PressedState> MouseClick;

        public event Action<Keys, PressedState> Key;

        public event Action<Keys, Keys, PressedState> KeyModifier;

        #region READ

        public void ReadAll(byte[] data) => ReadAll(new Packet(data));

        public void ReadAll(Packet packet)
        {
            var cmd = (Commands)packet.ReadByte();

            switch (cmd)
            {
                case Commands.MouseMove:
                    ReadMouseMove(packet);
                    break;
                case Commands.MouseScroll:
                    ReadMouseScroll(packet);
                    break;
                case Commands.MouseClick:
                    ReadMouseClick(packet);
                    break;
                case Commands.Key:
                    ReadKey(packet);
                    break;
                case Commands.KeyModifier:
                    ReadKeyModifier(packet);
                    break;
            }
        }

        private void ReadMouseMove(Packet packet)
        {
            var x = packet.ReadInt();
            var y = packet.ReadInt();
            MouseMove(x, y);
        }

        private void ReadMouseScroll(Packet packet)
        {
            int scrollDelta = packet.ReadInt();
            MouseScroll?.Invoke(scrollDelta);
        }

        private void ReadMouseClick(Packet packet)
        {
            var mouseButton = (MouseButtons)packet.ReadInt();
            var pressedState = (PressedState)packet.ReadByte();
            MouseClick?.Invoke(mouseButton, pressedState);
        }

        private void ReadKey(Packet packet)
        {
            var keys = (Keys)packet.ReadInt();
            var pressedState = (PressedState)packet.ReadByte();
            Key?.Invoke(keys, pressedState);
        }

        private void ReadKeyModifier(Packet packet)
        {
            ReadKeyModifier(packet);
            var keys = (Keys)packet.ReadInt();
            var mods = (Keys)packet.ReadInt();
            var pressedState = (PressedState)packet.ReadByte();
            KeyModifier?.Invoke(keys, mods, pressedState);
        }


        #endregion

        public void Dispose()
        {
            MouseMove = null;
            MouseScroll = null;
            MouseClick = null;
            Key = null;
            KeyModifier = null;
        }
    }
}