using InputSimulation;
using MouseKeyboard.MKInput;
using MouseKeyboard.Network;
using System;
using System.Windows.Forms;

namespace YuumiInstrumentation
{
    public class YuumiSender : MKInputSender
    {
        private readonly UDPSocketReceiver listener;

        public YuumiSender(UDPSocketReceiver listener)
        {
            this.listener = listener;
            listener.MySocket.SendBufferSize = YuumiPacket.MAX_PACKET_BYTE_SIZE;
            listener.OnReceive += OnReceive;
        }

        private static void OnReceive(int bytes, byte[] data)
        {
            var mkContent = YuumiPacket.ReadAll(data);


            switch (mkContent.command)
            {
                case Commands.MouseMove:
                    Console.WriteLine($"RECEIVE: {mkContent.x} {mkContent.y}");
                    MouseMove(mkContent.x, mkContent.y);
                    break;
                case Commands.MouseScroll:
                    Console.WriteLine($"RECEIVE: {mkContent.command} {mkContent.quant}");
                    MouseScroll(mkContent.quant);
                    break;
                case Commands.MouseClick:
                    Console.WriteLine($"RECEIVE: {mkContent.command} {mkContent.mouseButton} {mkContent.pressedState}");
                    MouseClick(mkContent.pressedState, mkContent.mouseButton);
                    break;
                case Commands.Key:
                    Console.WriteLine($"RECEIVE: {mkContent.command} {mkContent.keys} {mkContent.pressedState}");
                    Key(mkContent.pressedState, mkContent.keys);
                    break;
            }
        }

        private static void MouseMove(int x, int y)
        {
            Mouse.MoveAbsolute(x, y);
        }

        private static void MouseScroll(int scrollQuant)
        {
            Mouse.ScrollWheel(scrollQuant);
        }

        private static void MouseClick(PressedState pressedState, MouseButtons mouseButton)
        {
            MouseButtonExplicit.Click(pressedState, mouseButton);
        }

        private static void Key(PressedState pressedState, Keys key)
        {
            if (pressedState == PressedState.Down)
                KeyboardVK.SendKeyDown(key);
            else if (pressedState == PressedState.Up)
                KeyboardVK.SendKeyUp(key);
            else
                KeyboardVK.SendFull(key);
        }

        public override void Dispose()
        {
            listener.OnReceive -= OnReceive;
        }
    }
}