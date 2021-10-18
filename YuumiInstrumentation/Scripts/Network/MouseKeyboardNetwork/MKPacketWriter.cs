using System.Windows.Forms;

namespace MouseKeyboard.Network
{
    public class MKPacketWriter
    {
        public const int MAX_PACKET_BYTE_SIZE = 16;

        private Packet packet = new Packet(MAX_PACKET_BYTE_SIZE);

        public Packet GetPacket => packet;

        public void Reset()
        {
            packet.Clear();
            packet.Rewind();
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

        public void WriteDoubleMouseClick(MouseButtons mouseButton, int mouseClicks = 2)
        {
            packet.Add((byte)Commands.MouseDoubleClick);
            packet.Add((int)mouseButton);
            packet.Add(mouseClicks);
        }

        public void WriteMouseScroll(int scrollQuant = 120)
        {
            packet.Add((byte)Commands.MouseScroll);
            packet.Add(scrollQuant);
        }

        public void WriteKeyDown(Keys key)
        {
            packet.Add((byte)Commands.KeyDown);
            packet.Add((int)key);
        }

        public void WriteKeyUp(Keys key)
        {
            packet.Add((byte)Commands.KeyUp);
            packet.Add((int)key);
        }

    }
}