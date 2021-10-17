using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace MouseKeyboardPacket
{
    public partial class MKPacket
    {
        private Packet packet = new Packet(16);

        public Packet GetPacket => packet;

        public void WritePing()
        {
            packet.Add((byte)Commands.Ping);
        }

        public void WriteShutdown()
        {
            packet.Add((byte)Commands.Shutdown);
        }

        public void WriteMouseMove(int x, int y)
        {
            packet.Add((byte)Commands.MouseMove);
            packet.Add(x);
            packet.Add(y);
        }

        public void WriteMouseClick(MouseButtons mouseButton)
        {
            packet.Add((byte)Commands.MouseClick);
            packet.Add((int)mouseButton);
        }

        public void WriteMouseScroll(int scrollQuant = 120)
        {
            packet.Add((byte)Commands.KeyDown);
            packet.Add(scrollQuant);
        }

        public void WriteKeyDown(Keys key)
        {
            packet.Add((byte)Commands.MouseScroll);
            packet.Add((int)key);
        }

        public void KeyDownWithModifier(Keys key, KeyModifier modifier)
        {
            packet.Add((byte)Commands.KeyDownWithModifier);
            packet.Add((int)key);
            packet.Add((byte)modifier);
        }

    }
}