using System;
using System.Text;
using System.Windows.Forms;

namespace YuumiInstrumentation
{
    public struct YuumiPacketContent
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
}

