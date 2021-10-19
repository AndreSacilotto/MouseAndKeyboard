using System;
using System.Windows.Forms;

using MouseKeyboard.Network;
using InputSimulation;

namespace YuumiInstrumentation
{
    public class YuumiSender : MouseKeyboard.MKInput.MKInputSender
    {
        private readonly UDPSocketReceiver listener;

        private readonly YuumiPacketRead yuumiRead; 

        public YuumiSender(UDPSocketReceiver listener)
        {
            this.listener = listener;
            listener.MySocket.SendBufferSize = YuumiPacketWrite.MAX_PACKET_BYTE_SIZE;
            listener.OnReceive += OnReceive;

            yuumiRead = new YuumiPacketRead();

            yuumiRead.MouseMove += MouseMove;
            yuumiRead.MouseScroll += MouseScroll;
            yuumiRead.MouseClick += MouseClick;
            yuumiRead.Key += Key;
            yuumiRead.KeyModifier += KeyModifier;
        }

        private void OnReceive(int bytes, byte[] data)
        {
            yuumiRead.ReadAll(data);
        }

        private static void MouseMove(int x, int y)
        {
            Console.WriteLine($"RECEIVE: MOVE {x} {y}");
            Mouse.MoveAbsolute(x, y);
        }

        private static void MouseScroll(int scrollDelta)
        {
            Console.WriteLine($"RECEIVE: MScroll {scrollDelta}");
            Mouse.ScrollWheel(scrollDelta);
        }

        private static void MouseClick(MouseButtons mouseButton, PressedState pressedState)
        {
            Console.WriteLine($"RECEIVE: MClick {mouseButton} {pressedState}");

            MouseButtonExplicit.Click(pressedState, mouseButton);
        }

        private static void Key(Keys keys, PressedState pressedState)
        {
            Console.WriteLine($"RECEIVE: Key {keys} {pressedState}");

            if (pressedState == PressedState.Down)
                Keyboard.SendKeyDown(keys);
            else if (pressedState == PressedState.Up)
                Keyboard.SendKeyUp(keys);
            else
                Keyboard.SendFull(keys);
        }

        private static void KeyModifier(Keys keys, Keys modifier, PressedState pressedState)
        {
            Console.WriteLine($"RECEIVE: KeyMod {keys} {pressedState}");

            if (pressedState == PressedState.Down)
                Keyboard.SendWithModifier(keys);
            else if (pressedState == PressedState.Up)
                Keyboard.SendKeyUp(keys);
            else
                Keyboard.SendFull(keys);
        }

        public override void Dispose()
        {
            listener.OnReceive -= OnReceive;
            yuumiRead.Dispose();
        }
    }
}