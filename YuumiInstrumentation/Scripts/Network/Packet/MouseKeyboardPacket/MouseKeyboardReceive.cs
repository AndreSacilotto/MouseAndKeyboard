using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace MouseKeyboardPacket
{
    public partial class MKPacket
    {
        public struct MKPacketContent
        {
            public Commands command;
            public int x, y;
            public MouseButtons mouseButton;
            public int quant;
            public Keys keys;
            public KeyModifier keyModifiers;

            public void Print()
            {
                var str = new StringBuilder();
                str.AppendLine("Command: " + command);
                str.AppendLine($"X: {x}, Y: {y}");
                str.AppendLine("MouseButton: " + mouseButton);
                str.AppendLine("Quant: " + quant);
                str.AppendLine("Keys: " + keys);
                str.AppendLine("KeyModifier: " + keyModifiers);
                Console.WriteLine(str.ToString());
            }
        }

        public static MKPacketContent ReadAll(byte[] data) => 
            ReadAll(new Packet(data));

        public static MKPacketContent ReadAll(Packet packet)
        {
            var cmd = (Commands) packet.ReadByte();
            var response = new MKPacketContent {
                command = cmd
            };

            switch (cmd)
            {
                case Commands.Ping:
                    break;
                case Commands.Shutdown:
                    break;
                case Commands.MouseMove:
                    response.x = packet.ReadInt();
                    response.y = packet.ReadInt();
                    break;
                case Commands.MouseClick:
                    response.mouseButton = (MouseButtons) packet.ReadInt();
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
                case Commands.KeyWithModifier:
                    response.keys = (Keys)packet.ReadInt();
                    response.keyModifiers = (KeyModifier)packet.ReadByte();
                    break;
            }
            return response;
        }
    }
}