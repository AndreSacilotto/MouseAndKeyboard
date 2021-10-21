using InputSimulation;
using MouseKeyboard.Network;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace YuumiInstrumentation
{
    public class YuumiListen : MouseKeyboard.MKInput.MKInputListen
    {
        private readonly YuumiPacketWrite mkPacket;
        private readonly UDPSocketShipper client;

        public YuumiListen(UDPSocketShipper client) : base()
        {
            this.client = client;
            mkPacket = new YuumiPacketWrite();

            inputEvents.KeyDown += OnKeyDown;
            if (Control.IsKeyLocked(enablingKey))
                Subscribe();
            else
                enabled = false;
        }
        public override void Dispose()
        {
            base.Dispose();
            inputEvents.KeyDown -= OnKeyDown;
        }

        #region SUB
        public override void Subscribe()
        {
            base.Subscribe();

            inputEvents.MouseMove += OnMouseMove;
            inputEvents.MouseDown += OnMouseDown;
            inputEvents.MouseUp += OnMouseUp;
            inputEvents.MouseWheel += OnMouseScroll;

            //inputEvents.KeyDown += OnKeyDown;
            inputEvents.KeyUp += OnKeyUp;
        }

        public override void Unsubscribe()
        {
            base.Unsubscribe();

            inputEvents.MouseMove -= OnMouseMove;
            inputEvents.MouseDown -= OnMouseDown;
            inputEvents.MouseWheel -= OnMouseScroll;

            //inputEvents.KeyDown -= OnKeyDown;
            inputEvents.KeyUp -= OnKeyUp;
        }
        #endregion

        #region UTIL

        private void SendPacket()
        {
            client.Send(mkPacket.GetPacket);
            mkPacket.Reset();
        }

        private void WriteKey(Keys key, PressedState pressedState)
        {
            mkPacket.WriteKey(key, pressedState);
            SendPacket();
        }

        bool isCtrl = false;
        bool isShift = false;
        //bool isAlt = false;
        private void SetModifiers(Keys modifiers, bool enable)
        {
            if (modifiers == Keys.Control)
                isCtrl = enable;
            if (modifiers == Keys.Shift)
                isShift = enable;
            //if(modifiers == Keys.Alt)
            //    isAlt = enable;
        }

        #endregion

        #region MOUSE EVENTS
        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            Console.WriteLine("SEND Move: " + e.X + " " + e.Y);
            mkPacket.WriteMouseMove(e.X, e.Y);
            SendPacket();
        }

        private void OnMouseScroll(object sender, MouseEventArgs e)
        {
            Console.WriteLine("SEND Delta: " + e.Delta);
            mkPacket.WriteMouseScroll(e.Delta);
            SendPacket();
        }

        private void OnMouseDown(object sender, MouseEventArgs e) => UnifyMouse(e, PressedState.Down);

        private void OnMouseUp(object sender, MouseEventArgs e) => UnifyMouse(e, PressedState.Up);

        #endregion

        #region KEYS EVENTS
        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == enablingKey)
            {
                if (enabled)
                    Unsubscribe();
                else
                    Subscribe();
                Console.WriteLine("Enabled: " + enabled);
            }
            else if(enabled)
            {
                SetModifiers(e.Modifiers, true);
                UnifyKey(e, PressedState.Down);
            }
        }

        private void OnKeyUp(object sender, KeyEventArgs e)
        {
            if (enabled && e.KeyCode != enablingKey)
            {
                UnifyKey(e, PressedState.Up);
                SetModifiers(e.Modifiers, false);
            }
        }
        #endregion

        #region Unify

        private void UnifyMouse(MouseEventArgs e, PressedState pressed)
        {
            if (mouseToKey.TryGetValue(e.Button, out var key))
            {
                Console.WriteLine($"SEND M{pressed,-5}: {e.Button} => {key}");
                WriteKey(key, pressed);
            }
            else
            {
                Console.WriteLine($"SEND M{pressed,-5}: {e.Button}");
                mkPacket.WriteMouseClick(e.Button, pressed);
                SendPacket();
            }
        }

        private void UnifyKey(KeyEventArgs e, PressedState pressed)
        {
            if (isShift)
            {
                if (isCtrl && mirrorWhenControlShiftKeys.Contains(e.KeyCode))
                {
                    Console.WriteLine($"SEND K{pressed,-5}: {e.KeyCode} | ShiftControl");
                    WriteKey(e.KeyCode, pressed);

                    mkPacket.WriteKeyModifier(e.KeyCode, Keys.LControlKey, pressed);
                    SendPacket();
                }
                else if (mirrorWhenShiftKeys.Contains(e.KeyCode))
                {
                    Console.WriteLine($"SEND K{pressed,-5}: {e.KeyCode} | Shift");
                    WriteKey(e.KeyCode, pressed);
                }
            }
            else if (mirrorKeys.Contains(e.KeyCode))
            {
                Console.WriteLine($"SEND K{pressed,-5}: {e.KeyCode}");
                WriteKey(e.KeyCode, pressed);
            }
        }

        #endregion


        #region Buttons Data

        //Q W E R - Skills 4
        //D F - Spells 2
        //Space D1 D2 D4 - Itens 4
        //Y P B - Util 2

        private readonly Keys enablingKey = Keys.Scroll;

        private readonly static Dictionary<MouseButtons, Keys> mouseToKey = new Dictionary<MouseButtons, Keys> {
            { MouseButtons.XButton1, Keys.F },
            { MouseButtons.XButton2, Keys.D },
        };

        private readonly static HashSet<Keys> mirrorKeys = new HashSet<Keys> {
            Keys.Escape,
            Keys.P,

            Keys.F1,
            Keys.F2,
            Keys.F3,
            Keys.F4,
            Keys.F5,
        };

        private readonly static HashSet<Keys> mirrorWhenControlShiftKeys = new HashSet<Keys>
        {
            Keys.Q,
            Keys.W,
            Keys.E,
            Keys.R,
        };

        private readonly static HashSet<Keys> mirrorWhenShiftKeys = new HashSet<Keys> {
            Keys.Q,
            Keys.W,
            Keys.E,
            Keys.R,
            Keys.D1,
            Keys.D2,
            Keys.D3,
            Keys.D4,
            Keys.Space,
            Keys.D,
            Keys.F,

            Keys.Y,
            Keys.B,
        };

        #endregion
    }
}