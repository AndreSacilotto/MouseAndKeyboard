using InputSimulation;
using MouseKeyboard.Network;
using System.Windows.Forms;

namespace YuumiInstrumentation
{
    public class YuumiPacketRead
    {
        private readonly IYuumiPacketReceiver receiver;

        public YuumiPacketRead(IYuumiPacketReceiver receiver)
        {
            this.receiver = receiver;
        }

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

        #region READ

        private void ReadMouseMove(Packet packet)
        {
            var x = packet.ReadInt();
            var y = packet.ReadInt();
            receiver.MouseMove(x, y);
        }

        private void ReadMouseScroll(Packet packet)
        {
            int scrollDelta = packet.ReadInt();
            receiver.MouseScroll(scrollDelta);
        }

        private void ReadMouseClick(Packet packet)
        {
            var mouseButton = (MouseButtons)packet.ReadInt();
            var pressedState = (PressedState)packet.ReadByte();
            receiver.MouseClick(mouseButton, pressedState);
        }

        private void ReadKey(Packet packet)
        {
            var keys = (Keys)packet.ReadInt();
            var pressedState = (PressedState)packet.ReadByte();
            receiver.Key(keys, pressedState);
        }

        private void ReadKeyModifier(Packet packet)
        {
            ReadKeyModifier(packet);
            var keys = (Keys)packet.ReadInt();
            var mods = (Keys)packet.ReadInt();
            var pressedState = (PressedState)packet.ReadByte();
            receiver.KeyModifier(keys, mods, pressedState);
        }


        #endregion

    }
}