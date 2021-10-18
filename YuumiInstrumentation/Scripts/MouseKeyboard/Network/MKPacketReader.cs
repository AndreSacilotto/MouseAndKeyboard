using System.Windows.Forms;
using InputSimulation;

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
                case Commands.MouseScroll:
                    response.quant = packet.ReadInt();
                    break;
                case Commands.MouseClick:
                    response.mouseButton = (MouseButtons)packet.ReadInt();
                    response.pressedState = (PressedState)packet.ReadByte();
                    break;
                case Commands.MouseDoubleClick:
                    response.mouseButton = (MouseButtons)packet.ReadInt();
                    response.quant = packet.ReadInt();
                    response.pressedState = (PressedState)packet.ReadByte();
                    break;
                case Commands.Key:
                    response.keys = (Keys)packet.ReadInt();
                    response.pressedState = (PressedState)packet.ReadByte();
                    break;
            }
            return response;
        }
    }
}