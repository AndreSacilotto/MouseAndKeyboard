﻿using InputSimulation;
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

            Console.WriteLine("RECEIVE: " + mkContent.command);

            switch (mkContent.command)
            {
                case Commands.MouseMove:
                    MouseMove(mkContent.x, mkContent.y);
                    break;
                case Commands.MouseScroll:
                    MouseScroll(mkContent.quant);
                    break;
                case Commands.MouseClick:
                    MouseClick(mkContent.pressedState, mkContent.mouseButton);
                    break;
                case Commands.Key:
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