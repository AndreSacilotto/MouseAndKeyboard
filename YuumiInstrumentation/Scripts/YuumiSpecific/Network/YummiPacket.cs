using System.Windows.Forms;
using InputSimulation;
using Yuumi.Network;

namespace MouseKeyboard.Network
{
    public class YummiPacket
    {

        #region WRITE 
        
        public const int MAX_PACKET_BYTE_SIZE = 16;

        private Packet packet = new Packet(MAX_PACKET_BYTE_SIZE);

        public Packet GetPacket => packet;

        public void Reset()
        {
            packet.Reset();
            packet.Rewind();
        }

        public void WriteMouseMove(int x, int y)
        {
            packet.Add((byte)Commands.MouseMove);
            packet.Add(x);
            packet.Add(y);
        }

        public void WriteMouseScroll(int scrollQuant = 120)
        {
            packet.Add((byte)Commands.MouseScroll);
            packet.Add(scrollQuant);
        }

        public void WriteMouseClick(MouseButtons mouseButton, PressedState pressedState)
        {
            packet.Add((byte)Commands.MouseClick);
            packet.Add((int)mouseButton);
            packet.Add((byte)pressedState);
        }

        public void WriteDoubleMouseClick(MouseButtons mouseButton, int mouseClicks = 2)
        {
            packet.Add((byte)Commands.MouseDoubleClick);
            packet.Add((int)mouseButton);
            packet.Add(mouseClicks);
            packet.Add((byte)PressedState.Click);
        }

        public void WriteKey(Keys key, PressedState pressedState)
        {
            packet.Add((byte)Commands.Key);
            packet.Add((int)key);
            packet.Add((byte)pressedState);
        }

        #endregion

        #region READ

        public static YummiPacketContent ReadAll(byte[] data) =>
            ReadAll(new Packet(data));

        public static YummiPacketContent ReadAll(Packet packet)
        {
            var cmd = (Commands)packet.ReadByte();
            var response = new YummiPacketContent
            {
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

        #endregion

    }
}