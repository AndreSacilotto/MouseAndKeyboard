using System;
using System.Windows.Forms;

namespace MouseKeyboard.Network
{
    public class MKInputListener : IDisposable
    {
        private readonly InputListener inputListener;
        private readonly MKPacket mkPacket = new MKPacket();
        private readonly UDPSocketClient client;

        public MKInputListener(UDPSocketClient client)
        {
            this.client = client;
            inputListener = new InputListener(OnMouseMove, OnMouseDown, OnMouseDoubleClick, OnMouseScroll, OnKeyDown, OnKeyUp);
        }

        private void SendPacket()
        {
            client.Send(mkPacket.GetPacket);
            mkPacket.Reset();
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            //InputListenerUtil.Print(e);
            Console.WriteLine("SEND: " + e.X + " " + e.Y);

            mkPacket.WriteMouseMove(e.X, e.Y);
            SendPacket();
        }

        private void OnMouseScroll(object sender, MouseEventArgs e)
        {
            //InputListenerUtil.Print(e);
            Console.WriteLine("SEND: " + e.Delta);

            mkPacket.WriteMouseScroll(e.Delta);
            SendPacket();
        }

        private void OnMouseDown(object sender, MouseEventArgs e)
        {
            //InputListenerUtil.Print(e);
            Console.WriteLine("SEND: " + e.Button);

            mkPacket.WriteMouseClick(e.Button);
            SendPacket();
        }

        private void OnMouseDoubleClick(object sender, MouseEventArgs e)
        {
            //InputListenerUtil.Print(e);
            Console.WriteLine("SEND: " + e.Button + " 2");

            mkPacket.WriteDoubleMouseClick(e.Button, 2);
            SendPacket();
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            //InputListenerUtil.Print(e);
            Console.WriteLine("SEND: " + e.KeyCode);

            mkPacket.WriteKeyDown(e.KeyCode);
            SendPacket();
        }

        private void OnKeyUp(object sender, KeyEventArgs e)
        {
            //InputListenerUtil.Print(e);
            Console.WriteLine("SEND: " + e.KeyCode);

            mkPacket.WriteKeyUp(e.KeyCode);
            SendPacket();
        }

        public void Dispose()
        {
            inputListener.Dispose();
        }
    }
}
