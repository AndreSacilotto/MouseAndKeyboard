using System.Windows.Forms;

namespace MouseKeyboard.Network
{
    public static class MKPacketReader
    {
        public static MKPacketContent ReadAll(byte[] data) =>
            ReadAll(new Packet(data));

        public static MKPacketContent ReadAll(Packet packet)
        {
            var cmd = (Commands)packet.ReadByte();
            var response = new MKPacketContent{
                command = cmd
            };

            switch (cmd)
            {
                case Commands.MouseMove:
                    response.x = packet.ReadInt();
                    response.y = packet.ReadInt();
                    break;
                case Commands.MouseClick:
                    response.mouseButton = (MouseButtons)packet.ReadInt();
                    break;
                case Commands.MouseDoubleClick:
                    response.mouseButton = (MouseButtons)packet.ReadInt();
                    response.quant = packet.ReadInt();
                    break;
                case Commands.MouseScroll:
                    response.quant = packet.ReadInt();
                    break;
                case Commands.KeyDown:
                    response.keys = (Keys)packet.ReadInt();
                    break;
                case Commands.KeyUp:
                    response.keys = (Keys)packet.ReadInt();
                    break;
            }
            return response;
        }
    }
}