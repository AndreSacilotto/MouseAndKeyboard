using InputSimulation;
using MouseKeyboard.MKInput;
using MouseKeyboard.Network;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace YuumiInstrumentation
{
    public class YuumiListen : MKInputListen
    {
        private readonly YuumiPacket mkPacket;
        private readonly UDPSocketShipper client;

        private Keys enablingKey = Keys.Scroll;

        public YuumiListen(UDPSocketShipper client) : base()
        {
            this.client = client;
            mkPacket = new YuumiPacket();

            inputEvents.KeyDown += OnKeyDown;
            if (Control.IsKeyLocked(enablingKey))
                Subscribe();
            else
                enabled = false;
        }
        public override void Dispose()
        {
            inputEvents.KeyDown -= OnKeyDown;
            base.Dispose();
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

        private void EnableFunc()
        {
            if (enabled)
                Unsubscribe();
            else
                Subscribe();
            Console.WriteLine("Enabled: " + enabled);
        }

        bool isCtrl = false;
        //bool isShift = false;
        //bool isAlt = false;
        private void SetModifiers(Keys modifiers, bool enable)
        {
            if (modifiers == Keys.Control)
                isCtrl = enable;
            //if (modifiers == Keys.Shift)
            //    isShift = enable;
            //if(modifiers == Keys.Alt)
            //    isAlt = enable;
        }


        #endregion

        #region EVENTS
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

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == enablingKey)
                EnableFunc();
            else
            {
                SetModifiers(e.Modifiers, true);
                UnifyKey(e, PressedState.Down);
            }
        }

        private void OnKeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != enablingKey)
            {
                SetModifiers(e.Modifiers, false);
                UnifyKey(e, PressedState.Up);
            }
        }
        #endregion

        #region Unify

        private void UnifyMouse(MouseEventArgs e, PressedState pressed)
        {
            if (mouseToKey.TryGetValue(e.Button, out var key))
            {
                Console.WriteLine($"SEND M{pressed,5}: {e.Button} => {key}");
                WriteKey(key, pressed);
            }
            else
            {
                Console.WriteLine($"SEND M{pressed,5}: {e.Button}");
                mkPacket.WriteMouseClick(e.Button, pressed);
                SendPacket();
            }
        }

        private void UnifyKey(KeyEventArgs e, PressedState pressed)
        {
            if (isCtrl && allowedWithControlKeys.Contains(e.KeyCode))
            {
                Console.WriteLine($"SEND K{pressed,5}: {e.KeyCode} | Shift");
                WriteKey(e.KeyCode, pressed);
            }
            else if (allowedKeys.TryGetValue(e.KeyCode, out var key))
            {
                Console.WriteLine($"SEND K{pressed,5}: {e.KeyCode} => {key}");
                WriteKey(key, pressed);
            }
        }

        #endregion

        //Q W E R - Skills 4
        //D F - Spells 2
        //Space D1 D2 D4 - Itens 4
        //Y P B - Util 2

        #region Buttons Data

        private static Dictionary<MouseButtons, Keys> mouseToKey = new Dictionary<MouseButtons, Keys> {
            { MouseButtons.XButton1, Keys.Q },
            { MouseButtons.XButton2, Keys.E },
        };

        private static Dictionary<Keys, Keys> allowedKeys = new Dictionary<Keys, Keys> {
            { Keys.D8, Keys.R },
            { Keys.D9, Keys.D4 },
            { Keys.D0, Keys.D1 },
            { Keys.OemBackslash, Keys.D2 },
        };

        private static HashSet<Keys> allowedWithControlKeys = new HashSet<Keys> {
            Keys.D,
            Keys.F,
            Keys.Space,
            Keys.B,
            Keys.Y,
            Keys.P,
        };

        #endregion
    }
}