using InputSimulation;
using MouseKeyboard.Network;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace YuumiInstrumentation
{
    public class YuumiSender : MouseKeyboard.MKInput.MKInputSender, IYuumiPacketReceiver
    {
        private readonly UDPSocketReceiver listener;

        private readonly YuumiPacketRead yuumiRead;

        public YuumiSender(UDPSocketReceiver listener)
        {
            this.listener = listener;
            listener.MySocket.SendBufferSize = YuumiPacketWrite.MAX_PACKET_BYTE_SIZE;
            listener.OnReceive += OnReceive;

            yuumiRead = new YuumiPacketRead(this);
        }

        private void OnReceive(int bytes, byte[] data)
        {
            yuumiRead.ReadAll(data);
        }

        public void MouseMove(int x, int y)
        {
            Console.WriteLine($"RECEIVE: MOVE {x} {y}");
            Mouse.MoveAbsolute(x, y);
        }

        public void MouseScroll(int scrollDelta)
        {
            Console.WriteLine($"RECEIVE: MScroll {scrollDelta}");
            Mouse.ScrollWheel(scrollDelta);
        }

        public void MouseClick(MouseButtons mouseButton, PressedState pressedState)
        {
            Console.WriteLine($"RECEIVE: MClick {mouseButton} {pressedState}");

            MouseButtonExplicit.Click(pressedState, mouseButton);
        }


        private static HashSet<Keys> focusKeys = new HashSet<Keys> {
            Keys.F1, Keys.F2, Keys.F3, Keys.F4, Keys.F5,
        };
        private Keys currentFocusKey = Keys.None;

        public void Key(Keys keys, PressedState pressedState)
        {
            Console.WriteLine($"RECEIVE: Key {keys} {pressedState}");

            if (focusKeys.Contains(keys)){
                Keyboard.SendKeyUp(currentFocusKey);
                currentFocusKey = keys;
                Keyboard.SendKeyDown(keys);
                return;
            }

            if (pressedState == PressedState.Down)
            {
                Keyboard.SendKeyDown(keys);
            }
            else if (pressedState == PressedState.Up)
                Keyboard.SendKeyUp(keys);
            else
                Keyboard.SendFull(keys);
        }

        public void KeyModifier(Keys keys, Keys modifier, PressedState pressedState)
        {
            Console.WriteLine($"RECEIVE: KeyMod {keys} {pressedState}");

            if (pressedState == PressedState.Down)
                KeyboardWithMod.SendKeyDown(keys, modifier);
            else if (pressedState == PressedState.Up)
                KeyboardWithMod.SendKeyUp(keys, modifier);
            else
                KeyboardWithMod.SendFull(keys, modifier);
        }

        public override void Dispose()
        {
            listener.OnReceive -= OnReceive;
        }
    }
}