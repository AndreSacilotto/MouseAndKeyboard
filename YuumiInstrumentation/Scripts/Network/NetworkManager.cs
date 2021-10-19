using System.Net;

namespace MouseKeyboard.Network
{
    public class NetworkManager
    {
        public UDPSocketShipper Sender { get; }
        public UDPSocketReceiver Listener { get; }

        private IPEndPoint ep;

        public NetworkManager(string ip, int port, bool isReceiver, bool isShipper)
        {
            ep = new IPEndPoint(IPAddress.Parse(ip), port);

            if (isReceiver)
                Listener = new UDPSocketReceiver(true);

            if (isShipper)
                Sender = new UDPSocketShipper();
        }

        public void Start()
        {
            Sender?.Start(ep);
            Listener?.Start(ep);
        }

        public void Stop()
        {
            Sender?.Stop();
            Listener?.Stop();
        }
    }
}