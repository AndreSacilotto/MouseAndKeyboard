using System;
using System.Windows.Forms;
using Gma.System.MouseKeyHook;

namespace MouseKeyboard.Network
{
    public class MKInputListen : IDisposable
    {
        private readonly IKeyboardMouseEvents inputEvents;
        private readonly MKPacketWriter mkPacket;
        private readonly UDPSocketShipper client;

        private KeyEventHandler KeyDown;

        private bool enabled = false;
        public Keys enablingKey;

        public MKInputListen(UDPSocketShipper client, bool enabled = false)
        {
            this.client = client;
            mkPacket = new MKPacketWriter();
            inputEvents = Hook.GlobalEvents();

            inputEvents.KeyDown += OnKeyDownBase;
            inputEvents.KeyUp += OnKeyUpBase;

            if (enabled)
                Subscribe();
        }

        #region SUB
        public void Subscribe()
        {
            enabled = true;

            inputEvents.MouseMove += OnMouseMove;
            inputEvents.MouseDown += OnMouseDown;
            inputEvents.MouseUp += OnMouseUp;
            inputEvents.MouseDoubleClick += OnMouseDoubleClick;
            inputEvents.MouseWheel += OnMouseScroll;

            //inputEvents.KeyDown += OnKeyDown;
            KeyDown = OnKeyDown;
            inputEvents.KeyUp += OnKeyUp;
        }

        public void Unsubscribe()
        {
            enabled = false;

            inputEvents.MouseMove -= OnMouseMove;
            inputEvents.MouseDown -= OnMouseDown;
            inputEvents.MouseDoubleClick -= OnMouseDoubleClick;
            inputEvents.MouseWheel -= OnMouseScroll;

            KeyDown = null;
            inputEvents.KeyUp -= OnKeyUp;
        }
        #endregion

        #region SEND

        private void SendPacket()
        {
            client.Send(mkPacket.GetPacket);
            mkPacket.Reset();
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            //MKEventHandleUtil.Print(e);
            Console.WriteLine("SEND: " + e.X + " " + e.Y);

            mkPacket.WriteMouseMove(e.X, e.Y);
            SendPacket();
        }

        private void OnMouseScroll(object sender, MouseEventArgs e)
        {
            //MKEventHandleUtil.Print(e);
            Console.WriteLine("SEND: " + e.Delta);

            mkPacket.WriteMouseScroll(e.Delta);
            SendPacket();
        }

        private void OnMouseDown(object sender, MouseEventArgs e)
        {
            //MKEventHandleUtil.Print(e);
            Console.WriteLine("SEND: " + e.Button);

            mkPacket.WriteMouseClick(e.Button, InputSimulation.PressedState.Down);
            SendPacket();
        }

        private void OnMouseUp(object sender, MouseEventArgs e)
        {
            //MKEventHandleUtil.Print(e);
            Console.WriteLine("SEND: " + e.Button);

            mkPacket.WriteMouseClick(e.Button, InputSimulation.PressedState.Up);
            SendPacket();
        }

        private void OnMouseDoubleClick(object sender, MouseEventArgs e)
        {
            //MKEventHandleUtil.Print(e);
            Console.WriteLine("SEND: " + e.Button + " 2");

            mkPacket.WriteDoubleMouseClick(e.Button, 2);
            SendPacket();
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            //MKEventHandleUtil.Print(e);
            Console.WriteLine("SEND: " + e.KeyCode);

            mkPacket.WriteKey(e.KeyCode, InputSimulation.PressedState.Down);
            SendPacket();
        }

        private void OnKeyUp(object sender, KeyEventArgs e)
        {
            //MKEventHandleUtil.Print(e);
            Console.WriteLine("SEND: " + e.KeyCode);

            mkPacket.WriteKey(e.KeyCode, InputSimulation.PressedState.Up);
            SendPacket();
        }

        #endregion

        private bool isHolding = false;
        private void OnKeyDownBase(object sender, KeyEventArgs e)
        {
            if (isHolding)
                return;
            isHolding = true;

            if (e.KeyCode == enablingKey)
            {
                if (enabled)
                    Unsubscribe();
                else
                    Subscribe();
                Console.WriteLine("Enabled: " + enabled);
            }
            else
            {
                MKEventHandleUtil.Print(e);
                KeyDown?.Invoke(sender, e);
            }
        }

        private void OnKeyUpBase(object sender, KeyEventArgs e) => isHolding = false;

        public void Dispose()
        {
            Unsubscribe();
            inputEvents.KeyDown -= OnKeyDownBase;
            inputEvents.KeyUp -= OnKeyUpBase;
            inputEvents.Dispose();
        }
    }
}
