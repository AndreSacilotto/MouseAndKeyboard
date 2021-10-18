using System.Net;

public class NetworkManager
{
    public UDPSocketShipper Sender { get; }
    public UDPSocketReceiver Listener { get; }

    private IPEndPoint ep;

    public NetworkManager(string ip, int port, bool isSender, bool isListener)
    {
        ep = new IPEndPoint(IPAddress.Parse(ip), port);
        if (isSender)
            Sender = new UDPSocketShipper();

        if (isListener)
            Listener = new UDPSocketReceiver(true);
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
